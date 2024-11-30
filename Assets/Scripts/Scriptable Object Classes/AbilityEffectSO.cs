using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityEffectSO : ScriptableObject
{
    public abstract void Apply(List<Cell> targetCells);
}
