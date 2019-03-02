using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStates : MonoBehaviourPun
{
    public CharacterStats characterStat;
    public Text hpText;

    private float health;
    private float armor;

    Hashtable dmgHistory = new Hashtable();

    private void Start()
    {
        if (photonView.IsMine)
        {
            health = characterStat.GetHP();
            armor = characterStat.GetArmor();
            hpText.text = "" + health;
        }
    }

    public void GotShot(float dmg, string playerid)
    {
        if (photonView.IsMine)
        {
            Debug.Log("Got dmg: " + health + " " +dmg + " " + (health-dmg));
            if (health-dmg <= 0)
            {
                AddDmg(health, playerid);
                Die(playerid);
            }
            else
            {
                health -= dmg;
                AddDmg(dmg, playerid);

                hpText.text = "" + health;
            }
        }
    }

    public void HeadShot(string playerid)
    {
        if (photonView.IsMine)
        {
            Debug.Log("Headshot");
            AddDmg(health, playerid);
            Die(playerid);
        }
    }

    private void AddDmg(float dmg, string playerid)
    {

        if (dmgHistory.Contains(playerid))
        {
            dmgHistory[playerid] = (float)dmgHistory[playerid] + dmg;
        }
        else
        {
            dmgHistory.Add(playerid, dmg);
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
