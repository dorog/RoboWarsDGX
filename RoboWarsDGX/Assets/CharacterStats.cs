using Photon.Pun;
using System.Collections.Generic;

public class CharacterStats : MonoBehaviourPun
{
    private Character characterStat;
    private Weapon weaponStat;
    private Rune[] runes;

    void Awake()
    {
        if (photonView.IsMine)
        {
            List<Character> characters = AccountInfo.Instance.ownCharacters;
            foreach (Character character in characters)
            {
                if (character.id == SelectData.selectedCharacter.id)
                {
                    characterStat = character;
                    break;
                }
            }

            List<Weapon> weapons = AccountInfo.Instance.ownWeapons;
            foreach (Weapon weapon in weapons)
            {
                if (weapon.id == SelectData.selectedWeapon.id)
                {
                    weaponStat = weapon;
                    break;
                }
            }
        }
    }

    public float GetHP()
    {
        return characterStat.health;
    }

    public float GetArmor()
    {
        return characterStat.armor;
    }

    public float GetMovementSpeed()
    {
        return characterStat.movementSpeed;
    }

    public float GetJumpPower()
    {
        return characterStat.jumpPower;
    }

    public float GetDmg(WeaponType weaponType)
    {
        if (photonView.IsMine)
        {
            switch (weaponType)
            {
                case WeaponType.Shotgun:
                    return characterStat.shotGunDmg + weaponStat.dmg;
                case WeaponType.SMG:
                    return characterStat.smgDmg + weaponStat.dmg;
                case WeaponType.Sniper:
                    return characterStat.sniperDmg + weaponStat.dmg;
                default:
                    return 0;
            }
        }
        else
        {
            return 1;
        }
    }


    public float GetWeaponDistance()
    {
        return weaponStat.distance;
    }

    public float GetRapidTime()
    {
        return 1 / weaponStat.firingRate;
    }

    public int GetAmmo()
    {
        return weaponStat.ammo;
    }

    public int GetExtraAmmo()
    {
        return weaponStat.extraAmmo;
    }

    public float GetBoneIntensity(Bones bone)
    {
        if (photonView.IsMine)
        {
            switch (bone)
            {
                case Bones.Chest:
                    return characterStat.chestIntensity;
                case Bones.DownArm:
                    return characterStat.downArmIntensity;
                case Bones.DownLeg:
                    return characterStat.downLegIntensity;
                case Bones.Foot:
                    return characterStat.footIntensity;
                case Bones.Spine:
                    return characterStat.spineIntensity;
                case Bones.UpArm:
                    return characterStat.upArmIntensity;
                default:
                    return characterStat.upLegIntensity;
            }
        }
        else
        {
            return 1;
        }
    }
}
