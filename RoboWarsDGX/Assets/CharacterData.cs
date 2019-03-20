using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterData : MonoBehaviourPun
{
    public CharacterStats characterStat;
    public Text hpText;

    private float health;
    private float armor;

    Hashtable dmgHistory = new Hashtable();

    [Header ("Blood settings")]
    public Transform[] collideredBodyParts;
    public GameObject wound;

    private void Start()
    {
        if (photonView.IsMine)
        {
            health = characterStat.GetHP();
            armor = characterStat.GetArmor();
            hpText.text = "" + health;
        }
    }

    public void GotShot(float dmg, string playerid, Bones bone)
    {
        if(bone == Bones.Head)
        {
            photonView.RPC("HeadShotRPC", RpcTarget.All, playerid);
        }
        else
        {
            photonView.RPC("SimpleShotRPC", RpcTarget.All, dmg, playerid, (int)bone);
        }
    }

    public void GotShotByMoreBones(float dmg, string playerid, List<Bones> bones)
    {
        int[] forRpc = new int[bones.Count];
        for (int i=0; i<bones.Count; i++)
        {
            if(bones[i] == Bones.Head)
            {
                photonView.RPC("HeadShotRPC", RpcTarget.All, playerid);
                return;
            }
            else
            {
                forRpc[i] = (int)bones[i];
            }
        }

        photonView.RPC("ShotgunShotRPC", RpcTarget.All, dmg, playerid, forRpc);
    }

    [PunRPC]
    private void ShotgunShotRPC(float dmg, string playerid, int[] bones)
    {
        if (photonView.IsMine)
        {
            float realDmg = 0;
            for(int i=0; i< bones.Length; i++)
            {
                realDmg += characterStat.GetBoneIntensity((Bones)bones[i]) * dmg;
            }
            if (health - realDmg <= 0)
            {
                AddDmg(health, playerid);
                Die(playerid);
            }
            else
            {
                health -= realDmg;
                AddDmg(realDmg, playerid);
                hpText.text = "" + health;
            }
        }
    }

    [PunRPC]
    private void SimpleShotRPC(float dmg, string playerid, int bone)
    {
        if (photonView.IsMine)
        {
            float realDmg = characterStat.GetBoneIntensity((Bones)bone) * dmg;
            if (health - realDmg <= 0)
            {
                AddDmg(health, playerid);
                Die(playerid);
            }
            else
            {
                health -= realDmg;
                AddDmg(realDmg, playerid);
                hpText.text = "" + health;
            }
        }
    }

    [PunRPC]
    private void HeadShotRPC(string playerid)
    {
        if (photonView.IsMine)
        {
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
        GameModeManager.Instance.Died();
        PhotonNetwork.Destroy(view);
    }

    public void SpawnWound(Vector3 hit, Vector3 normal, Transform colliderTransform)
    {
        int partNumber = -1;
        for(int i=0; i<collideredBodyParts.Length; i++)
        {
            if(colliderTransform == collideredBodyParts[i])
            {
                partNumber = i;
            }
        }
        if(partNumber == -1)
        {
            Debug.Log("Not founeded");
            return;
        }
        photonView.RPC("SpawnWoundRPC", RpcTarget.All, hit.x, hit.y, hit.z, normal.x, normal.y, normal.z, partNumber);
    }

    [PunRPC]
    private void SpawnWoundRPC(float hitX, float hitY, float hitZ, float normalX, float normalY, float normalZ, int partNumber)
    {
        Vector3 hit = new Vector3(hitX, hitY, hitZ);
        Vector3 normal = new Vector3(normalX, normalY, normalZ);
        GameObject spawnedWound = Instantiate(wound, hit, Quaternion.LookRotation(normal));
        spawnedWound.transform.SetParent(collideredBodyParts[partNumber]);
    }
}
