using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroSequenceController : MonoBehaviour
{
    [SerializeField] GameObject combatScreen;
    [SerializeField] GameObject nameScreen;
    [SerializeField] TextMeshProUGUI playerNameInput;
    [SerializeField] TextMeshProUGUI noteText;
    [SerializeField] GameObject avatarScreen;
    [SerializeField] DraftScreenController draftScreen;
    [SerializeField] GameObject thinkingDots;
    [SerializeField] Image backdrop;
    [SerializeField] float fadeTime;
    bool fadeComplete = false;
    bool nameValid = false;
    bool pickedAvatar = false;
    bool pickedCard = false;
    private string gridChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";


    public void StartIntro()
    {
        StartCoroutine(IntroSequence());
    }

    private IEnumerator IntroSequence()
    {
        nameValid = false;
        pickedAvatar = false;
        pickedCard = false;
        nameScreen.SetActive(false);
        avatarScreen.SetActive(false);
        draftScreen.gameObject.SetActive(false);
        thinkingDots.SetActive(false);
        StartFadeOut();
        yield return new WaitUntil(() => fadeComplete == true);
        yield return new WaitForSeconds(2f);
        thinkingDots.SetActive(true);
        yield return new WaitForSeconds(3f);
        thinkingDots.SetActive(false);
        nameScreen.SetActive(true);
        yield return new WaitUntil(() => nameValid == true);
        nameScreen.SetActive(false);
        thinkingDots.SetActive(true);
        yield return new WaitForSeconds(3f);
        thinkingDots.SetActive(false);
        avatarScreen.SetActive(true);
        yield return new WaitUntil(() => pickedAvatar == true);
        avatarScreen.SetActive(false);
        thinkingDots.SetActive(true);
        yield return new WaitForSeconds(3f);
        thinkingDots.SetActive(false);
        draftScreen.gameObject.SetActive(true);
        draftScreen.Refresh();
        yield return new WaitUntil(() => DeckManager.Instance.PlayerDeck.Count > 2);
        draftScreen.Refresh();
        yield return new WaitUntil(() => DeckManager.Instance.PlayerDeck.Count > 5);
        draftScreen.Refresh();
        yield return new WaitUntil(() => DeckManager.Instance.PlayerDeck.Count > 8);
        draftScreen.gameObject.SetActive(false);
        combatScreen.SetActive(true);
        GameManager.Instance.NewBattle();
        StartFadeIn();
    }

    public void ChooseSprite(Sprite sprite)
    {
        UIManager.Instance.UpdatePlayerCharacterPortrait(sprite);
        pickedAvatar = true;
    }

    public void ValidatePlayerName()
    {
        string playerName = playerNameInput.text.ToUpper();
        int nameLength = playerNameInput.text.Length;

        if(nameLength <= 0)
        {
            noteText.text = "Invalid name. Letters and numbers only.";
            noteText.color = Color.red;
            return;
        }

        int gridCharsLength = gridChars.Length;
        int validCount = 0;

        for(int h = 0; h < nameLength - 1; h++)
        {
            for(int i = 0; i < gridCharsLength; i++)
            {
                if (playerName[h] == gridChars[i])
                {
                    validCount++;
                }
            }
        }

        if(validCount != nameLength - 1)
        {
            noteText.text = "Invalid name. Letters and numbers only.";
            noteText.color = Color.red;
        }
        else
        {
            nameValid = true;
            GameManager.Instance.SetPlayerName(playerName);
        }
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        fadeComplete = false;
        float a = 0;
        Color newColor;
        float fadeTime = this.fadeTime / 100;
        while(backdrop.color.a < 1)
        {
            a += 0.01f;
            newColor = new Color(backdrop.color.r, backdrop.color.g, backdrop.color.b, a);
            backdrop.color = newColor;
            yield return new WaitForSeconds(fadeTime);
        }
        fadeComplete = true;
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        fadeComplete = false;
        float a = 1;
        Color newColor;
        float fadeTime = this.fadeTime / 100;
        while (backdrop.color.a > 0)
        {
            a -= 0.01f;
            newColor = new Color(backdrop.color.r, backdrop.color.g, backdrop.color.b, a);
            backdrop.color = newColor;
            yield return new WaitForSeconds(fadeTime);
        }
        fadeComplete = true;
    }
}
