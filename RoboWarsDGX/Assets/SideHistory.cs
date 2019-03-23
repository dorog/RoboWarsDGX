using UnityEngine;

public class SideHistory : MonoBehaviour
{
    [Range(1, 5)]
    public uint maxRow = 5;
    public SideData sideData;

    public void ShowKillOnSide(string target, string killer, string[] assists, WeaponType type)
    {
        if (transform.childCount == maxRow)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        GameObject sideDataGO = Instantiate(sideData.gameObject, transform);
        SideData sideDataScript = sideDataGO.GetComponent<SideData>();

        SideHistoryType myState = SideHistoryType.nothing;
        string myName = AccountInfo.Instance.Info.PlayerProfile.DisplayName;
        if (target == myName)
        {
            myState = SideHistoryType.target;
        }
        else if (killer == myName)
        {
            myState = SideHistoryType.killer;
        }
        else
        {
            for (int i = 0; i < assists.Length; i++)
            {
                if (assists[i] == myName)
                {

                    myState = SideHistoryType.assist;
                    break;
                }
            }
        }

         sideDataScript.SetData(killer, type, target, myState);
        }
    }
