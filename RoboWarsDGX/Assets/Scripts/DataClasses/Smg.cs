using UnityEngine;

public class Smg : FiringWeapon
{
    private bool inFire = false;

    public override bool FireCheck()
    {

        if (Input.GetMouseButton(0))
        {
            if (!CanFire())
            {
                return false;
            }
            if (!inFire && weaponCanFire)
            {
                inFire = true;

                BoneColliderHit boneColliderHit = InstantFire(firePosition.position, firePosition.forward, distance);
                if (boneColliderHit != null)
                {
                    if (!teamGame)
                    {
                        boneColliderHit.GotShot(dmg, displayName, WeaponType.SMG);
                    }
                    else
                    {
                        boneColliderHit.GotShot(dmg, displayName, SelectData.teamColor, WeaponType.SMG);
                    }
                }
                ShowEffect();
                Fire();
                Invoke("WeaponCanFire", minTimeBetweenFire);
                weaponCanFire = false;
                return true;
            }
            else
            {
                if (weaponCanFire)
                {
                    BoneColliderHit boneColliderHit = InstantFire(firePosition.position, firePosition.forward, distance);
                    if (boneColliderHit != null)
                    {
                        if (!teamGame)
                        {
                            boneColliderHit.GotShot(dmg, displayName, WeaponType.SMG);
                        }
                        else
                        {
                            boneColliderHit.GotShot(dmg, displayName, SelectData.teamColor, WeaponType.SMG);
                        }
                    }
                    ShowEffect();
                    Fire();
                    Invoke("WeaponCanFire", minTimeBetweenFire);
                    weaponCanFire = false;
                    return true;
                }
                return false;
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            inFire = false;
            return false;
        }
        return false;
    }

    public override void ShowEffect()
    {
        fireEffect.Play();
    }
}
