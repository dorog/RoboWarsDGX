using UnityEngine;
using UnityEngine.UI;

public class CreateGameUI : MonoBehaviour
{
    private byte maxPlayer = 8;
    private GameMode gameMode = GameMode.DeathMatch;
    private Map map = Map.Desert;

    [Header ("Manager")]
    public MPManager mpManager;

    [Header("Inputs")]
    public InputField roomNameInput;
    public Dropdown maxPlayerDP;
    public Dropdown gameModeDP;
    public Dropdown mapDP;
    public Button createButton;

    [Header ("Game mode strings")]
    private const string deathMatch = "Death Match";
    private const string teamDeathMatch = "Team Death Match";
    private const string battleRoyal = "Battle Royal";

    private void Start()
    {
        roomNameInput.text = "";
        createButton.interactable = false;
    }

    public void CreateGame()
    {
        mpManager.CreateGame(roomNameInput.text, maxPlayer, GetMapSceneName());
    }

    public void ChangedMaxPlayer()
    {
        maxPlayer = byte.Parse(maxPlayerDP.options[maxPlayerDP.value].text);
    }

    public void ChangedGameMode()
    {
        gameMode = GetGameMode(gameModeDP.options[gameModeDP.value].text);
    }

    public void ChangedMap()
    {
        Debug.Log("ChangedMap");
    }

    private GameMode GetGameMode(string gameMode)
    {
        switch (gameMode)
        {
            case deathMatch:
                return GameMode.DeathMatch;
            case teamDeathMatch:
                return GameMode.TeamDeathMatch;
            case battleRoyal:
                return GameMode.BattleRoyal;
            default:
                return GameMode.DeathMatch;
        }
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
        return "Desert";
    }

    private enum GameMode
    {
        DeathMatch, TeamDeathMatch, BattleRoyal
    }

    private enum Map
    {
        Desert
    }
}
