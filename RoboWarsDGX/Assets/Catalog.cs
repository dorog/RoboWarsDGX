using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using System;

public class Catalog
{
    static private readonly string catalogVersion = "Items";


    public delegate void ListsReady();
    public event ListsReady CatalogInitReadyEvent;

    private List<Rune> runes = new List<Rune>();
    private List<Character> characters = new List<Character>();
    private List<Weapon> weapons = new List<Weapon>();

    public List<Rune> notOwnedRunes = new List<Rune>();
    public List<Character> notOwnedCharacters = new List<Character>();
    public List<Weapon> notOwnedWeapons = new List<Weapon>();

    public void CatalogInit()
    {
        GetCatalogItemsRequest request = new GetCatalogItemsRequest()
        {
            CatalogVersion = catalogVersion
        };

        PlayFabClientAPI.GetCatalogItems(request, CatalogInitSuccess, CatalogInitFail);
    }

    private void CatalogInitSuccess(GetCatalogItemsResult result)
    {
        for(int i = 0; i< result.Catalog.Count; i++)
        {
            if(result.Catalog[i].ItemClass == SharedData.runeClass)
            {
                Rune rune = Rune.CatalogItemToRune(result.Catalog[i]);
                runes.Add(rune);
                notOwnedRunes.Add(rune);
            }
            else if(result.Catalog[i].ItemClass == SharedData.characterClass)
            {
                Character character = Character.CatalogItemToCharacter(result.Catalog[i]);
                characters.Add(character);
                notOwnedCharacters.Add(character);
            }
            else if (result.Catalog[i].ItemClass == SharedData.weaponClass)
            {
                Debug.Log("Dont implemented!");
            }
        }

        CatalogInitReadyEvent();
    }

    private void CatalogInitFail(PlayFabError error)
    {
        Debug.Log(error);
    }

    public Rune GetRuneById(string id)
    {
        for(int i=0; i<runes.Count; i++)
        {
            if(runes[i].id == id)
            {
                return runes[i];
            }
        }
        return null;
    }

    public void RegistRuneForOwn(string id)
    {
        for (int i = 0; i < notOwnedRunes.Count; i++)
        {
            if (notOwnedRunes[i].id == id)
            {
                notOwnedRunes.RemoveAt(i);
                return;
            }
        }
    }

    public Character GetCharacterById(string id)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].id == id)
            {
                return characters[i];
            }
        }
        return null;
    }

    public void RegistCharacterForOwn(string id)
    {
        for (int i = 0; i < notOwnedCharacters.Count; i++)
        {
            if (notOwnedCharacters[i].id == id)
            {
                notOwnedCharacters.RemoveAt(i);
                return;
            }
        }
    }
}
