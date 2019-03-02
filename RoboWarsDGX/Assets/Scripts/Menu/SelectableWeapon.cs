using UnityEngine;
using UnityEngine.UI;

public class SelectableWeapon : MonoBehaviour
{
    public Text dmg;
    public Text firingRate;
    public Text ammo;
    public Text ammoFull;
    public Text type;

    public GameObject dataGO;

    void Start()
    {
        dataGO.SetActive(false);
    }

    public void ShowWeapon()
    {
        if (!dataGO.activeSelf)
        {
            dataGO.SetActive(true);
        }

        dmg.text = "" + SelectData.selectedWeapon.dmg;
        firingRate.text = "" + SelectData.selectedWeapon.firingRate;
        ammo.text = "" + SelectData.selectedWeapon.ammo;
        ammoFull.text = "" + SelectData.selectedWeapon.extraAmmo;
        type.text = "" + SelectData.selectedWeapon.type;
    }
}
