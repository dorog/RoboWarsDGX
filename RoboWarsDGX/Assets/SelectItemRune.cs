using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectItemRune : MonoBehaviour, IPointerClickHandler
{
    public Image img;
    public Rune rune;
    public RuneGameSelect runeGameSelect;

    public void Init()
    {
        img.sprite = rune.icon;
    }

    private int clicked = 0;
    private float clicktime = 0;
    private readonly float clickdelay = 0.5f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Time.time - clicktime > clickdelay)
            {
                clicked = 1;
                clicktime = Time.time;
            }
            else
            {
                clicked = 0;
                runeGameSelect.AddRune(rune, gameObject);
                clicktime = 0;
            }
        }
    }
}
