using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class Store
{
    public delegate void InitReady();
    public event InitReady InitReadyEvent;

    public delegate void ListsReady();
    public event ListsReady ListsReadyEvent;

    private int db = 3;

    public readonly List<Rune> runes = new List<Rune>();
    private List<StoreData> runesStoreData = new List<StoreData>();

    public readonly List<Character> characters = new List<Character>();
    private List<StoreData> charactersStoreData = new List<StoreData>();

    public readonly List<Weapon> weapons = new List<Weapon>();
    private List<StoreData> weaponsStoreData = new List<StoreData>();

    public void InitStore()
    {
        db = 3;
        GetStoreItems(SharedData.runeStoreName);
        GetStoreItems(SharedData.characterStoreName);
        GetStoreItems(SharedData.weaponStoreName);
    }

    private void GetStoreItems(string id)
    {
        GetStoreItemsRequest request = new GetStoreItemsRequest()
        {
            CatalogVersion = SharedData.itemName,
            StoreId = id
        };

        PlayFabClientAPI.GetStoreItems(request, GetStoreItemsSuccess, GetStoreItemsFail);
    }

    private void GetStoreItemsSuccess(GetStoreItemsResult result)
    {
        if (result.StoreId == SharedData.runeStoreName)
        {
            for (int i = 0; i < result.Store.Count; i++)
            {
                StoreData storeData = new StoreData();
                storeData.id = result.Store[i].ItemId;

                result.Store[i].VirtualCurrencyPrices.TryGetValue(SharedData.runeVirtualCurrency, out uint amount);
                storeData.price = unchecked((int)amount);

                runesStoreData.Add(storeData);
            }
        }
        else if (result.StoreId == SharedData.characterStoreName)
        {
            for (int i = 0; i < result.Store.Count; i++)
            {
                StoreData storeData = new StoreData();
                storeData.id = result.Store[i].ItemId;

                result.Store[i].VirtualCurrencyPrices.TryGetValue(SharedData.characterVirtualCurrency, out uint amount);
                storeData.price = unchecked((int)amount);

                charactersStoreData.Add(storeData);
            }
        }
        else if (result.StoreId == SharedData.weaponStoreName)
        {
            for (int i = 0; i < result.Store.Count; i++)
            {
                StoreData storeData = new StoreData();
                storeData.id = result.Store[i].ItemId;

                result.Store[i].VirtualCurrencyPrices.TryGetValue(SharedData.weaponVirtualCurrency, out uint amount);
                storeData.price = unchecked((int)amount);

                weaponsStoreData.Add(storeData);
            }
        }
        db--;
        if (db == 0)
        {
            InitReadyEvent();
        }
    }

    static void GetStoreItemsFail(PlayFabError error)
    {
        Debug.Log("GetStoreItemsFail");
    }

    public void CreateLists(List<Rune> notOwnedRunes, List<Character> notOwnedCharacters, List<Weapon> notOwnedWeapons)
    {
        for(int i=0; i<notOwnedRunes.Count; i++)
        {
            StoreData storeData = RuneInStore(notOwnedRunes[i].id);
            if(storeData != null)
            {
                notOwnedRunes[i].price = storeData.price;
                runes.Add(notOwnedRunes[i]);
            }
        }

        for (int i=0; i<notOwnedCharacters.Count; i++)
        {
            StoreData storeData = CharacterInStore(notOwnedCharacters[i].id);
            if (storeData != null)
            {
                notOwnedCharacters[i].price = storeData.price;
                characters.Add(notOwnedCharacters[i]);
            }
        }

        Debug.Log("Weapon list init is missing!");

        ListsReadyEvent();
    }

    private StoreData RuneInStore(string id)
    {
        for(int i=0; i < runesStoreData.Count; i++)
        {
            if(id == runesStoreData[i].id)
            {
                return runesStoreData[i];
            }
        }
        return null;
    }

    private StoreData CharacterInStore(string id)
    {
        for (int i = 0; i < charactersStoreData.Count; i++)
        {
            if (id == charactersStoreData[i].id)
            {
                return charactersStoreData[i];
            }
        }
        return null;
    }

    public void RemoveRune(string id)
    {
        for(int i=0; i<runes.Count; i++)
        {
            if (runes[i].id == id)
            {
                runes.RemoveAt(i);
                return;
            }
        }
    }

    private class StoreData
    {
        public string id;
        public int price;
    }
}
