using UnityEngine;
using UnityEngine.UI;

public class SideData : MonoBehaviour
{
    public float livingTime = 3f;

    [Header("UI")]
    public Text killer;
    public Image weaponImage;
    public GameObject headshotGO;
    public Image headshotWeapon;
    public Text target;
    public Image border;

    [Header("Image references")]
    public Sprite shotgun;
    public Sprite sniper;
    public Sprite smg;

    [Header("Border colors")]
    public Color killerColor;
    public Color assistColor;
    public Color targetColor;
    public Color nothingColor;

    [Header("Team Colors")]
    public Color redTeam;
    public Color blueTeam;

    void Update()
    {
        livingTime -= Time.deltaTime;
        if(livingTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetDataSingle(string killer, WeaponType type, string target, SideHistoryType sideHistoryType, bool headshot)
    {
        this.killer.text = killer;
        this.target.text = target;
        border.color = GetBorderColor(sideHistoryType);

        if (headshot)
        {
            weaponImage.gameObject.SetActive(false);
            headshotGO.SetActive(true);
            headshotWeapon.sprite = GetWeaponSprite(type);
        }
        else
        {
            weaponImage.gameObject.SetActive(true);
            headshotGO.SetActive(false);
            weaponImage.sprite = GetWeaponSprite(type);
        }
    }

    public void SetDataTeam(string killer, WeaponType type, string target, SideHistoryType sideHistoryType, TeamColor killerColor, TeamColor targetColor, bool headshot)
    {
        SetDataSingle(killer, type, target, sideHistoryType, headshot);
        this.killer.color = GetTextColor(killerColor);
        this.target.color = GetTextColor(targetColor);
    }

    private Sprite GetWeaponSprite(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Shotgun:
                return shotgun;
            case WeaponType.SMG:
                return smg;
            case WeaponType.Sniper:
                return sniper;
            default:
                return sniper;
        }
    }

    private Color GetBorderColor(SideHistoryType sideHistoryType)
    {
        switch (sideHistoryType)
        {
            case SideHistoryType.assist:
                return assistColor;
            case SideHistoryType.killer:
                return killerColor;
            case SideHistoryType.target:
                return targetColor;
            default:
                return nothingColor;
        }
    }

    private Color GetTextColor(TeamColor color)
    {
        if(color == TeamColor.Blue)
        {
            return blueTeam;
        }
        else
        {
            return redTeam;
        }
    }
}
