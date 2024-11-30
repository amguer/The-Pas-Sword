using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;

    [Header("Player Deck")]
    [SerializeField] int defaultDeckSize;
    [SerializeField] int maxDeckSize;
    [SerializeField] int minDeckSize;

    [Header("Player Hand")]
    [SerializeField] int defaultHandSize;
    [SerializeField] int maxHandSize;
    [SerializeField] int defaultDrawCost;
    [SerializeField] int drawCostMultipler;
    public int currentDrawCost { get; private set; }

    [Header("All Abilities")]
    [SerializeField] List<AbilitySO> allAbilities = new List<AbilitySO>();
    [SerializeField] private List<AbilitySO> playerDeck = new List<AbilitySO>();
    private List<AbilitySO> playerHand = new List<AbilitySO>();
    private List<AbilitySO> playerDiscardPile = new List<AbilitySO>();
    public List<AbilitySO> AllAbilities => allAbilities;

    [Header("Cards")]
    [SerializeField] private Card cardPrefab;
    [SerializeField] private RectTransform cardsParent;
    [SerializeField] private float distanceBetweenCards;
    [SerializeField] private float handPixelHeight;
    [SerializeField] private float handCurveAngle ;

    
    private Card[] hand;

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
        GridManager.OnAbilityPreview += LockHandInteraction;
        GridManager.OnAbilityActivate += UnlockhandInteraction;
        GridManager.OnAbilityCancel += UnlockhandInteraction;

        GameManager.OnEndTurn += DrawNewHand;
    }

    private void OnDisable()
    {
        GridManager.OnAbilityPreview -= LockHandInteraction;
        GridManager.OnAbilityActivate -= UnlockhandInteraction;
        GridManager.OnAbilityCancel -= UnlockhandInteraction;

        GameManager.OnEndTurn -= DrawNewHand;
    }

    private void Start()
    {
        InitializeHand();
        DrawNewHand();
    }

    // Subscribe this to GameManager.OnBeginRound when it is implemented
    private void InitializeHand()
    {
        Card newCard;
        hand = new Card[maxHandSize];

        for(int i = 0; i < maxHandSize; i++)
        {
            newCard = Instantiate(cardPrefab, cardsParent);
            newCard.Initialize(this);
            newCard.gameObject.SetActive(false);
            hand[i] = newCard;
        }
    }

    private Card GetCard()
    {
        for(int i = 0; i < maxHandSize; i++)
        {
            if (hand[i].gameObject.activeInHierarchy == false)
            {
                return hand[i];
            }
        }
        Debug.Log("No card available in pool. Returned null.");
        return null;
    }

    private void UpdateCardPositions()
    {
        // Calculate how many cards are active in the hierarchy so RectTransfroms can be evenly distributed
        List<Card> activeCards = new List<Card>();
        for(int i = 0; i < maxHandSize; i++)
        {
            if (hand[i].gameObject.activeInHierarchy == true)
            {
                activeCards.Add(hand[i]);
            }
        }

        // Distribute hand space evenly across active cards
        Vector2 newRectPos;
        int currentHandCount = activeCards.Count;
        float handPixelWidth = currentHandCount * distanceBetweenCards;
        for (int i = 0; i < currentHandCount; i++)
        {
            newRectPos.x = (handPixelWidth / currentHandCount * i) - handPixelWidth / 2;
            newRectPos.y = handPixelHeight;
            activeCards[i].GetComponent<RectTransform>().anchoredPosition = newRectPos;
        }
    }

    private void LockHandInteraction()
    {
        SetHandInteractable(false);
    }

    private void UnlockhandInteraction()
    {
        SetHandInteractable(true);
    }

    public void SetHandInteractable(bool isInteractable)
    {
        for(int i = 0; i < maxHandSize; i++)
        {
            hand[i].isInteractable = isInteractable;
        }
    }

    public AbilitySO GetAnyRandomAbility()
    {
        return allAbilities[Random.Range(0, allAbilities.Count)];
    }

    public void DrawNewHand()
    {
        for(int i = 0; i < maxHandSize; i++)
        {
            if (hand[i].gameObject.activeInHierarchy == true)
            {
                Discard(hand[i]);
            }
        }

        for (int i = 0; i < defaultHandSize; i++)
        {
            Draw();
        }
    }

    public void Draw()
    {
        if (playerHand.Count > 6)
        {
            Debug.Log("Hand full!");
            return;
        }

        if (playerDeck.Count < 1)
        {
            RecycleDeck();
        }

        AbilitySO newAbility = playerDeck[Random.Range(0, playerDeck.Count)];
        playerDeck.Remove(newAbility);
        playerHand.Add(newAbility);
        Card newCard = GetCard();
        if(newCard != null)
        {
            newCard.LoadAbility(newAbility);
            newCard.gameObject.SetActive(true);
            UpdateCardPositions();
        }
    }

    public void Discard(Card card)
    {
        card.gameObject.SetActive(false);
        UpdateCardPositions();
        playerHand.Remove(card.assignedAbility);
        playerDiscardPile.Add(card.assignedAbility);
    }

    private void RecycleDeck()
    {
        AbilitySO randomDiscardedAbility;
        for (int i = 0; i < playerDiscardPile.Count; i++)
        {
            randomDiscardedAbility = playerDiscardPile[Random.Range(0, playerDiscardPile.Count)];
            playerDiscardPile.Remove(randomDiscardedAbility);
            playerDeck.Add(randomDiscardedAbility);
        }
    }

    

}
