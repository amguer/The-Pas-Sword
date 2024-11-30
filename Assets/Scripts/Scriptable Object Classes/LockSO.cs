using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityEffect", menuName = "Scriptable Objects/Ability Effects/Lock", order = 1)]
public class LockSO : AbilityEffectSO
{
    [SerializeField] int duration;

    public override void Apply(List<Cell> targetCells)
    {
        GameManager gm = GameManager.Instance;
        Instigator instigator = gm.currentInstigator;
        int targetCellCount = targetCells.Count;

        for(int i = 0; i < targetCellCount; i++)
        {
            targetCells[i].Lock(duration);
        }
    }
}
