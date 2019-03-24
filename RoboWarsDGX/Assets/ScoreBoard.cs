using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviourPun
{
    public static ScoreBoard Instance = null;

    List<Score> scores = new List<Score>();

    private GameObject ui;
    private GameMode gameMode;

    public GameScore gameScore;

    [Header ("Side history")]
    public SideHistory sideHistory;

    [Header("Single ScoreBoard")]
    public GameObject singleUI;
    public Transform singleScoresUI;

    [Header("Team ScoreBoard")]
    public GameObject teamUI;
    public Transform redTeamScoresUI;
    public Transform blueTeamScoresUI;

    private delegate void RefreshFunction();
    private event RefreshFunction Refresh;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        singleUI.SetActive(false);
        teamUI.SetActive(false);

        Room currentRoom = PhotonNetwork.CurrentRoom;
        gameMode = SharedData.GetGameMode((string)currentRoom.CustomProperties[SharedData.GameModeKey]);

        switch (gameMode)
        {
            case GameMode.DeathMatch:
                ui = singleUI;
                Refresh += RefreshSingle;
                break;
            case GameMode.TeamDeathMatch:
                ui = teamUI;
                Refresh += RefreshTeam;
                break;
            default:
                break;
        }

        if (!PhotonNetwork.IsMasterClient)
        {
            switch (gameMode)
            {
                case GameMode.DeathMatch:
                    photonView.RPC("NewPlayerSingleGame", RpcTarget.MasterClient, AccountInfo.Instance.Info.PlayerProfile.DisplayName);
                    break;
                case GameMode.TeamDeathMatch:
                    photonView.RPC("NewPlayerTeamGame", RpcTarget.MasterClient, AccountInfo.Instance.Info.PlayerProfile.DisplayName, (int)SelectData.teamColor);
                    break;
                default:
                    break;
            }
        }
        else
        {
            scores.Add(new Score(AccountInfo.Instance.Info.PlayerProfile.DisplayName));
            Refresh();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ui.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            ui.SetActive(false);
        }
    }

    [PunRPC]
    private void NewPlayerSingleGame(string displayName)
    {
        if (!SearchByDisplayname(displayName)) {
            scores.Add(new Score(displayName));
        }

        Send();
    }

    [PunRPC]
    private void NewPlayerTeamGame(string displayName, int color)
    {
        if (!SearchByDisplayname(displayName))
        {
            scores.Add(new Score(displayName, (TeamColor)color));
        }
        else
        {
            Score score = SearchById(displayName);
            score.color = (TeamColor)color;
        }

        Send();
    }

    [PunRPC]
    private void RefreshScoreBoardTeam(string[] displayNames, int[] kills, int[] deaths, int[] assists, int[] colors)
    {
        scores.Clear();
        for (int i = 0; i < displayNames.Length; i++)
        {
            scores.Add(new Score(displayNames[i], kills[i], deaths[i], assists[i], (TeamColor)colors[i]));
        }

        Refresh();
    }

    [PunRPC]
    private void RefreshScoreBoardSingle(string[] displayNames, int[] kills, int[] deaths, int[] assists)
    {
        scores.Clear();
        for (int i = 0; i < displayNames.Length; i++)
        {
            scores.Add(new Score(displayNames[i], kills[i], deaths[i], assists[i]));
        }

        Refresh();
    }

    public void Killed(string target, string killer, string[] assists, WeaponType type)
    {
        photonView.RPC("KilledRPC", RpcTarget.All, target, killer, assists, (int)type);
    }

    [PunRPC]
    private void KilledRPC(string target, string killer, string[] assists, int type)
    {
        int correctData = 0;
        Score targetScore = SearchById(target);
        if (targetScore == null)
        {
            Debug.Log("Bad target id");
        }
        else
        {
            targetScore.deaths++;
            correctData++;
        }

        Score killerScore = SearchById(killer);
        if (killerScore == null)
        {
            Debug.Log("Bad killer id");
        }
        else
        {
            killerScore.kills++;
            correctData++;
        }

        foreach (string actualAssist in assists)
        {
            Score assist = SearchById(actualAssist);
            if (assist == null)
            {
                Debug.Log("Bad assist id");
            }
            else
            {
                assist.assists++;
            }
        }

        if(correctData == 2)
        {
            switch (gameMode)
            {
                case GameMode.DeathMatch:
                    sideHistory.ShowKillOnSide(target, killer, assists, (WeaponType)type);
                    break;
                case GameMode.TeamDeathMatch:
                    sideHistory.ShowKillOnSide(target, killer, assists, (WeaponType)type, killerScore.color, targetScore.color);
                    break;
                default:
                    break;
            }
        }

        Refresh();
    }

    private void RefreshSingle()
    {
        scores.Sort();
        for (int i = singleScoresUI.childCount - 1; i >= 0; i--)
        {
            Destroy(singleScoresUI.GetChild(i).gameObject);
        }
        foreach (Score actualScore in scores)
        {

            GameObject gameScoreGO = Instantiate(gameScore.gameObject, singleScoresUI);
            GameScore gameScoreScript = gameScoreGO.GetComponent<GameScore>();
            gameScoreScript.displayName = actualScore.displayName;
            gameScoreScript.kills = actualScore.kills;
            gameScoreScript.deaths = actualScore.deaths;
            gameScoreScript.assists = actualScore.assists;
            if (actualScore.displayName == AccountInfo.Instance.Info.PlayerProfile.DisplayName)
            {
                gameScoreScript.ChangeColor();
            }
            gameScoreScript.Init();
        }
    }

    private void RefreshTeam()
    {
        scores.Sort();
        for (int i = redTeamScoresUI.childCount - 1; i >= 0; i--)
        {
            Destroy(redTeamScoresUI.GetChild(i).gameObject);
        }
        for (int i = blueTeamScoresUI.childCount - 1; i >= 0; i--)
        {
            Destroy(blueTeamScoresUI.GetChild(i).gameObject);
        }

        foreach (Score actualScore in scores)
        {
            GameObject gameScoreGO;
            if (actualScore.color == TeamColor.Red)
            {
                gameScoreGO = Instantiate(gameScore.gameObject, redTeamScoresUI);
            }
            else
            {
                gameScoreGO = Instantiate(gameScore.gameObject, blueTeamScoresUI);
            }
            GameScore gameScoreScript = gameScoreGO.GetComponent<GameScore>();
            gameScoreScript.displayName = actualScore.displayName;
            gameScoreScript.kills = actualScore.kills;
            gameScoreScript.deaths = actualScore.deaths;
            gameScoreScript.assists = actualScore.assists;
            if (actualScore.displayName == AccountInfo.Instance.Info.PlayerProfile.DisplayName)
            {
                gameScoreScript.ChangeColor();
            }
            gameScoreScript.Init();
        }
    }

    public void ChangeTeam(string displayName, TeamColor color)
    {
        photonView.RPC("ChangeTeamRPC", RpcTarget.All, displayName, (int)color);
    }

    [PunRPC]
    private void ChangeTeamRPC(string displayName, int color)
    {
        Score changedScore = SearchById(displayName);
        changedScore.color = (TeamColor)color;

        Refresh();
    }

    private Score SearchById(string id)
    {
        foreach (Score actualScore in scores)
        {
            if (actualScore.displayName == id)
            {
                return actualScore;
            }
        }
        return null;
    }

    private bool SearchByDisplayname(string id)
    {
        foreach (Score actualScore in scores)
        {
            if (actualScore.displayName == id)
            {
                return true;
            }
        }
        return false;
    }

    private void Send()
    {
        string[] displayNames = new string[scores.Count];
        int[] kills = new int[scores.Count];
        int[] deaths = new int[scores.Count];
        int[] assists = new int[scores.Count];
        int[] colors = new int[scores.Count];

        for (int i = 0; i < scores.Count; i++)
        {
            displayNames[i] = scores[i].displayName;
            kills[i] = scores[i].kills;
            deaths[i] = scores[i].deaths;
            assists[i] = scores[i].assists;
            colors[i] = (int)scores[i].color;
        }

        switch (gameMode)
        {
            case GameMode.DeathMatch:
                photonView.RPC("RefreshScoreBoardSingle", RpcTarget.Others, displayNames, kills, deaths, assists);
                break;
            case GameMode.TeamDeathMatch:
                photonView.RPC("RefreshScoreBoardTeam", RpcTarget.Others, displayNames, kills, deaths, assists, colors);
                break;
            default:
                break;
        }

        Refresh();
    }

    private class Score : IComparable
    {
        public string displayName;
        public int kills;
        public int deaths;
        public int assists;
        public TeamColor color;

        public Score(string displayName)
        {
            this.displayName = displayName;
            kills = 0;
            deaths = 0;
            assists = 0;
        }

        public Score(string displayName, TeamColor color)
        {
            this.displayName = displayName;
            kills = 0;
            deaths = 0;
            assists = 0;
            this.color = color;
        }

        public Score(string displayName, int kills, int deaths, int assists)
        {
            this.displayName = displayName;
            this.kills = kills;
            this.deaths = deaths;
            this.assists = assists;
        }

        public Score(string displayName, int kills, int deaths, int assists, TeamColor color)
        {
            this.displayName = displayName;
            this.kills = kills;
            this.deaths = deaths;
            this.assists = assists;
            this.color = color;
        }

        public int CompareTo(object obj)
        {
            Score orderToCompare = obj as Score;
            if (orderToCompare.kills > kills)
            {
                return 1;
            }
            else if (orderToCompare.kills < kills)
            {
                return -1;
            }
            else
            {
                if (orderToCompare.assists > assists)
                {
                    return 1;
                }
                else if (orderToCompare.assists < assists)
                {
                    return -1;
                }
                else
                {
                    if (orderToCompare.deaths < deaths)
                    {
                        return 1;
                    }
                    else if (orderToCompare.deaths > deaths)
                    {
                        return -1;
                    }
                    else
                    {
                        return orderToCompare.displayName.CompareTo(displayName);
                    }
                }
            }
        }
    }
}
