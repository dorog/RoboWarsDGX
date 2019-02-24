using UnityEngine;
using UnityEngine.UI;

public class SelectableCharacter : MonoBehaviour
{
    public Text health;
    public Text armor;
    public Text jumpPower;
    public Text hpReg;
    public Text movementSpeed;
    public Text shotgunDmg;
    public Text sniperDmg;
    public Text smgDmg;
    public Text type;

    public GameObject dataGO;

    void Start()
    {
        dataGO.SetActive(false);
    }

    public void ShowCharacterData()
    {
        if (!dataGO.activeSelf)
        {
            dataGO.SetActive(true);
        }

        health.text = "" + SelectData.selectedCharacter.health;
        armor.text = "" + SelectData.selectedCharacter.armor;
        jumpPower.text = "" + SelectData.selectedCharacter.jumpPower;
        hpReg.text = "" + SelectData.selectedCharacter.hpReg;
        movementSpeed.text = "" + SelectData.selectedCharacter.movementSpeed;
        shotgunDmg.text = "" + SelectData.selectedCharacter.shotGunDmg;
        sniperDmg.text = "" + SelectData.selectedCharacter.sniperDmg;
        smgDmg.text = "" + SelectData.selectedCharacter.smgDmg;
        type.text = "" + SelectData.selectedCharacter.type;
    }
}
