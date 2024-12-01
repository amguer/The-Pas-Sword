using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DraftScreenController : MonoBehaviour
{
    [SerializeField] List<CardIcon> draftOptions;
    public void Refresh()
    {
        DeckManager deckManager = DeckManager.Instance;
        AbilitySO newAbility;
        for (int i = 0; i < draftOptions.Count; i++)
        {
            newAbility = deckManager.GetAnyRandomAbility();

            while (newAbility == draftOptions[i].assignedAbility)
            {
                newAbility = deckManager.GetAnyRandomAbility();
            }
            draftOptions[i].LoadAbility(newAbility);
        }
    }
}
