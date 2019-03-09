using System.Collections.Generic;
using UnityEngine;

public class ItemSelect : MonoBehaviour
{
    public ItemType itemTpye = ItemType.Character;
    public Transform parent;

    [Header ("Select character settings")]
    public SelectItemCharacter character;
    public Transform characterParent;

    public SelectableCharacter selectableCharacter;
    private readonly List<SelectItemCharacter> selectableCharacters = new List<SelectItemCharacter>();

    private GameObject selectedItemGO = null;

    [Header("Select weapon settings")]
    public SelectItemWeapon weapon;
    public Transform weaponParent;

    public SelectableWeapon selectableWeapon;
    private readonly List<SelectItemWeapon> selectableWeapons = new List<SelectItemWeapon>();

    private void Start()
    {
        switch (itemTpye)
        {
            case ItemType.Character:
                InitCharacters();
                break;
            case ItemType.Weapon:
                InitWeapons();
                break;
            case ItemType.Rune:
                Debug.Log("Not implemented");
                break;
            default:
                break;
        }
    }

    private void InitCharacters()
    {
        List<Character> characters = AccountInfo.Instance.ownCharacters;
        for (int i = 0; i < characters.Count; i++)
        {
            GameObject selectItem = Instantiate(character.gameObject, parent);
            SelectItemCharacter selectableCharacter = selectItem.GetComponent<SelectItemCharacter>();
            selectableCharacters.Add(selectableCharacter);
            selectableCharacter.Character = characters[i];
            selectableCharacter.itemSelect = this;
            selectableCharacter.Init();
        }
    }

    private void InitWeapons()
    {
        List<Weapon> weapons = AccountInfo.Instance.ownWeapons;
        for (int i = 0; i < weapons.Count; i++)
        {
            GameObject selectItem = Instantiate(weapon.gameObject, parent);
            SelectItemWeapon selectableWeapon = selectItem.GetComponent<SelectItemWeapon>();
            selectableWeapons.Add(selectableWeapon);
            selectableWeapon.Weapon = weapons[i];
            selectableWeapon.itemSelect = this;
            selectableWeapon.Init();
        }
    }

    public void ChangeSelectedCharacter(SelectItemCharacter item)
    {
        if (SelectData.selectedItemCharacter == item)
        {
            return;
        }

        if (SelectData.selectedItemCharacter == null)
        {
            SelectData.selectedItemCharacter = item;
            item.Selected();
        }
        else
        {
            SelectData.selectedItemCharacter.Deselected();
            SelectData.selectedItemCharacter = item;
            item.Selected();
        }
        SelectData.selectedCharacter = item.Character;

        if(selectedItemGO != null)
        {
            Destroy(selectedItemGO);
        }
        selectedItemGO = Instantiate(item.Character.previewPrefab, characterParent);

        selectableCharacter.ShowCharacterData();
    }

    public void ChangeSelectedWeapon(SelectItemWeapon item)
    {
        if (SelectData.selectedItemWeapon == item)
        {
            return;
        }

        if (SelectData.selectedItemWeapon == null)
        {
            SelectData.selectedItemWeapon = item;
            item.Selected();
        }
        else
        {
            SelectData.selectedItemWeapon.Deselected();
            SelectData.selectedItemWeapon = item;
            item.Selected();
        }
        SelectData.selectedWeapon = item.Weapon;

        if (selectedItemGO != null)
        {
            Destroy(selectedItemGO);
        }

        selectedItemGO = Instantiate(item.Weapon.previewPrefab, weaponParent);

        selectableWeapon.ShowWeapon();
    }

    private void OnDisable()
    {
        if(selectedItemGO != null)
        {
            Destroy(selectedItemGO);
        }
    }
}
