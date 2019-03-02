using UnityEngine;
using UnityEngine.UI;

public class StoreWeapon : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text dmg;
    [SerializeField]
    private Text firingRate;
    [SerializeField]
    private Text ammo;
    [SerializeField]
    private Text ammoFull;
    [SerializeField]
    private Text type;

    [SerializeField]
    private Text price;
    [SerializeField]
    private Button buyButton;
    [SerializeField]
    private GameObject buyPart;

    public Weapon Weapon { get; set; } = null;
    public Button BuyButton { get => buyButton; set => buyButton = value; }
    public GameObject BuyPart { get => buyPart; set => buyPart = value; }
    public Text Price { get => price; set => price = value; }
    public Image Icon { get => icon; set => icon = value; }
    public Text Dmg { get => dmg; set => dmg = value; }
    public Text FiringRate { get => firingRate; set => firingRate = value; }
    public Text Ammo { get => ammo; set => ammo = value; }
    public Text AmmoFull { get => ammoFull; set => ammoFull = value; }
    public Text Type { get => type; set => type = value; }

    public bool storeWeapon = true;

    public void Refresh(string id)
    {
        if (id == Weapon.id)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            if (PlayerProfile.iron < Weapon.price)
            {
                BuyButton.interactable = false;
            }
        }
    }

    public void InitWeapon()
    {
        if (Weapon != null)
        {
            Icon.sprite = Weapon.icon;
            Dmg.text = "" + Weapon.dmg;
            FiringRate.text = "" + Weapon.firingRate + " shot/s";
            Ammo.text = "" + Weapon.ammo;
            AmmoFull.text = "" + Weapon.extraAmmo;
            Type.text = "" + Weapon.type;

            if (storeWeapon)
            {
                Price.text = "" + Weapon.price + " Iron";
                if (PlayerProfile.iron < Weapon.price)
                {
                    BuyButton.interactable = false;
                }
                BuyButton.onClick.AddListener(delegate { AccountInfo.Instance.BuyWeapon(Weapon.id, Weapon.price); });
            }
            else
            {
                BuyPart.SetActive(false);
            }
        }
    }
}
