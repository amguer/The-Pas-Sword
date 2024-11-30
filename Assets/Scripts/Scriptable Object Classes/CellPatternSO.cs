using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CellPatternSO : ScriptableObject
{
    public bool canTargetLockedCells;
    public abstract List<Cell> GetTargets(Vector2 gridPos, GridManager gridManager);
}
