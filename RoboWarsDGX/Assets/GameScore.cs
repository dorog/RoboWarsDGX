using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public string displayName;
    public int kills;
    public int deaths;
    public int assists;

    public Text nameText;
    public Text killText;
    public Text deathText;
    public Text assistText;

    public void Init()
    {
        nameText.text = displayName;
        killText.text = "" + kills;
        deathText.text = "" + deaths;
        assistText.text = "" + assists;
    }
}
