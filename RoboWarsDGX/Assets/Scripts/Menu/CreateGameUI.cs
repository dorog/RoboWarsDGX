using UnityEngine;
using UnityEngine.UI;

public class CreateGameUI : MonoBehaviour
{
    private byte maxPlayer = 8;
    private string gameMode = SharedData.deathMatch;
    private SpawnMode spawnMode = SpawnMode.DistanceBased;
    private string map = SharedData.desertMap;
    private float timeOpt = 600f;

    [Header("Inputs")]
    public InputField roomNameInput;
    public Dropdown maxPlayerDP;
    public Dropdown time;
    public Dropdown gameModeDP;
    public Dropdown mapDP;
    public Dropdown spawnModeDP;
    public Button createButton;

    private string[] gameModes = { SharedData.deathMatch, SharedData.teamDeathMatch, SharedData.battleRoyal, SharedData.searchAndDestroy };
    private SpawnMode[] spawnModes = { SpawnMode.DistanceBased, SpawnMode.AreaBased, SpawnMode.Random };
    private float[] timeOpts = { 300f, 600f, 1200f, 1800f};

    private void Start()
    {
        roomNameInput.text = "";
        createButton.interactable = false;
    }

    public void CreateGame()
    {
        PhotonLobby.Instance.CreateRoom(roomNameInput.text, maxPlayer, map, gameMode, (int)spawnMode, timeOpt);
    }

    public void ChangedMaxPlayer()
    {
        maxPlayer = byte.Parse(maxPlayerDP.options[maxPlayerDP.value].text);
    }

    public void ChangedGameMode()
    {
        gameMode = gameModes[gameModeDP.value];
    }

    public void ChangeSpawnMode()
    {
        spawnMode = spawnModes[spawnModeDP.value];
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

    public void ChangeTime()
    {
        timeOpt = timeOpts[time.value];
    }

    private string GetMapSceneName()
    {
        return SharedData.desertMap;
    }
}
