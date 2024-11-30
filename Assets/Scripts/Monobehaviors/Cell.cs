
using System.Collections;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    
    [SerializeField] private Image cellBackdrop;
    [SerializeField] private Image borderHL;
    [SerializeField] private RectTransform lockedBorder;
    [SerializeField] private TextMeshProUGUI charTextBox;

    private Color defaultCellColor;
    private Color defaultCharColor;
    private Color playerGuessedCellColor;
    private Color playerGuessedCharColor;
    private Color enemyGuessedCellColor;
    private Color enemyGuessedCharColor;
    private Color allGuessedCellColor;
    private Color allGuessedCharColor;
    private Vector2 gridPosition;
    private string assignedChar;
    private GridManager gridManager;
    public int roundsLocked { get; private set; }

    public Vector2 GridPosition => gridPosition;
    public string AssignedChar => assignedChar;

    public bool isLocked { get; private set; }
    public bool isTrapped { get; private set; }
    [HideInInspector] public bool isInteractable;
    public CellState currentCellState;
    public CellState highlightedCellState;

    public void InitializeCell(GridManager gridManager)
    {
        this.gridManager = gridManager;
        borderHL.gameObject.SetActive(false);
        lockedBorder.gameObject.SetActive(false);
        roundsLocked = 0;
    }

    public void SetGridPosition(float x, float y)
    {
        gridPosition.x = x;
        gridPosition.y = y;
    }

    public void AssignCharacter(string character)
    {
        assignedChar = character;
        charTextBox.text = character;
    }

    public void InitializeColors(Color defaultCell, Color defaultChar, Color playerCell, Color playerChar, Color enemyCell, Color enemyChar, Color allCell, Color allChar)
    {
        defaultCellColor = defaultCell;
        defaultCharColor = defaultChar;
        playerGuessedCellColor = playerCell;
        playerGuessedCharColor = playerChar;
        enemyGuessedCellColor = enemyCell;
        enemyGuessedCharColor = enemyChar;
        allGuessedCellColor = allCell;
        allGuessedCharColor = allChar;
    }

    public void SetColors(CellState state)
    {
        switch(state)
        {
            case CellState.Default:
                cellBackdrop.color = defaultCellColor;
                charTextBox.color = defaultCharColor;
                break;
            case CellState.PlayerGuessed:
                cellBackdrop.color = playerGuessedCellColor;
                charTextBox.color = playerGuessedCharColor;
                break;
            case CellState.EnemyGuessed:
                cellBackdrop.color = enemyGuessedCellColor;
                charTextBox.color = enemyGuessedCharColor;
                break;
            case CellState.AllGuessed:
                cellBackdrop.color = allGuessedCellColor;
                charTextBox.color = allGuessedCharColor;
                break;
        }
    }

    public void AddHighlight(Instigator instigator)
    {
        if(instigator == Instigator.Player)
        {
            switch (currentCellState)
            {
                case CellState.Default:
                    cellBackdrop.color = playerGuessedCellColor;
                    charTextBox.color = playerGuessedCharColor;
                    highlightedCellState = CellState.PlayerGuessed;
                    break;
                case CellState.PlayerGuessed:
                    cellBackdrop.color = playerGuessedCellColor;
                    charTextBox.color = playerGuessedCharColor;
                    highlightedCellState = CellState.PlayerGuessed;
                    break;
                case CellState.EnemyGuessed:
                    cellBackdrop.color = allGuessedCellColor;
                    charTextBox.color = allGuessedCharColor;
                    highlightedCellState = CellState.AllGuessed;
                    break;
                case CellState.AllGuessed:
                    cellBackdrop.color = allGuessedCellColor;
                    charTextBox.color = allGuessedCharColor;
                    highlightedCellState = CellState.AllGuessed;
                    break;
            }
        }
        else
        {
            switch (currentCellState)
            {
                case CellState.Default:
                    cellBackdrop.color = enemyGuessedCellColor;
                    charTextBox.color = enemyGuessedCharColor;
                    highlightedCellState = CellState.EnemyGuessed;
                    break;
                case CellState.PlayerGuessed:
                    cellBackdrop.color = allGuessedCellColor;
                    charTextBox.color = allGuessedCharColor;
                    highlightedCellState = CellState.AllGuessed;
                    break;
                case CellState.EnemyGuessed:
                    cellBackdrop.color = enemyGuessedCellColor;
                    charTextBox.color = enemyGuessedCharColor;
                    highlightedCellState = CellState.EnemyGuessed;
                    break;
                case CellState.AllGuessed:
                    cellBackdrop.color = allGuessedCellColor;
                    charTextBox.color = allGuessedCharColor;
                    highlightedCellState = CellState.AllGuessed;
                    break;
            }
        }
    }

    public void Lock(int duration)
    {
        isLocked = true;
        lockedBorder.gameObject.SetActive(true);
        roundsLocked = duration;
        GameManager.OnEndTurn += TickLock;
    }

    public void Unlock()
    {
        roundsLocked = 0;
        isLocked = false;
        lockedBorder.gameObject.SetActive(false);
        GameManager.OnEndTurn -= TickLock;
    }

    private void TickLock()
    {
        roundsLocked--;
        if(roundsLocked <= 0)
        {
            Unlock();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isInteractable)
        {
            gridManager.SetSelectedCell(this);
            gridManager.HighlightTargetCells();
        }
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isInteractable)
        {
            gridManager.ActivateAbility();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isInteractable)
        {
            gridManager.RevertTargetCells();
        }
    }
}
