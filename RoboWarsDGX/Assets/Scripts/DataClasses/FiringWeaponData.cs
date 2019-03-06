using UnityEngine;
using UnityEngine.UI;

public class FiringWeaponData
{
    public int ammo = 0;
    public int maxAmmoAtOnce = 0;
    public int extraAmmo = 0;
    public LayerMask layerMask;
    public float minTimeBetweenFire;
    public string displayName;
    public Text ammoText;
    public Text extraAmmoText;
    public float dmg;
    public float distance;
    public Transform firePosition;
}
