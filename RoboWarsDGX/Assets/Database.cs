using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class Database : MonoBehaviour
{
    private static readonly string itemName = "Items";
    private static readonly string runeName = "Rune";
    private static readonly int costNumber = 1;
    private static readonly int iconNumber = 3;
    private static readonly int prefabNumber = 5;

    private static Database instance;

    [SerializeField]
    private List<CatalogItem> catalogRunes;

    [SerializeField]
    private List<RuneStats> runes;

    public static Database Instance { get => instance; set => instance = value; }
    public List<RuneStats> Runes { get => runes; set => runes = value; }
    public List<CatalogItem> CatalogRunes { get => catalogRunes; set => catalogRunes = value; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void UpdateDatabase()
    {
        GetCatalogItemsRequest request = new GetCatalogItemsRequest()
        {
            CatalogVersion = itemName
        };

        PlayFabClientAPI.GetCatalogItems(request, OnUpdateDatabaseSuccess, OnUpdateDatabaseError);
    }

    private static void OnUpdateDatabaseSuccess(GetCatalogItemsResult result)
    {
        for(int i=0; i<result.Catalog.Count; i++)
        {
            if(result.Catalog[i].ItemClass == runeName)
            {
                Instance.CatalogRunes.Add(result.Catalog[i]);
                Instance.Runes.Add(CreateRune(result.Catalog[i], i));
            }
        }
    }

    private static void OnUpdateDatabaseError(PlayFabError error)
    {
        Debug.Log("OnUpdateDatabaseError:" + error);
    }

    private static RuneStats CreateRune(CatalogItem item, int i)
    {
        RuneStats rune = new RuneStats();
        rune.Index = i;
        rune.Name = item.DisplayName;
        //Debug.Log(GetCatalogCustomData(costNumber, item));
        rune.Cost = int.Parse(GetCatalogCustomData(costNumber, item));
        Sprite icon = Resources.Load(GetCatalogCustomData(iconNumber, item), typeof(Sprite)) as Sprite;
        rune.Icon = icon;
        GameObject prefab = Resources.Load(GetCatalogCustomData(prefabNumber, item), typeof(GameObject)) as GameObject;
        rune.Prefab = prefab;

        return rune;
    }

    private static string GetCatalogCustomData(int i, CatalogItem item)
    {
        string cDataTemp = item.CustomData.Trim();
        cDataTemp = cDataTemp.TrimStart('{');
        cDataTemp = cDataTemp.TrimEnd('}');
        //Debug.Log(cDataTemp);
        string[] newCData = cDataTemp.Split(',', ':');

        for(int j=0; j<newCData.Length; j++)
        {
            Debug.Log(newCData[j]);
            if(i == j)
            {
                newCData[j] = newCData[j].Trim();
                newCData[j] = newCData[j].TrimStart('"');
                newCData[j] = newCData[j].TrimEnd('"');
                newCData[j] = newCData[j].Trim();
                return newCData[j];
            }
        }
        return "ERROR";
    }
}
