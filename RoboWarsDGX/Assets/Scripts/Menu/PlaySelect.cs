using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlaySelect : MonoBehaviour
{
    public GameObject characters;
    public GameObject weapons;
    public GameObject runes;
    public GameObject teams;

    private void Start()
    {
        Room currentRoom = PhotonNetwork.CurrentRoom;
        GameMode gameMode = SharedData.GetGameMode((string)currentRoom.CustomProperties[SharedData.GameModeKey]); //Refactor?

        ResetSelecData();
        StartSelection(gameMode);
    }

    private void StartSelection(GameMode gameMode)
    {
        if(gameMode == GameMode.DeathMatch || gameMode == GameMode.BattleRoyal)
        {
            ShowCharacterSelect();
            //ShowRuneSelect();
        }
        else
        {
            ShowTeamSelect();
        }
    }

    private void ShowTeamSelect()
    {
        //TODO: team select, show character select with skinned character or character select then team select and i dont have to show with skin? doesnt matter i have to spawn with color anyway
        characters.SetActive(false);
        weapons.SetActive(false);
        runes.SetActive(false);
        teams.SetActive(true);
    }

    private void ResetSelecData()
    {
        SelectData.selectedCharacter = null;
        SelectData.selectedItemCharacter = null;
        SelectData.selectedWeapon = null;
        SelectData.selectedItemWeapon = null;
        SelectData.selectedRunes = new Rune[3] { null, null, null };
    }

    private void ShowCharacterSelect()
    {
        characters.SetActive(true);
        weapons.SetActive(false);
        runes.SetActive(false);
        teams.SetActive(false);
    }

    public void ShowWeaponSelect()
    {
        characters.SetActive(false);
        weapons.SetActive(true);
        runes.SetActive(false);
        teams.SetActive(false);
    }

    public void ShowRuneSelect()
    {
        characters.SetActive(false);
        weapons.SetActive(false);
        runes.SetActive(true);
        teams.SetActive(false);
    }

    public void StartGame()
    {
        GameModeManager.Instance.SpawnPlayer();
        gameObject.SetActive(false);
    }

    public void SelectRedTeam()
    {
        SelectData.teamColor = TeamColor.Red;
        ShowCharacterSelect();
    }

    public void SelectBlueTeam()
    {
        SelectData.teamColor = TeamColor.Blue;
        ShowCharacterSelect();
    }
}
