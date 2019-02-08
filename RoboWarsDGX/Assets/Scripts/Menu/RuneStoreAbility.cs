using UnityEngine;
using UnityEngine.UI;

public class RuneStoreAbility : MonoBehaviour
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text amountText;

    public void Init(string name, string amount)
    {
        nameText.text = name;
        amountText.text = amount;
    }
}
