using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedRune : MonoBehaviour, IPointerClickHandler
{
    public int slotNumber;
    public RuneGameSelect runeGameSelect;
    public Image img;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            runeGameSelect.RemoveRune(slotNumber);
        }
    }
}
