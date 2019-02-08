using System.Collections.Generic;
using UnityEngine;

public class ItemStore : MonoBehaviour
{
    [Header ("Shared data:")]
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private GameObject noItemText;
    [SerializeField]
    private StoreType type;

    [Header("Rune data:")]
    [SerializeField]
    private StoreRune storeRune;

    /*[Header("Character data:")]
    [SerializeField]
    private StoreCharacter storeCharacter;*/

    void Start()
    {
        Init();
    }

    public void Init()
    {
        /*if (parent.childCount != 0)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }*/

        switch (type)
        {
            case StoreType.RuneStore:
                RuneStoreInit();
                break;
            case StoreType.CharacterStore:
                CharacterStoreInit();
                break;
            default:
                break;
        }
    }

    private void RuneStoreInit()
    {
        List<Rune> runes = AccountInfo.Instance.GetStoreRunes();
        if (runes.Count == 0)
        {
            noItemText.SetActive(true);
            return;
        }
        else
        {
            noItemText.SetActive(false);
        }
        for (int i = 0; i < runes.Count; i++)
        {
            GameObject storeRuneGO = Instantiate(storeRune.gameObject, parent);
            StoreRune newStoreRune = storeRuneGO.GetComponent<StoreRune>();
            newStoreRune.Rune = runes[i];
            newStoreRune.InitRune();
        }
    }

    private void CharacterStoreInit()
    {
        /*if (Database.Instance.NotOwnedCharacterStoreItems.Count == 0)
        {
            noItemText.SetActive(true);
            return;
        }
        for (int i = 0; i < Database.Instance.NotOwnedCharacterStoreItems.Count; i++)
        {
            /*Rune newRune = SharedData.CreateRuneFromStoreItem(Database.Instance.NotOwnedRuneStoreItems[i]);
            GameObject storeRuneGO = Instantiate(storeRune.gameObject, parent);
            StoreRune newStoreRune = storeRuneGO.GetComponent<StoreRune>();
            newStoreRune.Rune = newRune;
            newStoreRune.InitRune();
        }*/
    }
}
