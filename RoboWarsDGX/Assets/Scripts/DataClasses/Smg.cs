using UnityEngine;

public class Smg : FiringWeapon
{
    private bool inFire = false;

    public override void FireCheck()
    {

        if (Input.GetMouseButton(0))
        {
            if (!HasAmmo())
            {
                return;
            }
            if (!inFire && weaponCanFire)
            {
                inFire = true;

                BoneColliderHit boneColliderHit = InstantFire(firePosition.position, firePosition.forward, distance);
                if (boneColliderHit != null)
                {
                    if (!teamGame)
                    {
                        boneColliderHit.GotShot(dmg, displayName);
                    }
                    else
                    {
                        boneColliderHit.GotShot(dmg, displayName, SelectData.teamColor);
                    }
                }

                Fire();
                Invoke("WeaponCanFire", minTimeBetweenFire);
                weaponCanFire = false;
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
                            boneColliderHit.GotShot(dmg, displayName);
                        }
                        else
                        {
                            boneColliderHit.GotShot(dmg, displayName, SelectData.teamColor);
                        }
                    }
                    Fire();
                    Invoke("WeaponCanFire", minTimeBetweenFire);
                    weaponCanFire = false;
                }
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            inFire = false;
        }
    }
}
