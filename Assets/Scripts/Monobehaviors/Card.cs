
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private DeckManager deckManager;
    public AbilitySO assignedAbility { get; private set; }
    private RectTransform rectTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private float hoverScale;
    [SerializeField] private float hoverDistance;
    [SerializeField] private TextMeshProUGUI nameTextBox;
    [SerializeField] private TextMeshProUGUI descriptionTextBox;
    [SerializeField] private TextMeshProUGUI cpCostTextBox;
    [SerializeField] private TextMeshProUGUI abilityTypeTextBox;
    [SerializeField] private Image iconImage;
    [SerializeField] private Image patternTypeImage;
    [SerializeField] private Image highlightImage;
    private int siblingIndex;
    public bool isInteractable;

    public void Initialize(DeckManager deckManager)
    {
        this.deckManager = deckManager;
        rectTransform = GetComponent<RectTransform>();
        siblingIndex = transform.GetSiblingIndex();
        isInteractable = true;
        if(rectTransform == null)
        {
            Debug.Log("Card has no RectTransform component attached and could not initialize.");
            return;
        }
                    
    }

    public void LoadAbility(AbilitySO ability)
    {
        assignedAbility = ability;
        nameTextBox.text = ability.abilityName;
        descriptionTextBox.text = ability.description;
        cpCostTextBox.text = ability.cost.ToString();
        //Uncomment these when they are implemented;
        abilityTypeTextBox.text = ability.type.name;
        //patternTypeImage.sprite = ability.pattern.spriteIcon;     
        //iconImage.sprite = ability.abilityIcon;    
    }

    private void Hover()
    {
        rectTransform.localScale *= hoverScale;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + hoverDistance);
        highlightImage.gameObject.SetActive(true);
        transform.SetAsLastSibling();
        
    }

    private void Hide()
    {
        rectTransform.localScale /= hoverScale;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - hoverDistance);
        highlightImage.gameObject.SetActive(false);
        transform.SetSiblingIndex(siblingIndex);
    }

    private void Discard()
    {
        GridManager.OnAbilityActivate -= Discard;
        GridManager.OnAbilityCancel -= Cancel;
        rectTransform.localScale = Vector3.one;
        highlightImage.gameObject.SetActive(false);
        transform.SetSiblingIndex(siblingIndex);
        deckManager.Discard(this);
    }

    private void Cancel()
    {
        animator.SetBool("CardShake", false);
        Hide();
        GridManager.OnAbilityActivate -= Discard;
        GridManager.OnAbilityCancel -= Cancel;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInteractable) return;

        Hover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isInteractable || GameManager.Instance.playerCP < assignedAbility.cost || GameManager.Instance.currentInstigator != Instigator.Player) return;

        animator.SetBool("CardShake", true);
        GridManager.Instance.PreviewAbility(assignedAbility);
        GridManager.OnAbilityActivate += Discard;
        GridManager.OnAbilityCancel += Cancel;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isInteractable) return;

        Hide();
    }
}
