using UnityEngine;

public class SideHistory : MonoBehaviour
{
    [Range(1, 5)]
    public uint maxRow = 5;
    public SideData sideData;

    public void ShowKillOnSide(string target, string killer, string[] assists, WeaponType type, bool headshot)
    {
        if (transform.childCount == maxRow)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        GameObject sideDataGO = Instantiate(sideData.gameObject, transform);
        SideData sideDataScript = sideDataGO.GetComponent<SideData>();

        sideDataScript.SetDataSingle(killer, type, target, GetSideType(killer, target, assists), headshot);
    }

    public void ShowKillOnSide(string target, string killer, string[] assists, WeaponType type, TeamColor killerColor, TeamColor targetColor, bool headshot)
    {
        if (transform.childCount == maxRow)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        GameObject sideDataGO = Instantiate(sideData.gameObject, transform);
        SideData sideDataScript = sideDataGO.GetComponent<SideData>();

        sideDataScript.SetDataTeam(killer, type, target, GetSideType(killer, target, assists), killerColor, targetColor, headshot);
    }

    private SideHistoryType GetSideType(string killer, string target, string[] assists)
    {
        string myName = AccountInfo.Instance.Info.PlayerProfile.DisplayName;
        if (target == myName)
        {
            return SideHistoryType.target;
        }
        else if (killer == myName)
        {
            return SideHistoryType.killer;
        }
        else
        {
            for (int i = 0; i < assists.Length; i++)
            {
                if (assists[i] == myName)
                {

                    return  SideHistoryType.assist;
                }
            }
        }

        return SideHistoryType.nothing;
    }
}
