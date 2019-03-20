using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public abstract class FiringWeapon : MonoBehaviourPun
{
    protected bool weaponCanFire = true;
    protected LayerMask layerMask;

    private int ammo = 0;
    private int maxAmmoAtOnce = 0;
    private int extraAmmo = 0;
    private Text ammoText;
    private Text extraAmmoText;

    protected bool teamGame;
    protected float minTimeBetweenFire;
    protected string displayName;
    protected float dmg;
    protected float distance;
    protected Transform firePosition;
    public RuntimeAnimatorController animatorController;
    public GameObject aim;

    protected void WeaponCanFire()
    {
        weaponCanFire = true;
    }

    public virtual void SetData(FiringWeaponData data)
    {
        ammo = data.ammo;
        maxAmmoAtOnce = ammo;
        extraAmmo = data.extraAmmo;
        layerMask = data.layerMask;
        minTimeBetweenFire = data.minTimeBetweenFire;
        displayName = data.displayName;
        ammoText = data.ammoText;
        extraAmmoText = data.extraAmmoText;
        dmg = data.dmg;
        distance = data.distance;
        firePosition = data.firePosition;
        teamGame = data.teamGame;

        aim.SetActive(true);
        ammoText.text = "" + ammo;
        extraAmmoText.text = "" + extraAmmo;
    }

    public abstract void FireCheck();

    public void ReloadCheck()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (extraAmmo == 0)
            {
                return;
            }
            if (extraAmmo >= (maxAmmoAtOnce - ammo))
            {
                extraAmmo -= (maxAmmoAtOnce - ammo);
                ammo = maxAmmoAtOnce;
                ammoText.text = "" + ammo;
                extraAmmoText.text = "" + extraAmmo;
            }
            else
            {
                ammo += extraAmmo;
                extraAmmo = 0;
                ammoText.text = "" + ammo;
                extraAmmoText.text = "" + extraAmmo;
            }
        }
    }

    protected BoneColliderHit InstantFire(Vector3 position, Vector3 forward, float distance)
    {
        RaycastHit hit;
        if (Physics.Raycast(position, forward, out hit, distance, layerMask))
        {
            BoneColliderHit boneColliderHit = hit.collider.gameObject.GetComponent<BoneColliderHit>();
            // hit.point: Spawn blood
            if (boneColliderHit == null)
            {
                return null;
            }
            return boneColliderHit;
        }
        return null;
    }

    protected void Fire()
    {
        ammo--;
        ammoText.text = "" + ammo;
    }

    protected bool HasAmmo()
    {
        return ammo == 0 ? false : true;
    }
}
