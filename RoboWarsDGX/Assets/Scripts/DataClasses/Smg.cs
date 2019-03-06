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
                    boneColliderHit.GotShot(dmg, displayName);
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
                        boneColliderHit.GotShot(dmg, displayName);
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
