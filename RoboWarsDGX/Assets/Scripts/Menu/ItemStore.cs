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

    [Header("Character data:")]
    [SerializeField]
    private StoreCharacter storeCharacter;

    private List<StoreRune> storeRunes = new List<StoreRune>();

    private List<StoreCharacter> storeCharacters = new List<StoreCharacter>();

    void Start()
    {
        switch (type)
        {
            case StoreType.RuneStore:
                AccountInfo.Instance.SuccessRuneBuyingEvent += RuneStoreRefresh;
                break;
            case StoreType.CharacterStore:
                AccountInfo.Instance.SuccessCharacterBuyingEvent += CharacterStoreRefresh;
                break;
            default:
                break;
        }
        Init();
    }

    public void Init()
    {
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
            storeRunes.Add(newStoreRune);
            newStoreRune.Rune = runes[i];
            newStoreRune.InitRune();
        }
    }

    private void CharacterStoreInit()
    {
        List<Character> characters = AccountInfo.Instance.GetStoreCharacters();
        if (characters.Count == 0)
        {
            noItemText.SetActive(true);
            return;
        }
        else
        {
            noItemText.SetActive(false);
        }
        
        for (int i = 0; i < characters.Count; i++)
        {
            GameObject storeCharacterGO = Instantiate(storeCharacter.gameObject, parent);
            StoreCharacter newStoreCharacter = storeCharacterGO.GetComponent<StoreCharacter>();
            storeCharacters.Add(newStoreCharacter);
            newStoreCharacter.Character = characters[i];
            newStoreCharacter.InitCharacter();
        }
    }

    private void RuneStoreRefresh(string id)
    {
        for(int i=0; i<storeRunes.Count; i++)
        {
            storeRunes[i].Refresh(id);
        }
    }

    private void CharacterStoreRefresh(string id)
    {
        for (int i = 0; i < storeCharacters.Count; i++)
        {
            storeCharacters[i].Refresh(id);
        }
    }
}
