using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCellPattern", menuName = "Scriptable Objects/Cell Patterns/Compass", order = 1)]
public class CompassSO : CellPatternSO
{
    [SerializeField] int magnitudeNorth;
    [SerializeField] int magnitudeNortheast;
    [SerializeField] int magnitudeEast;
    [SerializeField] int magnitudeSoutheast;
    [SerializeField] int magnitudeSouth;
    [SerializeField] int magnitudeSouthwest;
    [SerializeField] int magnitudeWest;
    [SerializeField] int magnitudeNorthwest;

    public override List<Cell> GetTargets(Vector2 gridPos, GridManager gridManager)
    {
        List<Cell> targetCells = new List<Cell>();
        Cell targetCell;
        Vector2 targetCellPos = new Vector2();

        targetCell = gridManager.GetCellAtGridPosition(gridPos);
        if (canTargetLockedCells)
        {
            if (targetCell != null)
                targetCells.Add(targetCell);
        }
        else
        {
            if (targetCell != null && !targetCell.isLocked)
                targetCells.Add(targetCell);
        }

        for (int i = 0; i < magnitudeNorth; i++)
        {
            targetCellPos.x = gridPos.x;
            targetCellPos.y = gridPos.y + i;
            targetCell = gridManager.GetCellAtGridPosition(targetCellPos);
            if (canTargetLockedCells)
            {
                if (targetCell != null)
                    targetCells.Add(targetCell);
            }
            else
            {
                if (targetCell != null && !targetCell.isLocked)
                    targetCells.Add(targetCell);
            }
            
        }
        for (int i = 0; i < magnitudeNortheast; i++)
        {
            targetCellPos.x = gridPos.x + i;
            targetCellPos.y = gridPos.y + i;
            targetCell = gridManager.GetCellAtGridPosition(targetCellPos);
            if (canTargetLockedCells)
            {
                if (targetCell != null)
                    targetCells.Add(targetCell);
            }
            else
            {
                if (targetCell != null && !targetCell.isLocked)
                    targetCells.Add(targetCell);
            }
        }
        for (int i = 0; i < magnitudeEast; i++)
        {
            targetCellPos.x = gridPos.x + i;
            targetCellPos.y = gridPos.y;
            targetCell = gridManager.GetCellAtGridPosition(targetCellPos);
            if (canTargetLockedCells)
            {
                if (targetCell != null)
                    targetCells.Add(targetCell);
            }
            else
            {
                if (targetCell != null && !targetCell.isLocked)
                    targetCells.Add(targetCell);
            }
        }
        for (int i = 0; i < magnitudeSoutheast; i++)
        {
            targetCellPos.x = gridPos.x + i;
            targetCellPos.y = gridPos.y - i;
            targetCell = gridManager.GetCellAtGridPosition(targetCellPos);
            if (canTargetLockedCells)
            {
                if (targetCell != null)
                    targetCells.Add(targetCell);
            }
            else
            {
                if (targetCell != null && !targetCell.isLocked)
                    targetCells.Add(targetCell);
            }
        }
        for (int i = 0; i < magnitudeSouth; i++)
        {
            targetCellPos.x = gridPos.x;
            targetCellPos.y = gridPos.y - i;
            targetCell = gridManager.GetCellAtGridPosition(targetCellPos);
            if (canTargetLockedCells)
            {
                if (targetCell != null)
                    targetCells.Add(targetCell);
            }
            else
            {
                if (targetCell != null && !targetCell.isLocked)
                    targetCells.Add(targetCell);
            }
        }
        for (int i = 0; i < magnitudeSouthwest; i++)
        {
            targetCellPos.x = gridPos.x - i;
            targetCellPos.y = gridPos.y - i;
            targetCell = gridManager.GetCellAtGridPosition(targetCellPos);
            if (canTargetLockedCells)
            {
                if (targetCell != null)
                    targetCells.Add(targetCell);
            }
            else
            {
                if (targetCell != null && !targetCell.isLocked)
                    targetCells.Add(targetCell);
            }
        }
        for (int i = 0; i < magnitudeWest; i++)
        {
            targetCellPos.x = gridPos.x - i;
            targetCellPos.y = gridPos.y;
            targetCell = gridManager.GetCellAtGridPosition(targetCellPos);
            if (canTargetLockedCells)
            {
                if (targetCell != null)
                    targetCells.Add(targetCell);
            }
            else
            {
                if (targetCell != null && !targetCell.isLocked)
                    targetCells.Add(targetCell);
            }
        }
        for (int i = 0; i < magnitudeNorthwest; i++)
        {
            targetCellPos.x = gridPos.x - i;
            targetCellPos.y = gridPos.y + i;
            targetCell = gridManager.GetCellAtGridPosition(targetCellPos);
            if (canTargetLockedCells)
            {
                if (targetCell != null)
                    targetCells.Add(targetCell);
            }
            else
            {
                if (targetCell != null && !targetCell.isLocked)
                    targetCells.Add(targetCell);
            }
        }
    
        return targetCells;
    }
}
