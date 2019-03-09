using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlaySelect : MonoBehaviour
{
    public GameObject cam;
    public GameObject characters;
    public GameObject weapons;
    public GameObject runes;
    public GameObject teams;
    public GameObject respawn;

    public GameObject teamSelectChanger;

    private bool firstSelect = true;
    private States state;

    private GameObject selectedMenu = null;

    private void Start()
    {
        Room currentRoom = PhotonNetwork.CurrentRoom;
        GameMode gameMode = SharedData.GetGameMode((string)currentRoom.CustomProperties[SharedData.GameModeKey]); //Refactor?

        ResetSelecData();
        SetUIToDefault();
        StartSelection(gameMode);
    }

    private void ResetSelecData()
    {
        SelectData.selectedCharacter = null;
        SelectData.selectedItemCharacter = null;
        SelectData.selectedWeapon = null;
        SelectData.selectedItemWeapon = null;
        SelectData.selectedRunes = new Rune[3] { null, null, null };
    }

    private void SetUIToDefault()
    {
        characters.SetActive(false);
        weapons.SetActive(false);
        runes.SetActive(false);
        teams.SetActive(false);
        respawn.SetActive(false);
        cam.SetActive(true);
    }

    private void StartSelection(GameMode gameMode)
    {
        if(gameMode == GameMode.DeathMatch || gameMode == GameMode.BattleRoyal)
        {
            state = States.CharacterSelect;
            teamSelectChanger.SetActive(false);
        }
        else
        {
            state = States.TeamSelect;
        }
        Next();
    }

    public void ShowTeamSelect()
    {
        if (!firstSelect)
        {
            respawn.SetActive(false);
        }
        else
        {
            state = States.CharacterSelect;
        }
        teams.SetActive(true);
        selectedMenu = teams;
    }

    public void SelectRedTeam()
    {
        SelectData.teamColor = TeamColor.Red;
        Next();
    }

    public void SelectBlueTeam()
    {
        SelectData.teamColor = TeamColor.Blue;
        Next();
    }

    public void ShowCharacterSelect()
    {
        if (!firstSelect)
        {
            respawn.SetActive(false);
        }
        else
        {
            state = States.WeaponSelect;
        }
        characters.SetActive(true);
        selectedMenu = characters;
    }

    public void ShowWeaponSelect()
    {
        if (!firstSelect)
        {
            respawn.SetActive(false);
        }
        else
        {
            state = States.RuneSelect;
        }
        weapons.SetActive(true);
        selectedMenu = weapons;
    }

    public void ShowRuneSelect()
    {
        if (!firstSelect)
        {
            respawn.SetActive(false);
        }
        else
        {
            state = States.Ready;
        }
        runes.SetActive(true);
        selectedMenu = runes;
    }

    private void ShowRespawn()
    {
        respawn.SetActive(true);
        selectedMenu = respawn;
    }

    private void StartGame()
    {
        firstSelect = false;
        cam.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        GameModeManager.Instance.SpawnPlayer();
    }

    public void Died()
    {
        Cursor.lockState = CursorLockMode.None;
        ShowRespawn();
        cam.SetActive(true);
    }

    public void Respawn()
    {
        cam.SetActive(false);
        respawn.SetActive(false);
        GameModeManager.Instance.SpawnPlayer();
    }

    public void Next()
    {
        if (selectedMenu != null)
        {
            selectedMenu.SetActive(false);
        }
        if (firstSelect)
        {
            switch (state)
            {
                case States.TeamSelect:
                    ShowTeamSelect();
                    break;
                case States.CharacterSelect:
                    ShowCharacterSelect();
                    break;
                case States.WeaponSelect:
                    ShowWeaponSelect();
                    break;
                case States.RuneSelect:
                    ShowRuneSelect();
                    break;
                case States.Ready:
                    StartGame();
                    break;
            }
        }
        else
        {
            ShowRespawn();
        }
    }

    private enum States
    {
        TeamSelect, CharacterSelect, WeaponSelect, RuneSelect, Ready
    }
}
