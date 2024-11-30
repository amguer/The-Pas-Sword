using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityEffect", menuName = "Scriptable Objects/Ability Effects/Swap", order = 1)]
public class SwapSO : AbilityEffectSO
{
    public override void Apply(List<Cell> targetCells)
    {
        GameManager gm = GameManager.Instance;
        Instigator instigator = gm.currentInstigator;

        List<Cell> cells = targetCells;
        Cell cellOne;
        Cell cellTwo;
        Vector2 storedRectTransform;
        Vector2 storedGridPos;
        bool storedIsLocked;
        int storedIsLockedDuration;

        for (int i = 0; i < cells.Count; i++)
        {
            cellOne = cells[Random.Range(0, cells.Count)];
            cells.Remove(cellOne);

            cellTwo = cells[Random.Range(0, cells.Count)];
            cells.Remove(cellTwo);

            storedRectTransform = cellTwo.GetComponent<RectTransform>().anchoredPosition;
            storedGridPos = cellTwo.GridPosition;
            storedIsLocked = cellTwo.isLocked;
            storedIsLockedDuration = cellTwo.roundsLocked;

            cellTwo.GetComponent<RectTransform>().anchoredPosition = cellOne.GetComponent<RectTransform>().anchoredPosition;
            cellTwo.SetGridPosition(cellOne.GridPosition.x, cellOne.GridPosition.y);
            if (cellOne.isLocked && !cellTwo.isLocked)
            {
                cellTwo.Lock(cellOne.roundsLocked);
            }
            else if(!cellOne.isLocked && cellTwo.isLocked)
            {
                cellTwo.Unlock();
            }

            cellOne.GetComponent<RectTransform>().anchoredPosition = storedRectTransform;
            cellOne.SetGridPosition(storedGridPos.x, storedGridPos.y);
            if (storedIsLocked && !cellOne.isLocked)
            {
                cellOne.Lock(storedIsLockedDuration);
            }
            else if (cellOne.isLocked && !storedIsLocked)
            {
                cellOne.Unlock();
            }
        }
    }
}
