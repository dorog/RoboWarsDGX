using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class Database : MonoBehaviour
{
    private static readonly string runeStoreName = "RuneStore";
    private static readonly string characterStoreName = "CharacterStore";

    private static readonly string itemName = "Items";
    private static readonly string runeName = "Rune";
    /*private static readonly int costNumber = 1;
    private static readonly int iconNumber = 3;
    private static readonly int prefabNumber = 5;*/

    private static readonly int typeNumber = 1;

    private static Database instance;


    /*[SerializeField]
    private List<RuneStats> runes;*/

    /*[SerializeField]
    private List<StoreItem> chestStoreItems;*/

    [SerializeField]
    private List<StoreItem> runeStoreItems;

    [SerializeField]
    private List<StoreItem> characterStoreItems;

    [SerializeField]
    private List<StoreItem> notOwnedCharacterStoreItems = new List<StoreItem>();

    [SerializeField]
    private List<StoreItem> notOwnedRuneStoreItems = new List<StoreItem>();

    public List<StoreItem> NotOwnedRuneStoreItems { get => notOwnedRuneStoreItems; set => notOwnedRuneStoreItems = value;  }

    public static Database Instance { get => instance; set => instance = value; }
    /*public List<RuneStats> Runes { get => runes; set => runes = value; }*/
    /*public List<StoreItem> ChestStoreItems { get => chestStoreItems; set => chestStoreItems = value; }*/

    public List<StoreItem> RuneStoreItems { get => runeStoreItems; set => runeStoreItems = value; }
    public List<StoreItem> CharacterStoreItems { get => characterStoreItems; set => characterStoreItems = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
            UpdateDatabase();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private static void UpdateDatabase()
    {
        /*GetCatalogItemsRequest request = new GetCatalogItemsRequest()
        {
            CatalogVersion = itemName
        };

        PlayFabClientAPI.GetCatalogItems(request, UpdateDataBaseSuccess, UpdateDataBaseFail);*/
        GetStoreItems(runeStoreName);
        GetStoreItems(characterStoreName);
    }



    private static void UpdateDataBaseSuccess(GetCatalogItemsResult result)
    {
        /*GetStoreItems(runeStoreName);
        GetStoreItems(characterStoreName);*/
    }


    private static void UpdateDataBaseFail(PlayFabError error)
    {
        Debug.Log("OnUpdateDatabaseError:" + error);
    }

    /*private static RuneStats CreateRune(CatalogItem item, int i)
    {
        RuneStats rune = new RuneStats();
        rune.Index = i;
        rune.Name = item.DisplayName;
        rune.Cost = int.Parse(GetCatalogCustomData(costNumber, item));
        Sprite icon = Resources.Load(GetCatalogCustomData(iconNumber, item), typeof(Sprite)) as Sprite;
        rune.Icon = icon;
        GameObject prefab = Resources.Load(GetCatalogCustomData(prefabNumber, item), typeof(GameObject)) as GameObject;
        rune.Prefab = prefab;

        return rune;
    }*/

    private static string GetStoreCustomData(int i, StoreItem item)
    {
        string cDataTemp = item.CustomData.ToString();
        cDataTemp = cDataTemp.TrimStart('{');
        cDataTemp = cDataTemp.TrimEnd('}');
        string[] newCData = cDataTemp.Split(',', ':');

        for (int j = 0; j < newCData.Length; j++)
        {
            if (i == j)
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

    static void GetStoreItems(string id)
    {
        GetStoreItemsRequest request = new GetStoreItemsRequest()
        {
            CatalogVersion = itemName,
            StoreId = id
        };

        PlayFabClientAPI.GetStoreItems(request, GetStoreItemsSuccess, GetStoreItemsFail);
    }

    static void GetStoreItemsSuccess(GetStoreItemsResult result)
    {
        if (result.StoreId == runeStoreName)
        {
            Instance.RuneStoreItems = result.Store;
            ClearOwnedItems(runeStoreName);
        }

        if (result.StoreId == characterStoreName)
        {
            Instance.CharacterStoreItems = result.Store;
            ClearOwnedItems(characterStoreName);
        }

        /*if(result.StoreId == "RuneStore")
        {
            Instance.runeStoreItems = result.Store;
        }*/
        /*if (result.StoreId == "ChestStore")
        {
            Instance.chestStoreItems = result.Store;
        }*/
    }

    static void GetStoreItemsFail(PlayFabError error)
    {
        Debug.Log("GetStoreItemsFail");
    }

    static private void ClearOwnedItems(string storeName)
    {
        AccountInfo accountInfo = FindObjectOfType<AccountInfo>();
        if (accountInfo == null)
        {
            Debug.Log("AccountInfo is null!");
            return;
        }
        if (storeName == characterStoreName)
        {
            for (int i = 0; i < Instance.CharacterStoreItems.Count; i++)
            {
                string type = GetStoreCustomData(typeNumber, Instance.CharacterStoreItems[i]);
                if (!accountInfo.IsOwnedCharacter(SharedData.CharacterStringToEnum(type)))
                {
                    Instance.notOwnedCharacterStoreItems.Add(Instance.CharacterStoreItems[i]);
                }
            }

        }

        if (storeName == runeStoreName)
        {
            for(int i=0; i<Instance.RuneStoreItems.Count; i++)
            {
                //string type = GetStoreCustomData(typeNumber, Instance.RuneStoreItems[i]);
                if (!accountInfo.IsOwnedRune(SharedData.RuneStringToEnum(Instance.RuneStoreItems[i].ItemId)))
                {
                    Instance.notOwnedRuneStoreItems.Add(Instance.RuneStoreItems[i]);
                }
            }
        }
    }
}
