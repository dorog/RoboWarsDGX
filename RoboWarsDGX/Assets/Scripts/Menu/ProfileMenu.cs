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
    private Text coins;
    [SerializeField]
    private Text xp;

    public void OnEnable()
    {
        kills.text = "" + PlayerProfile.profileStats.Kills;
        headShots.text = "" + PlayerProfile.profileStats.HeadShots;
        deaths.text = "" + PlayerProfile.profileStats.Deaths;
        coins.text = "" + PlayerProfile.gold;
        xp.text = "" + PlayerProfile.experience;
    }
}
