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

    public void OnEnable()
    {
        kills.text = "" + PlayerProfile.profileStats.Kills;
        headShots.text = "" + PlayerProfile.profileStats.HeadShots;
        deaths.text = "" + PlayerProfile.profileStats.Deaths;
        coins.text = "" + PlayerProfile.coins;
    }
}
