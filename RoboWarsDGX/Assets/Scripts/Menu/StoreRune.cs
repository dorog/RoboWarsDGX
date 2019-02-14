using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreRune : MonoBehaviour
{
    public bool inStore = true;

    [SerializeField]
    private Transform abilityParent;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private RuneStoreAbility goodAbility;

    [SerializeField]
    private RuneStoreAbility badAbility;

    [SerializeField]
    private GameObject buyPart;

    [SerializeField]
    private Button buyButton;

    [SerializeField]
    private Text price;

    public Rune Rune { get; set; } = null;
    public Transform AbilityParent { get => abilityParent; set => abilityParent = value; }
    public Image Icon { get => icon; set => icon = value; }
    public RuneStoreAbility GoodAbility { get => goodAbility; set => goodAbility = value; }
    public RuneStoreAbility BadAbility { get => badAbility; set => badAbility = value; }
    public GameObject BuyPart { get => buyPart; set => buyPart = value; }
    public Button BuyButton { get => buyButton; set => buyButton = value; }
    public Text Price { get => price; set => price = value; }

    public void InitRune()
    {
        if (Rune != null)
        {
            Icon.sprite = Rune.icon;

            List<RunStoreDescription> descriptions = Rune.GetData();
            for(int i=0; i< descriptions.Count; i++)
            {
                if (descriptions[i].isGood)
                {
                    AddAbility(GoodAbility, descriptions[i].displayName, descriptions[i].displayAmount);
                }
                else
                {
                    AddAbility(BadAbility, descriptions[i].displayName, descriptions[i].displayAmount);
                }
            }

            if (inStore)
            {
                Price.text = "" + Rune.price + " XP";
                if(PlayerProfile.experience < Rune.price)
                {
                    BuyButton.interactable = false;
                }
                BuyButton.onClick.AddListener(delegate { AccountInfo.Instance.BuyRune(Rune.id, Rune.price); });
            }
            else
            {
                BuyPart.SetActive(false);
            }
        }
    }

    private void AddAbility(RuneStoreAbility runeStoreAbility, string name, string amount)
    {
        GameObject runeStore = Instantiate(runeStoreAbility.gameObject, AbilityParent);
        RuneStoreAbility runeStoreAbilityScript = runeStore.GetComponent<RuneStoreAbility>();
        runeStoreAbilityScript.Init(name, amount);
    }

    public void Refresh(string id)
    {
        if(id == Rune.id)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            if (PlayerProfile.experience < Rune.price)
            {
                BuyButton.interactable = false;
            }
        }
    }
}
