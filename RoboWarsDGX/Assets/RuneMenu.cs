using System.Collections.Generic;
using UnityEngine;

public class RuneMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject noRuneText;
    [SerializeField]
    private Transform runeParent;
    [SerializeField]
    private StoreRune storeRune;

    public void Init()
    {
        List<Rune> runes = AccountInfo.Instance.ownRunes;
        if (runeParent.childCount != 0)
        {
            for (int i = runeParent.childCount - 1; i >= 0; i--)
            {
                Destroy(runeParent.GetChild(i).gameObject);
            }
        }
        if (runes.Count==0)
        {
            noRuneText.SetActive(true);
            return;
        }
        else
        {
            noRuneText.SetActive(false);
        }
        for (int i = 0; i < runes.Count; i++)
        {
            GameObject storeRuneGO = Instantiate(storeRune.gameObject, runeParent);
            StoreRune newStoreRune = storeRuneGO.GetComponent<StoreRune>();
            newStoreRune.Rune = runes[i];
            newStoreRune.inStore = false;
            newStoreRune.InitRune();
        }
    }

    private void OnEnable()
    {
        Init();
    }
}
