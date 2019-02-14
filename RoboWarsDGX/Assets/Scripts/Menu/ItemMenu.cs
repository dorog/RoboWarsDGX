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

    public GameObject NoItemText { get => noItemText; set => noItemText = value; }
    public Transform Parent { get => parent; set => parent = value; }
    private MenuType MenuType1 { get => menuType; set => menuType = value; }
    public StoreRune StoreRune { get => storeRune; set => storeRune = value; }
    public StoreCharacter StoreCharacter { get => storeCharacter; set => storeCharacter = value; }

    public void Init()
    {
        if (Parent.childCount != 0)
        {
            for (int i = Parent.childCount - 1; i >= 0; i--)
            {
                Destroy(Parent.GetChild(i).gameObject);
            }
        }

        switch (MenuType1)
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
            NoItemText.SetActive(true);
            return;
        }
        else
        {
            NoItemText.SetActive(false);
        }
        for (int i = 0; i < runes.Count; i++)
        {
            GameObject storeRuneGO = Instantiate(StoreRune.gameObject, Parent);
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
            NoItemText.SetActive(true);
            return;
        }
        else
        {
            NoItemText.SetActive(false);
        }
        for (int i = 0; i < characters.Count; i++)
        {
            GameObject storeCharacterGO = Instantiate(StoreCharacter.gameObject, Parent);
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
