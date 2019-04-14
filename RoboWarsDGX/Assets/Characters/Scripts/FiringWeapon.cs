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
    private float reloadTime;
    private bool inReload = false;

    protected bool teamGame;
    protected float minTimeBetweenFire;
    protected string displayName;
    protected float dmg;
    protected float distance;
    protected Transform firePosition;
    public RuntimeAnimatorController animatorController;
    public GameObject aim;

    [Header ("Firing settings")]
    public ParticleSystem fireEffect;
    public float fireUpDistance = 0.1f;
    public float timeForUp = 0.25f;
    public float timeForDown = 0.25f;
    public float timeBetweenIncreaseAndDistance = 0.1f;

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
        reloadTime = data.reloadTime;

        aim.SetActive(true);
        ammoText.text = "" + ammo;
        extraAmmoText.text = "" + extraAmmo;
    }

    public abstract bool FireCheck();

    public abstract void ShowEffect();

    public void ReloadCheck()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    protected BoneColliderHit InstantFire(Vector3 position, Vector3 forward, float distance)
    {
        RaycastHit hit;
        if (Physics.Raycast(position, forward, out hit, distance, layerMask))
        {
            BoneColliderHit boneColliderHit = hit.collider.gameObject.GetComponent<BoneColliderHit>();

            if (boneColliderHit != null)
            {

                boneColliderHit.SpawnBlood(hit.point, hit.normal);

                return boneColliderHit;
            }

            GameModeManager.Instance.SpawnMapDamage(hit.collider.sharedMaterial, hit.point, hit.normal);
        }
        return null;
    }

    protected void Fire()
    {
        ammo--;
        ammoText.text = "" + ammo;
        if(ammo == 0 && extraAmmo !=0 )
        {
            Reload();
        }
    }

    protected bool CanFire()
    {
        return ammo == 0 || inReload? false : true;
    }

    protected void Reload()
    {
        if (!inReload && extraAmmo != 0)
        {
            Invoke("ChangeAmmo", reloadTime);
            inReload = true;
        }
    }

    private void ChangeAmmo()
    {
        inReload = false;
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
