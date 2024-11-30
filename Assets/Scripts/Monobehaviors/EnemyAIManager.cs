using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIManager : MonoBehaviour
{
    [SerializeField] int level; // AI's ability variety is 2x their level
    private List<AbilitySO> hand = new List<AbilitySO>();

    private void OnEnable()
    {
        GameManager.OnBeginGame += Initialize;
    }

    private void OnDisable()
    {
        GameManager.OnBeginGame -= Initialize;
    }

    public void Initialize()
    {
        int numOfAbilities = level * 2;
        DeckManager deckManager = DeckManager.Instance;
        for(int i = 0; i < numOfAbilities; i++)
        {
            AbilitySO newAbility = deckManager.GetAnyRandomAbility();
            for (int j = 0; j < hand.Count; j++)
            {
                while(newAbility == hand[j])
                {
                    newAbility = deckManager.GetAnyRandomAbility();
                }
            }
            hand.Add(newAbility);
            Debug.Log(newAbility.name);
        }
    }

    public void Play()
    {
        Invoke("PlayRandomAbility", Random.Range(2, 4));
    }

    private void PlayRandomAbility()
    {
        // Find valid cell targets and playable abilities
        List<Cell> validCells = GetValidCells();
        List<AbilitySO> validAbilities = GetValidAbilities();
        if(validCells.Count < 1 || validAbilities.Count < 1)
        {
            GameManager.Instance.EndTurn();
        }

        // Get a random valid cell position and ability
        Vector2 randomCellPos = validCells[Random.Range(0, validCells.Count)].GridPosition;
        Debug.Log(validAbilities.Count);
        AbilitySO randomAbility = validAbilities[Random.Range(0, validAbilities.Count )];

        // Use ability at cell position
        List<Cell> targets = randomAbility.pattern.GetTargets(randomCellPos, GridManager.Instance);
        randomAbility.effect.Apply(targets);

        // Update CP. If enough CP for another ability, keep playing.
        GameManager.Instance.UpdateEnemyCP(-randomAbility.cost);
        if(GetValidAbilities().Count > 0)
        {
            Invoke("PlayRandomAbility", Random.Range(2, 4));
        }
        else
        {
            GameManager.Instance.EndTurn();
        }
    }

    private List<AbilitySO> GetValidAbilities()
    {
        int currentCP = GameManager.Instance.enemyCP;
        List<AbilitySO> validAbilities = new List<AbilitySO>();
        int handCount = hand.Count;
        for(int i = 0; i < handCount; i++)
        {
            if (hand[i].cost < currentCP)
            {
                validAbilities.Add(hand[i]);
            }
        }
        return validAbilities;
    }

    private List<Cell> GetValidCells()
    {
        List<Cell> cells = GridManager.Instance.Cells;
        List<Cell> validCells = new List<Cell>();
        int cellCount = cells.Count;

        for (int i = 0; i < cellCount; i++)
        {
            if (cells[i].currentCellState != CellState.EnemyGuessed && cells[i].currentCellState != CellState.AllGuessed && !cells[i].isLocked)
            {
                validCells.Add(cells[i]);
            }
        }

        return validCells;
    }
}
