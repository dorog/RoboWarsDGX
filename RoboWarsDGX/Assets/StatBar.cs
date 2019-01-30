using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public string statName;
    public string statValue;

    [SerializeField]
    private Text nameText;

    [SerializeField]
    private Text valueText;

    public void InitTexts()
    {
        nameText.text = statName;
        valueText.text = statValue;

    }
}
