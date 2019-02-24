using UnityEngine;
using UnityEngine.UI;

public class SelectItemWeapon : MonoBehaviour
{
    public Text weaponName;
    public Image backGround;
    public Color selectedColor;

    private Color originalColor;

    public Weapon Weapon { get; set; } = null;
    public ItemSelect itemSelect;

    private void Start()
    {
        originalColor = backGround.color;
    }

    public void Init()
    {
        if (Weapon != null)
        {
            weaponName.text = Weapon.id;
        }
    }

    public void SelectWeapon()
    {
        itemSelect.ChangeSelectedWeapon(this);
    }

    public void Selected()
    {
        backGround.color = selectedColor;
    }

    public void Deselected()
    {
        backGround.color = originalColor;
    }
}
