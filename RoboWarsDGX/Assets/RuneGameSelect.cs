using System.Collections.Generic;
using UnityEngine;

public class RuneGameSelect : MonoBehaviour
{
    public Transform gridLayout;
    public SelectItemRune selectItemRune;
    private int runeCount = 0;
    public Transform[] runeTransform;
    public SelectedRune selectedRuneSlot;

    private GameObject[] items;

    public void Start()
    {
        items = new GameObject[SelectData.selectedRunes.Length];

        List<Rune> runes = AccountInfo.Instance.ownRunes;
        foreach(Rune rune in runes)
        {
            GameObject runeGO = Instantiate(selectItemRune.gameObject, gridLayout);
            SelectItemRune runeScript = runeGO.GetComponent<SelectItemRune>();
            runeScript.rune = rune;
            runeScript.runeGameSelect = this;
            runeScript.Init();
        }
        if(runeTransform.Length < SelectData.selectedRunes.Length)
        {
            Debug.LogError("Less rune transform than selectable rune");
        }
    }

    public void AddRune(Rune selectedRune, GameObject item)
    {
        if(runeCount == 3)
        {   
            //TODO: Sound effekt
            return;
        }

        for (int i=0; i< SelectData.selectedRunes.Length; i++)
        {
            if(SelectData.selectedRunes[i] == null)
            {
                SelectData.selectedRunes[i] = selectedRune;
                runeCount++;
                GameObject selectedRuneSlotGO = Instantiate(selectedRuneSlot.gameObject, runeTransform[i]);
                SelectedRune selectedRuneScript = selectedRuneSlotGO.GetComponent<SelectedRune>();
                selectedRuneScript.slotNumber = i;
                selectedRuneScript.runeGameSelect = this;
                selectedRuneScript.img.sprite = selectedRune.icon;

                item.SetActive(false);
                items[i] = item;
                break;
            }
        }

    }

    public void RemoveRune(int number)
    {
        items[number].SetActive(true);
        Destroy(runeTransform[number].GetChild(0).gameObject);
        SelectData.selectedRunes[number] = null;
        runeCount--;
    }

}
