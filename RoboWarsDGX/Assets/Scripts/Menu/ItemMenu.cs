using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    [Header("Shared data")]
    [SerializeField]
    private GameObject noItemText;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private MenuType menuType = MenuType.Rune;
    [Header("Store data")]
    [SerializeField]
    private StoreRune storeRune;
    [Header("Character data")]
    [SerializeField]
    private StoreCharacter storeCharacter;

    public void Init()
    {
        if (parent.childCount != 0)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }

        switch (menuType)
        {
            case MenuType.Rune:
                InitRuneMenu();
                break;
            case MenuType.Character:
                InitCharacterMenu();
                break;
            default:
                break;
        }
    }

    private void InitRuneMenu()
    {
        List<Rune> runes = AccountInfo.Instance.ownRunes;
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
            newStoreRune.inStore = false;
            newStoreRune.InitRune();
        }
    }

    private void InitCharacterMenu()
    {
        List<Character> characters = AccountInfo.Instance.ownCharacters;
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
            newStoreCharacter.Character = characters[i];
            newStoreCharacter.storeCharacter = false;
            newStoreCharacter.InitCharacter();
        }
    }

    private void OnEnable()
    {
        Init();
    }

    private enum MenuType
    {
        Rune, Character, Weapon
    }
}
