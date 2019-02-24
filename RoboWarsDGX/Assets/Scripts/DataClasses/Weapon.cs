using UnityEngine;
using PlayFab.ClientModels;

public class Weapon
{
    public string id;
    public int price;

    public Sprite icon;
    public float dmg;
    public float firingRate;
    public float ammo;
    public float ammoFull;
    public string type;
    public GameObject prefab;
    public GameObject previewPrefab;

    public static Weapon CatalogItemToWeapon(CatalogItem item)
    {
        Weapon weapon = new Weapon();

        string[] splited = SharedData.ParseJson(item.CustomData.ToString());

        weapon.id = item.ItemId;

        InitWeapon(weapon, splited);

        return weapon;
    }

    private static void InitWeapon(Weapon weapon, string[] splited)
    {
        weapon.icon = Resources.Load<Sprite>("WeaponIcons/" + splited[1]);
        weapon.dmg = float.Parse(splited[3]);
        weapon.firingRate = float.Parse(splited[5]);
        weapon.ammo = float.Parse(splited[7]);
        weapon.ammoFull = float.Parse(splited[9]);
        weapon.prefab = Resources.Load<GameObject>("Weapons/" + splited[11]);
        weapon.previewPrefab = Resources.Load<GameObject>("WeaponsPreview/" + splited[11]);
        weapon.type = splited[13];
    }
}
