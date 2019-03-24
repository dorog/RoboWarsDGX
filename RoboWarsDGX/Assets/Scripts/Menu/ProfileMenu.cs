using UnityEngine;
using UnityEngine.UI;

public class ProfileMenu : MonoBehaviour
{
    [SerializeField]
    private Text kills;
    [SerializeField]
    private Text headShots;
    [SerializeField]
    private Text deaths;
    [SerializeField]
    private Text assists;
    [SerializeField]
    private Text coins;
    [SerializeField]
    private Text xp;

    public Text Kills { get => kills; set => kills = value; }
    public Text HeadShots { get => headShots; set => headShots = value; }
    public Text Deaths { get => deaths; set => deaths = value; }
    public Text Coins { get => coins; set => coins = value; }
    public Text Xp { get => xp; set => xp = value; }
    public Text Assists { get => assists; set => assists = value; }

    public void OnEnable()
    {
        Kills.text = "" + PlayerProfile.profileStats.Kills;
        HeadShots.text = "" + PlayerProfile.profileStats.HeadShots;
        Assists.text = "" + PlayerProfile.profileStats.Assists;
        Deaths.text = "" + PlayerProfile.profileStats.Deaths;
        Coins.text = "" + PlayerProfile.gold;
        Xp.text = "" + PlayerProfile.experience;
    }
}
