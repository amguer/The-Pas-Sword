using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Scriptable Objects/New Ability", order = 1)]
public class AbilitySO : ScriptableObject
{
    public string abilityName;
    public int cost;
    public int tier;
    public AbilityTypeSO type;
    public Sprite icon;
    public CellPatternSO pattern;
    public AbilityEffectSO effect;
    [TextArea] public string description;

}
