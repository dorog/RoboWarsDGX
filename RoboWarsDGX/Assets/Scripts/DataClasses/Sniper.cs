using UnityEngine;

public class Sniper : FiringWeapon
{
    public float scoopeFOV = 15f;
    private float originalF0V;
    private float scopeMouseIntensity;
    private Camera characterCamera;
    private bool scoped = false;
    public GameObject zoomImage;

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
                    if (!teamGame)
                    {
                        boneColliderHit.GotShot(dmg, displayName);
                    }
                    else
                    {
                        Debug.Log(SelectData.teamColor);
                        boneColliderHit.GotShot(dmg, displayName, SelectData.teamColor);
                    }
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
                aim.SetActive(true);
                zoomImage.SetActive(false);
                characterCamera.fieldOfView = originalF0V;
            }
            else
            {
                aim.SetActive(false);
                zoomImage.SetActive(true);
                characterCamera.fieldOfView = scoopeFOV;
            }
            scoped = !scoped;
        }
    }
}
