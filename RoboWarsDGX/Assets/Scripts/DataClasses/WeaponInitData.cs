using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Init Data", menuName ="WeaponInitData")]
public class WeaponInitData : ScriptableObject
{
    [Header("AK-47 Settings")]
    public Vector3 ak47_position;
    public Vector3 ak47_rotation;
    public Vector3 ak47_gunhold_position;

    [Header("870_Shotgun Settings")]
    public Vector3 Shotgun_870_position;
    public Vector3 Shotgun_870_rotation;
    public Vector3 Shotgun_870_gunhold_position;

    [Header("L96_Sniper_Rifle Settings")]
    public Vector3 L96_Sniper_Rifle_position;
    public Vector3 L96_Sniper_Rifle_rotation;
    public Vector3 L96_Sniper_Rifle_gunhold_position;

    public Vector3 GetWeaponPosition(string weaponName)
    {
        switch (weaponName)
        {
            case "AK-47":
                return ak47_position;
            case "870_Shotgun":
                return Shotgun_870_position;
            case "L96_Sniper_Rifle":
                return L96_Sniper_Rifle_position;
            default:
                return Vector3.zero;
        }
    }

    public Vector3 GetWeaponRotation(string weaponName)
    {
        switch (weaponName)
        {
            case "AK-47":
                return ak47_rotation;
            case "870_Shotgun":
                return Shotgun_870_rotation;
            case "L96_Sniper_Rifle":
                return L96_Sniper_Rifle_rotation;
            default:
                return Vector3.zero;
        }
    }

    public Vector3 GetGunHoldPosition(string weaponName)
    {
        switch (weaponName)
        {
            case "AK-47":
                return ak47_gunhold_position;
            case "870_Shotgun":
                return Shotgun_870_gunhold_position;
            case "L96_Sniper_Rifle":
                return L96_Sniper_Rifle_gunhold_position;
            default:
                return Vector3.zero;
        }
    }
}
