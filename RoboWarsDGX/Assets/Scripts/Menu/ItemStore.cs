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
    private StoreType type = StoreType.RuneStore;

    [Header("Rune data:")]
    [SerializeField]
    private StoreRune storeRune;

    [Header("Character data:")]
    [SerializeField]
    private StoreCharacter storeCharacter;

    [Header("Weapon data:")]
    [SerializeField]
    private StoreWeapon storeWeapon;

    private List<StoreRune> storeRunes = new List<StoreRune>();
    private List<StoreCharacter> storeCharacters = new List<StoreCharacter>();
    private List<StoreWeapon> storeWeapons = new List<StoreWeapon>();

    public Transform Parent { get => parent; set => parent = value; }
    public GameObject NoItemText { get => noItemText; set => noItemText = value; }
    public StoreType Type { get => type; set => type = value; }

    public StoreRune StoreRune { get => storeRune; set => storeRune = value; }
    public StoreCharacter StoreCharacter { get => storeCharacter; set => storeCharacter = value; }
    public StoreWeapon StoreWeapon { get => storeWeapon; set => storeWeapon = value; }

    void Start()
    {
        switch (Type)
        {
            case StoreType.RuneStore:
                AccountInfo.Instance.SuccessRuneBuyingEvent += RuneStoreRefresh;
                break;
            case StoreType.CharacterStore:
                AccountInfo.Instance.SuccessCharacterBuyingEvent += CharacterStoreRefresh;
                break;
            case StoreType.WeaponStore:
                AccountInfo.Instance.SuccessWeaponBuyingEvent += WeaponStoreRefresh;
                break;
            default:
                break;
        }
        Init();
    }

    public void Init()
    {
        switch (Type)
        {
            case StoreType.RuneStore:
                RuneStoreInit();
                break;
            case StoreType.CharacterStore:
                CharacterStoreInit();
                break;
            case StoreType.WeaponStore:
                WeaponStoreInit();
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
            storeCharacters.Add(newStoreCharacter);
            newStoreCharacter.Character = characters[i];
            newStoreCharacter.InitCharacter();
        }
    }

    private void WeaponStoreInit()
    {
        List<Weapon> weapons = AccountInfo.Instance.GetStoreWeapons();
        if (weapons.Count == 0)
        {
            NoItemText.SetActive(true);
            return;
        }
        else
        {
            NoItemText.SetActive(false);
        }
        for (int i = 0; i < weapons.Count; i++)
        {
            GameObject storeWeaponGO = Instantiate(StoreWeapon.gameObject, Parent);
            StoreWeapon newStoreWeapon = storeWeaponGO.GetComponent<StoreWeapon>();
            storeWeapons.Add(newStoreWeapon);
            newStoreWeapon.Weapon = weapons[i];
            newStoreWeapon.InitWeapon();
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


    private void WeaponStoreRefresh(string id)
    {
        for (int i = 0; i < storeWeapons.Count; i++)
        {
            storeWeapons[i].Refresh(id);
        }
    }
}
