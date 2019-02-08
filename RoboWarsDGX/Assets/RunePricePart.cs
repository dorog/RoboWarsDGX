using UnityEngine;
using UnityEngine.UI;

public class RunePricePart : MonoBehaviour
{
    [SerializeField]
    private Text priceText;
    [SerializeField]
    private Button buyButton;

    public void Init(int price, string id, GameObject forDelete)
    {
        priceText.text = "" + price + " " + SharedData.runeVirtualCurrency;
        if(PlayerProfile.experience < price)
        {
            buyButton.interactable = false;
        }
        buyButton.onClick.AddListener(delegate { AccountInfo.Instance.BuyRune(id, price, forDelete); });
    }
}
