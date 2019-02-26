using UnityEngine;
using UnityEngine.UI;

public class CreateGameUI : MonoBehaviour
{
    private byte maxPlayer = 8;
    private string gameMode = SharedData.deathMatch;
    private string map = SharedData.desertMap;

    [Header("Inputs")]
    public InputField roomNameInput;
    public Dropdown maxPlayerDP;
    public Dropdown gameModeDP;
    public Dropdown mapDP;
    public Button createButton;

    private string[] gameModes = { SharedData.deathMatch, SharedData.teamDeathMatch, SharedData.battleRoyal };

    private void Start()
    {
        roomNameInput.text = "";
        createButton.interactable = false;
    }

    public void CreateGame()
    {
        PhotonLobby.Instance.CreateRoom(roomNameInput.text, maxPlayer, map, gameMode);
    }

    public void ChangedMaxPlayer()
    {
        maxPlayer = byte.Parse(maxPlayerDP.options[maxPlayerDP.value].text);
    }

    public void ChangedGameMode()
    {
        gameMode = gameModes[gameModeDP.value];
    }

    public void ChangedMap()
    {
        Debug.Log("ChangedMap");
    }

    public void ChangedRoomName()
    {
        if (roomNameInput.text.Length == 0)
        {
            createButton.interactable = false;
        }
        else
        {
            createButton.interactable = true;
        }
    }

    private string GetMapSceneName()
    {
        return SharedData.desertMap;
    }
}
