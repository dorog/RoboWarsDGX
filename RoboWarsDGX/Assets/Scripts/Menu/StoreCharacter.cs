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
    public Image Icon { get => icon; set => icon = value; }
    public Text Hp { get => hp; set => hp = value; }
    public Text HpReg { get => hpReg; set => hpReg = value; }
    public Text Armor { get => armor; set => armor = value; }
    public Text SmgDmg { get => smgDmg; set => smgDmg = value; }
    public Text ShotgunDmg { get => shotgunDmg; set => shotgunDmg = value; }
    public Text SniperDmg { get => sniperDmg; set => sniperDmg = value; }
    public Text MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public Text JumpPower { get => jumpPower; set => jumpPower = value; }
    public Text Price { get => price; set => price = value; }
    public Button BuyButton { get => buyButton; set => buyButton = value; }
    public GameObject BuyPart { get => buyPart; set => buyPart = value; }

    public void InitCharacter()
    {
        if(Character != null)
        {
            Icon.sprite = Character.icon;
            Hp.text = "" + Character.health;
            HpReg.text = "" + Character.hpReg;
            Armor.text = "" + Character.armor;
            SmgDmg.text = "" + Character.smgDmg;
            ShotgunDmg.text = "" + Character.shotGunDmg;
            SniperDmg.text = "" + Character.sniperDmg;
            MovementSpeed.text = "" + Character.movementSpeed;
            JumpPower.text = "" + Character.jumpPower;
            if (storeCharacter)
            {
                Price.text = "" + Character.price + " Gold";
                if (PlayerProfile.gold < Character.price)
                {
                    BuyButton.interactable = false;
                }
                BuyButton.onClick.AddListener(delegate { AccountInfo.Instance.BuyCharacter(Character.id, Character.price); });
            }
            else
            {
                BuyPart.SetActive(false);
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
                BuyButton.interactable = false;
            }
        }
    }
}
