
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI DialogueBox => dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueBox;

    public TextMeshProUGUI TitleBox => titleBox;
    [SerializeField] private TextMeshProUGUI titleBox;

    public Image CharacterPortrait => characterPortrait;
    [SerializeField] private Image characterPortrait;

    public Image ProgressArrow => progressArrow;
    [SerializeField] private Image progressArrow;

    [SerializeField] private GameObject dialogueBoxBackDrop;

    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerCPText;
    [SerializeField] private TextMeshProUGUI enemyNameText;
    [SerializeField] private TextMeshProUGUI enemyCPText;
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI directionText;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button continueButton;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void OnEnable()
    {
        GameManager.OnUpdateRound += UpdateRound;
        GameManager.OnUpdatePlayerCP += UpdatePlayerCP;
        GameManager.OnUpdateEnemyCP += UpdateEnemyCP;
        GameManager.OnUpdateEnemyDisplayName += UpdateEnemyDisplayName;
        GameManager.OnUpdatePlayerDisplayName += UpdatePlayerDisplayName;
        GameManager.OnWin += ShowWinUI;
        GameManager.OnLose += ShowLoseUI;

        GridManager.OnAbilityPreview += EnableAbilityPreviewUI;
        GridManager.OnAbilityActivate += DisableAbilityPreviewUI;
        GridManager.OnAbilityCancel += DisableAbilityPreviewUI;
    }

    private void OnDisable()
    {
        GameManager.OnUpdateRound -= UpdateRound;
        GameManager.OnUpdatePlayerCP -= UpdatePlayerCP;
        GameManager.OnUpdateEnemyCP -= UpdateEnemyCP;
        GameManager.OnUpdateEnemyDisplayName -= UpdateEnemyDisplayName;
        GameManager.OnUpdatePlayerDisplayName -= UpdatePlayerDisplayName;
        GameManager.OnWin -= ShowWinUI;
        GameManager.OnLose -= ShowLoseUI;


        GridManager.OnAbilityPreview -= EnableAbilityPreviewUI;
        GridManager.OnAbilityActivate -= DisableAbilityPreviewUI;
        GridManager.OnAbilityCancel -= DisableAbilityPreviewUI;
    }

    private void Start()
    {
        dialogueBoxBackDrop.SetActive(false); 
        restartButton.gameObject.SetActive(false); 
        continueButton.gameObject.SetActive(false);
    }

    public void EnableDialogueUI()
    {
        dialogueBoxBackDrop.SetActive(true);
    }

    public void DisableDialogueUI()
    {
        dialogueBoxBackDrop.SetActive(false);
    }

    private void EnableAbilityPreviewUI()
    {
        directionText.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
    }

    private void DisableAbilityPreviewUI()
    {
        directionText.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }

    private void UpdateRound(int num)
    {
        roundText.text = $"Round {num}";
    }

    private void UpdatePlayerDisplayName(string name)
    {
        playerNameText.text = name;
    }

    private void UpdateEnemyDisplayName(string name)
    {
        enemyNameText.text = name;
    }

    private void UpdatePlayerCP(int i)
    {
        playerCPText.text = i.ToString();
    }

    private void UpdateEnemyCP(int i)
    {
        enemyCPText.text = i.ToString();
    }

    private void ShowLoseUI()
    {
        roundText.text = "Defeat!";
        restartButton.gameObject.SetActive(true);
    }

    private void ShowWinUI()
    {
        roundText.text = "Victory!";
        continueButton.gameObject.SetActive(true);
    }
}
