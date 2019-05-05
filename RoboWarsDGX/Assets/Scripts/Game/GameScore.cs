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

    [Header("Color change")]
    public Color color;
    public Image nameImage;
    public Image emptyImage;
    public Image killImage;
    public Image deathImage;
    public Image assistsImage;

    public void Init()
    {
        nameText.text = displayName;
        killText.text = "" + kills;
        deathText.text = "" + deaths;
        assistText.text = "" + assists;
    }

    public void ChangeColor()
    {
        nameImage.color = color;
        emptyImage.color = color;
        killImage.color = color;
        deathImage.color = color;
        assistsImage.color = color;
    }
}
