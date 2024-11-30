using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityEffect", menuName = "Scriptable Objects/Ability Effects/Search", order = 1)]
public class SearchSO : AbilityEffectSO
{
    public override void Apply(List<Cell> targetCells)
    {
        GameManager gm = GameManager.Instance;
        if (gm == null) return;

        Instigator instigator = gm.currentInstigator;

        string opponentName = string.Empty;
        string opponentDisplayName = string.Empty;
        if(instigator == Instigator.Player)
        {
            opponentDisplayName = gm.enemyDisplayName;
            opponentName = gm.fullEnemyName;
        }
        else
        {
            opponentDisplayName = gm.playerDisplayName;
            opponentName = gm.fullPlayerName;
        }
        int opponentNameLength = opponentDisplayName.Length;
        int targetCellsCount = targetCells.Count;
        string[] opponentDisplayChars = new string[opponentNameLength];
        for(int i = 0; i < opponentNameLength; i++)
        {
            opponentDisplayChars[i] = opponentDisplayName[i].ToString();
        }

        for (int h = 0; h < targetCellsCount; h++)
        {
            targetCells[h].AddHighlight(instigator);
            targetCells[h].currentCellState = targetCells[h].highlightedCellState;
            targetCells[h].SetColors(targetCells[h].currentCellState);

            for (int i = 0; i < opponentNameLength; i++)
            {
                if (targetCells[h].AssignedChar == opponentName[i].ToString())
                {
                    opponentDisplayChars[i] = targetCells[h].AssignedChar;
                }
            }
        }

        string newOpponentDisplayName = string.Empty;
        for (int i = 0; i < opponentNameLength; i++)
        {
            newOpponentDisplayName += opponentDisplayChars[i];
        }
        
        if(instigator == Instigator.Player)
        {
            gm.UpdateEnemyDisplayName(newOpponentDisplayName);
        }
        else
        {
            gm.UpdatePlayerDisplayName(newOpponentDisplayName);
        }

    }
}
