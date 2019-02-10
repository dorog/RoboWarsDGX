using UnityEngine;
using UnityEngine.UI;

public class StoreCharacter : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text hp;
    [SerializeField]
    private Text hpReg;
    [SerializeField]
    private Text armor;
    [SerializeField]
    private Text smgDmg;
    [SerializeField]
    private Text shotgunDmg;
    [SerializeField]
    private Text sniperDmg;
    [SerializeField]
    private Text movementSpeed;
    [SerializeField]
    private Text jumpPower;
    [SerializeField]
    private Text price;
    [SerializeField]
    private Button buyButton;

    [SerializeField]
    private GameObject buyPart;

    public bool storeCharacter = true;

    public Character Character { get; set; } = null;

    public void InitCharacter()
    {
        if(Character != null)
        {
            icon.sprite = Character.icon;
            hp.text = "" + Character.health;
            hpReg.text = "" + Character.hpReg;
            armor.text = "" + Character.armor;
            smgDmg.text = "" + Character.smgDmg;
            shotgunDmg.text = "" + Character.shotGunDmg;
            sniperDmg.text = "" + Character.sniperDmg;
            movementSpeed.text = "" + Character.movementSpeed;
            jumpPower.text = "" + Character.jumpPower;
            if (storeCharacter)
            {
                price.text = "" + Character.price + " Gold";
                if (PlayerProfile.gold < Character.price)
                {
                    buyButton.interactable = false;
                }
                buyButton.onClick.AddListener(delegate { AccountInfo.Instance.BuyCharacter(Character.id, Character.price); });
            }
            else
            {
                buyPart.SetActive(false);
            }
        }
    }

    public void Refresh(string id)
    {
        if (id == Character.id)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            if(PlayerProfile.gold < Character.price)
            {
                buyButton.interactable = false;
            }
        }
    }
}
