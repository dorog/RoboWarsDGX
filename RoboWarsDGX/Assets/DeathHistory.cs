using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathHistory : MonoBehaviour
{
    [Header("First")]
    public Text firstDmgName;
    public Text firstDmg;

    [Header("Second")]
    public Text secondDmgName;
    public Text secondDmg;

    [Header("Third")]
    public Text thirdDmgName;
    public Text thirdDmg;

    private List<DmgRow> rows = new List<DmgRow>();

    private void OnEnable()
    {
        rows.Clear();
        foreach (var table in SelectData.deathHistory.Keys)
        {
            string dsaf = (string)table;
            float dsafds = (float)SelectData.deathHistory[table];
            rows.Add(new DmgRow((string)table, (float)SelectData.deathHistory[table]));
        }

        rows.Sort();
        int max = (3 - rows.Count);
        for (int i=0; i < max; i++)
        {
            rows.Add(new DmgRow());
        }

        firstDmgName.text = rows[0].GetId();
        firstDmg.text = rows[0].GetDmg();

        secondDmgName.text = rows[1].GetId();
        secondDmg.text = rows[1].GetDmg();

        thirdDmgName.text = rows[2].GetId();
        thirdDmg.text = rows[2].GetDmg();
    }

    private class DmgRow : IComparable
    {
        string id = "";
        float dmg = 0;

        public string GetId()
        {
            return id;
        }

        public string GetDmg()
        {
            if(dmg == 0)
            {
                return "";
            }
            else
            {
                return "" + dmg;
            }
        }

        public int CompareTo(object obj)
        {
            DmgRow orderToCompare = obj as DmgRow;
            if (orderToCompare.dmg > dmg)
            {
                return 1;
            }
            else if (orderToCompare.dmg < dmg)
            {
                return -1;
            }
            else
            {
                return orderToCompare.id.CompareTo(id);
            }
        }

        public DmgRow()
        {
            id = "";
            dmg = 0;
        }

        public DmgRow(string id, float dmg)
        {
            this.id = id;
            this.dmg = dmg;
        }
    }
}
