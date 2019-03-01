using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlaySelect : MonoBehaviour
{
    public GameObject characters;
    public GameObject weapons;
    public GameObject runes;

    private void Start()
    {
        Room currentRoom = PhotonNetwork.CurrentRoom;
        GameMode gameMode = SharedData.GetGameMode((string)currentRoom.CustomProperties[SharedData.GameModeKey]); //Refactor?

        ResetSelecData();
        StartSelection(gameMode);

        ShowCharacterSelect();
    }

    private void StartSelection(GameMode gameMode)
    {
        if(gameMode == GameMode.DeathMatch || gameMode == GameMode.BattleRoyal)
        {
            ShowCharacterSelect();
        }
        else
        {
            ShowTeamSelect();
        }
    }

    private void ShowTeamSelect()
    {
        Debug.Log("TeamSelect call");
        //TODO: team select, show character select with skinned character or character select then team select and i dont have to show with skin? doesnt matter i have to spawn with color anyway
    }

    private void ResetSelecData()
    {
        SelectData.selectedCharacter = null;
        SelectData.selectedItemCharacter = null;
        SelectData.selectedWeapon = null;
        SelectData.selectedItemWeapon = null;
    }

    private void ShowCharacterSelect()
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
        GameModeManager.Instance.SpawnPlayer();
        gameObject.SetActive(false);
    }
}
