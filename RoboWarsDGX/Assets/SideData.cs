using UnityEngine;
using UnityEngine.UI;

public class SideData : MonoBehaviour
{
    public float livingTime = 3f;

    [Header("UI")]
    public Text killer;
    public Image weaponImage;
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

    void Update()
    {
        livingTime -= Time.deltaTime;
        if(livingTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetData(string killer, WeaponType type, string target, SideHistoryType sideHistoryType)
    {
        this.killer.text = killer;
        this.target.text = target;
        weaponImage.sprite = GetWeaponSprite(type);
        border.color = GetBorderColor(sideHistoryType);
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
}
