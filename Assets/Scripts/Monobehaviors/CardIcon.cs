using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
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
    public bool isInteractable;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInteractable) return;

        animator.SetBool("CardShake", true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isInteractable) return;

        DeckManager.Instance.AddToDeck(assignedAbility, 3);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isInteractable) return;

        animator.SetBool("CardShake", false);
    }
}
