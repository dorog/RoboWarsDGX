using Photon.Pun;
using System.Collections;
using UnityEngine;

public class CharacterStates : MonoBehaviourPun
{
    public CharacterStats characterStat;

    private float health;
    private float armor;

    Hashtable dmgHistory = new Hashtable();

    private void Start()
    {
        if (photonView.IsMine)
        {
            health = characterStat.GetHP();
            armor = characterStat.GetArmor();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            dmgHistory.Clear();

            float dmg = 60f;
            dmgHistory.Add("test1", dmg);
            Die("test1");
        }
    }

    public void GotShot(float dmg, string playerid)
    {
        if (photonView.IsMine)
        {
            health -= dmg;
            if (health <= 0)
            {
                Die(playerid);
            }

            if (dmgHistory.Contains(playerid))
            {
                dmgHistory[playerid] = (float)dmgHistory[playerid] + dmg;
            }
            else
            {
                dmgHistory.Add(playerid, dmg);
            }
        }
    }

    private void Die(string killer)
    {
        PhotonView view = GetComponent<PhotonView>();

        string[] assists = new string[dmgHistory.Count-1];
        int i = 0;
        foreach (string key in dmgHistory.Keys)
        {
            if(key != killer)
            {
                assists[i] = key;
                i++;
            }
        }

        ScoreBoard.Instance.Killed(AccountInfo.Instance.Info.PlayerProfile.DisplayName, killer, assists);
        SelectData.deathHistory = dmgHistory;
        GameModeManager.Instance.ShowLobby();
        PhotonNetwork.Destroy(view);
    }
}
