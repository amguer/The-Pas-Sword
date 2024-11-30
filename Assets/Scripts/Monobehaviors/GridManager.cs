using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public static Action OnAbilityPreview;
    public static Action OnAbilityActivate;
    public static Action OnAbilityCancel;

    // TEMPORARY VARIABLES
    private CellPatternSO cellPattern;
    private AbilityEffectSO abilityEffect;
    private AbilitySO currentAbility;
    [SerializeField] private Color defaultCellColor;
    [SerializeField] private Color defaultLetterColor;
    [SerializeField] private Color playerGuessedCellColor;
    [SerializeField] private Color playerGuessedLetterColor;
    [SerializeField] private Color enemyGuessedCellColor;
    [SerializeField] private Color enemyGuessedLetterColor;
    [SerializeField] private Color allGuessedCellColor;
    [SerializeField] private Color allGuessedLetterColor;

    [SerializeField] private RectTransform cellsParent;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Vector2 gridSize = new Vector2(6, 6);
    [SerializeField] private float distanceBetweenColumns = 40;
    [SerializeField] private float distanceBetweenRows = 50;
    private string gridChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private Cell selectedCell;

    public List<Cell> Cells => cells;
    private List<Cell> cells = new List<Cell>();
 
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

    private void Start()
    {
        InitializeGrid();
    }

    public void InitializeGrid()
    {
        if (cells.Count > 0)
        {
            // clear list of cells before instantiating new grid
            cells.Clear();
        }

        int rowsCount = (int)gridSize.y;
        int columnsCount = (int)gridSize.x;
        Vector2 startRectPosition;
        if(rowsCount % 2 == 0)
        {
            // if the amount of rows is even, make sure an even amount of rows are on both sides of the x-axis
            startRectPosition.y = (distanceBetweenRows / 2) + (distanceBetweenRows * (rowsCount/2) - 1);
        }
        else
        {
            // if the amount of rows is odd, make sure the middle row is centered on the x-axis
            startRectPosition.y = distanceBetweenRows * ((rowsCount - 1) / 2);
        }
        if(columnsCount % 2 == 0)
        {
            // if the amount of columns is even, make sure an even amount of columns are on both sides of the y-axis
            startRectPosition.x = -((distanceBetweenColumns / 2) + (distanceBetweenColumns * ((columnsCount / 2) - 1)));
        }
        else
        {
            // if the amount of columns is odd, make sure the middle column is centered on the y-axis
            startRectPosition.x = -distanceBetweenColumns * ((columnsCount - 1) / 2);
        }

        Cell newCell;
        Vector2 newRectPos = new Vector2();
        int j = 0;

        // iterate through grid positions, instantiating cells and initializing their values
        for (int h = 0; h < rowsCount; h++)
        {
            for (int i = 0; i < columnsCount; i++)
            {
                newCell = Instantiate(cellPrefab, cellsParent);
                newCell.gameObject.name = $"Cell {i}-{h}";
                newCell.InitializeCell(this);
                newCell.isInteractable = false;
                if (j > gridChars.Length)
                    newCell.AssignCharacter(string.Empty);
                else
                    newCell.AssignCharacter(gridChars[j++].ToString());
                newRectPos.x = startRectPosition.x + (i * distanceBetweenColumns);
                newRectPos.y = startRectPosition.y - (h * distanceBetweenRows);
                newCell.GetComponent<RectTransform>().anchoredPosition = newRectPos;
                newCell.SetGridPosition(i, h);
                newCell.InitializeColors(
                    defaultCellColor,
                    defaultLetterColor,
                    playerGuessedCellColor,
                    playerGuessedLetterColor,
                    enemyGuessedCellColor,
                    enemyGuessedLetterColor,
                    allGuessedCellColor,
                    allGuessedLetterColor
                    );
                newCell.SetColors(CellState.Default);
                newCell.currentCellState = CellState.Default;
                cells.Add(newCell);
            }
        }
    }

    public Cell GetCellAtGridPosition(Vector2 pos)
    {
        int cellsCount = cells.Count;
        for (int i = 0; i < cellsCount; i++)
        {
            if (cells[i].GridPosition == pos)
            {
                return cells[i];
            }
        }
        return null;
    }

    public void SetCellsInteractive(bool isInteractive)
    {
        int cellCount = cells.Count;
        for (int i = 0; i < cellCount; i++)
        {
            cells[i].isInteractable = isInteractive;
        }
    }

    public void SetSelectedCell(Cell selectedCell)
    {
        this.selectedCell = selectedCell;
    }

    public void PreviewAbility(AbilitySO ability)
    {
        cellPattern = ability.pattern;
        abilityEffect = ability.effect;
        currentAbility = ability;
        SetCellsInteractive(true);
        OnAbilityPreview?.Invoke();
    }

    public void ActivateAbility()
    {
        RevertTargetCells();
        List<Cell> targetCells = cellPattern.GetTargets(selectedCell.GridPosition, this);
        int targetCellCount = targetCells.Count;
        for (int i = 0; i < targetCellCount; i++)
        {
            if (targetCells[i] != null)
            {
                targetCells[i].GetComponent<Animator>().SetBool("CellShake", false);
            }
        }
        abilityEffect.Apply(cellPattern.GetTargets(selectedCell.GridPosition, this));
        SetCellsInteractive(false);
        GameManager.Instance.UpdatePlayerCP(-currentAbility.cost);
        OnAbilityActivate?.Invoke();
    }

    public void HighlightTargetCells()
    {
        List<Cell> targetCells = cellPattern.GetTargets(selectedCell.GridPosition, this);
        int targetCellCount = targetCells.Count;
        for (int i = 0; i < targetCellCount; i++)
        {
            if (targetCells[i] != null)
            {
                targetCells[i].AddHighlight(Instigator.Player);
                targetCells[i].GetComponent<Animator>().SetBool("CellShake", true);
            }
                
        }
    }

    public void RevertTargetCells()
    {
        if (selectedCell == null) return;

        List<Cell> targetCells = cellPattern.GetTargets(selectedCell.GridPosition, this);

        int targetCellCount = targetCells.Count;
        for (int i = 0; i < targetCellCount; i++)
        {
            if (targetCells[i] != null)
            {
                targetCells[i].SetColors(targetCells[i].currentCellState);
                targetCells[i].GetComponent<Animator>().SetBool("CellShake", false);
            }
                
        }
    }

    public void CancelAbilityPreview()
    {
        RevertTargetCells();
        cellPattern = null;
        abilityEffect = null;
        SetCellsInteractive(false);
        OnAbilityCancel?.Invoke();
    }

}
