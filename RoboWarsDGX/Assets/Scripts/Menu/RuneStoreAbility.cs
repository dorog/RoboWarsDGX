using UnityEngine;
using UnityEngine.UI;

public class RuneStoreAbility : MonoBehaviour
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text amountText;

    public Text NameText { get => nameText; set => nameText = value; }
    public Text AmountText { get => amountText; set => amountText = value; }

    public void Init(string name, string amount)
    {
        NameText.text = name;
        AmountText.text = amount;
    }
}
