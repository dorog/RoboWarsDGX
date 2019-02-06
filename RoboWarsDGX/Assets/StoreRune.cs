using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreRune : MonoBehaviour
{
    private int maxAbility = 5;

    [SerializeField]
    private Transform abilityParent;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private RuneStoreAbility goodAbility;

    [SerializeField]
    private RuneStoreAbility badAbility;

    public Rune Rune { get; set; } = null;

    public void InitRune()
    {
        if (Rune != null)
        {
            icon.sprite = Rune.icon;

            List<RunStoreDescription> descriptions = Rune.GetData();
            for(int i=0; i< descriptions.Count; i++)
            {
                if (descriptions[i].isGood)
                {
                    AddAbility(goodAbility, descriptions[i].displayName, descriptions[i].displayAmount);
                }
                else
                {
                    AddAbility(badAbility, descriptions[i].displayName, descriptions[i].displayAmount);
                }
            }
        }
    }

    private void AddAbility(RuneStoreAbility runeStoreAbility, string name, string amount)
    {
        GameObject runeStore = Instantiate(runeStoreAbility.gameObject, abilityParent);
        RuneStoreAbility runeStoreAbilityScript = runeStore.GetComponent<RuneStoreAbility>();
        runeStoreAbilityScript.Init(name, amount);
    }
}
