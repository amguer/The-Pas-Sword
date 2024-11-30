using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialoguePlayer : MonoBehaviour
{
    private UIManager uiManager;
    private TextMeshProUGUI titleBox;
    private TextMeshProUGUI dialogueBox;
    private bool isPlaying;
    private string currentString;
    private int currentIndex;

    [SerializeField] float playBackSpeed;
    [SerializeField] bool disableMovementUntilFinished;
    [SerializeField] [TextArea] List<string> dialogue;
    [SerializeField] private UnityEvent OnDialogueStart;
    [SerializeField] private UnityEvent OnDialogueEnd;

    private void Start()
    {
        uiManager = UIManager.Instance;
        if(uiManager != null)
        {
            titleBox = uiManager.TitleBox;
            dialogueBox = uiManager.DialogueBox;
        }
        
    }

    public void BeginDialogue()
    {
        if(dialogue.Count > 0)
        {
            if(disableMovementUntilFinished)
            {
                InputManager.Instance.EnableMovementControls(false);
            }
            uiManager.EnableDialogueUI();
            uiManager.ProgressArrow.gameObject.SetActive(false);
            OnDialogueStart?.Invoke();
            InputManager.OnGetSpace += ProgressDialogue;
            currentString = dialogue[0];
            currentIndex = 0;
            StartCoroutine(PlayDialogue(dialogue[0]));
        }
        else
        {
            Debug.Log("No dialogue has been written for this interaction.");
        }
        
    }

    private IEnumerator PlayDialogue(string text)
    {
        isPlaying = true;
        dialogueBox.text = string.Empty;
        int textLength = text.Length;

        for(int i = 0; i < textLength; i++)
        {
            dialogueBox.text += text[i];
            yield return new WaitForSeconds(playBackSpeed);
        }
        isPlaying = false;
    }

    private void ProgressDialogue()
    {
        if(isPlaying == true)
        {
            StopAllCoroutines();
            dialogueBox.text = currentString;
            uiManager.ProgressArrow.gameObject.SetActive(true);
            isPlaying = false;
        }
        else if(isPlaying == false && currentIndex < dialogue.Count - 1)
        {
            currentIndex++;
            currentString = dialogue[currentIndex];
            StartCoroutine(PlayDialogue(dialogue[currentIndex]));
            uiManager.ProgressArrow.gameObject.SetActive(false);
        }
        else
        {
            if (disableMovementUntilFinished)
            {
                InputManager.Instance.EnableMovementControls(true);
            }
            uiManager.DisableDialogueUI();
            OnDialogueEnd?.Invoke();
            InputManager.OnGetSpace -= ProgressDialogue;
        }
    }
    
}
