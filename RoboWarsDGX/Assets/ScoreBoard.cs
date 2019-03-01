using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviourPun
{

    public static ScoreBoard Instance = null;

    List<Score> scores = new List<Score>();

    public GameObject ui;
    public Transform scoresUI;

    public GameScore gameScore;

    private void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ui.SetActive(false);
        photonView.RPC("NewPlayer", RpcTarget.AllBuffered, AccountInfo.Instance.Info.PlayerProfile.DisplayName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ui.SetActive(!ui.activeSelf);
        }
    }

    [PunRPC]
    private void NewPlayer(string displayName)
    {
        scores.Add(new Score(displayName));
        Refresh();
    }

    public void Killed(string target, string killer, string[] assists)
    {
        photonView.RPC("KilledRPC", RpcTarget.AllBuffered, target, killer, assists);
    }

    [PunRPC]
    private void KilledRPC(string target, string killer, string[] assists)
    {
        Score targetScore = SearchById(target);
        if (targetScore == null)
        {
            Debug.Log("Bad target id");
        }
        else
        {
            targetScore.deaths++;
        }

        Score killerScore = SearchById(killer);
        if (killerScore == null)
        {
            Debug.Log("Bad killer id");
        }
        else
        {
            killerScore.kills++;
        }

        foreach(string actualAssist in assists)
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

        Refresh();
    }

    private Score SearchById(string id)
    {
        foreach(Score actualScore in scores)
        {
            if(actualScore.displayName == id)
            {
                return actualScore;
            }
        }
        return null;
    }

    private void Refresh()
    {
        scores.Sort();
        for (int i = scoresUI.childCount-1; i>=0; i--)
        {
            Destroy(scoresUI.GetChild(i).gameObject);
        }
        foreach(Score actualScore in scores)
        {
            GameObject gameScoreGO = Instantiate(gameScore.gameObject, scoresUI);
            GameScore gameScoreScript = gameScoreGO.GetComponent<GameScore>();
            gameScoreScript.displayName = actualScore.displayName;
            gameScoreScript.kills = actualScore.kills;
            gameScoreScript.deaths = actualScore.deaths;
            gameScoreScript.assists = actualScore.assists;
            gameScoreScript.Init();
        }
    }

    private class Score : IComparable
    {
        public string displayName;
        public int kills;
        public int deaths;
        public int assists;

        public Score(string displayName)
        {
            this.displayName = displayName;
            kills = 0;
            deaths = 0;
            assists = 0;
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
