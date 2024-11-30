using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCellPattern", menuName = "Scriptable Objects/Cell Patterns/Plus Sign", order = 1)]
public class PlusSignSO : CellPatternSO
{
    [SerializeField] int magnitude = 2;

    public override List<Cell> GetTargets(Vector2 gridPos, GridManager gridManager)
    {
        List<Cell> targetCells = new List<Cell>();
        Cell targetCell;
        Vector2 targetCellPos = new Vector2();

        targetCell = (gridManager.GetCellAtGridPosition(gridPos));
        if (targetCell != null && !targetCell.isLocked)
            targetCells.Add(targetCell);
       
        for(int i = 1; i < magnitude; i++)
        {
            targetCellPos.x = gridPos.x + i;
            targetCellPos.y = gridPos.y;
            targetCell = gridManager.GetCellAtGridPosition(targetCellPos);
            if (targetCell != null && !targetCell.isLocked)
                targetCells.Add(targetCell);

            targetCellPos.x = gridPos.x - i;
            targetCellPos.y = gridPos.y;
            targetCell = gridManager.GetCellAtGridPosition(targetCellPos);
            if (targetCell != null && !targetCell.isLocked)
                targetCells.Add(targetCell);

            targetCellPos.x = gridPos.x;
            targetCellPos.y = gridPos.y + i;
            targetCell = gridManager.GetCellAtGridPosition(targetCellPos);
            if (targetCell != null && !targetCell.isLocked)
                targetCells.Add(targetCell);

            targetCellPos.x = gridPos.x;
            targetCellPos.y = gridPos.y - i;
            targetCell = gridManager.GetCellAtGridPosition(targetCellPos);
            if (targetCell != null && !targetCell.isLocked)
                targetCells.Add(targetCell);
        }
        
        return targetCells;
    }
}
