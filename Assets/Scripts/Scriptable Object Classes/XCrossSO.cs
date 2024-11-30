using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCellPattern", menuName = "Scriptable Objects/Cell Patterns/X Cross", order = 1)]
public class XCrosSSO : CellPatternSO
{
    [SerializeField] int magnitude = 2;

    public override List<Cell> GetTargets(Vector2 gridPos, GridManager gridManager)
    {
        List<Cell> targetCells = new List<Cell>();
        Vector2 targetCellPos = new Vector2();
        Cell targetCell;

        targetCell = gridManager.GetCellAtGridPosition(gridPos);
        if (targetCell != null && !targetCell.isLocked)
            targetCells.Add(targetCell);

        for (int i = 1; i < magnitude; i++)
        {
            targetCellPos.x = gridPos.x + i;
            targetCellPos.y = gridPos.y + i;
            targetCell = gridManager.GetCellAtGridPosition(targetCellPos);
            if(targetCell != null && !targetCell.isLocked)
                targetCells.Add(targetCell);

            targetCellPos.x = gridPos.x + i;
            targetCellPos.y = gridPos.y - i;
            targetCell = gridManager.GetCellAtGridPosition(targetCellPos);
            if (targetCell != null && !targetCell.isLocked)
                targetCells.Add(targetCell);

            targetCellPos.x = gridPos.x - i;
            targetCellPos.y = gridPos.y + i;
            targetCell = gridManager.GetCellAtGridPosition(targetCellPos);
            if (targetCell != null && !targetCell.isLocked)
                targetCells.Add(targetCell);

            targetCellPos.x = gridPos.x - i;
            targetCellPos.y = gridPos.y - i;
            targetCell = gridManager.GetCellAtGridPosition(targetCellPos);
            if (targetCell != null && !targetCell.isLocked)
                targetCells.Add(targetCell);
        }

        return targetCells;
    }
}
