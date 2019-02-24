using UnityEngine;
using UnityEngine.UI;

public class SelectItemCharacter : MonoBehaviour
{
    public Text characterName;
    public Image backGround;
    public Color selectedColor;

    private Color originalColor;

    public Character Character { get; set; } = null;
    public ItemSelect itemSelect;

    private void Start()
    {
        originalColor = backGround.color;
    }

    public void Init()
    {
        if(Character != null)
        {
            characterName.text = Character.id;
        }
    }

    public void SelectCharacter()
    {
        itemSelect.ChangeSelectedCharacter(this);
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
