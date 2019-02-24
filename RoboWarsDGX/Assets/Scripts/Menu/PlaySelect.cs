using UnityEngine;

public class PlaySelect : MonoBehaviour
{
    public GameObject characters;
    public GameObject weapons;
    public GameObject runes;

    public DeathMatchManager manager;

    private void Start()
    {
        ResetSelecData();
        ShowFirstScenario();
    }

    private void ResetSelecData()
    {
        SelectData.selectedCharacter = null;
        SelectData.selectedItemCharacter = null;
        SelectData.selectedWeapon = null;
        SelectData.selectedItemWeapon = null;
    }

    private void ShowFirstScenario()
    {
        characters.SetActive(true);
        weapons.SetActive(false);
        //runes.SetActive(false);
    }

    public void ShowWeaponSelect()
    {
        characters.SetActive(false);
        weapons.SetActive(true);
    }

    public void StartGame()
    {
        manager.SpawnPlayer();
        gameObject.SetActive(false);
    }
}
