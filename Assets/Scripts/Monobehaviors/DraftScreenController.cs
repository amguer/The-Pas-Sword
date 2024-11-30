using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DraftScreenController : MonoBehaviour
{
    [SerializeField] List<Card> draftOptions;

    public void Refresh()
    {
        DeckManager deckManager = DeckManager.Instance;
        AbilitySO newAbility = deckManager.GetAnyRandomAbility();
        for (int i = 0; i < draftOptions.Count; i++)
        {
            while (newAbility == draftOptions[i].assignedAbility)
            {
                newAbility = deckManager.GetAnyRandomAbility();
            }
            draftOptions[i].LoadAbility(newAbility);
        }
    }
}
