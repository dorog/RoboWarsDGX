using UnityEngine;

public class Sniper : FiringWeapon
{
    public float scoopeFOV = 15f;
    private float originalF0V;
    private float scopeMouseIntensity;
    private Camera characterCamera;
    private bool scoped = false;

    public override void SetData(FiringWeaponData data)
    {
        base.SetData(data);
        characterCamera = Camera.main;
        originalF0V = characterCamera.fieldOfView;
        scopeMouseIntensity = scoopeFOV / originalF0V;
    }

    public override void FireCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!HasAmmo())
            {
                return;
            }
            if (weaponCanFire)
            {
                BoneColliderHit boneColliderHit = InstantFire(firePosition.position, firePosition.forward, distance);
                if (boneColliderHit != null)
                {
                    Debug.Log(displayName);
                    boneColliderHit.GotShot(dmg, displayName);
                    Debug.Log("boneHitAfter");
                }
                Fire();
                Invoke("WeaponCanFire", minTimeBetweenFire);
                weaponCanFire = false;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (scoped)
            {
                characterCamera.fieldOfView = originalF0V;
            }
            else
            {
                characterCamera.fieldOfView = scoopeFOV;
            }
            scoped = !scoped;
        }
    }
}
