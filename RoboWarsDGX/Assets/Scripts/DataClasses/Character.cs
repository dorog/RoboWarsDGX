﻿using PlayFab.ClientModels;
using UnityEngine;

public class Character
{
    public string id;
    public int price;
    public Sprite icon;
    public int health;
    public int armor;
    public float jumpPower;
    public int hpReg;
    public int movementSpeed;
    public int shotGunDmg;
    public int sniperDmg;
    public int smgDmg;
    public string type;

    public static Character CatalogItemToCharacter(CatalogItem item)
    {
        Character newCharacter = new Character();

        string[] splited = SharedData.ParseJson(item.CustomData.ToString());

        uint amount = 0;
        item.VirtualCurrencyPrices.TryGetValue(SharedData.characterVirtualCurrency, out amount);

        newCharacter.id = item.ItemId;

        InitCharacter(newCharacter, splited);

        return newCharacter;
    }

    private static void InitCharacter(Character character, string[] splited)
    {
        character.icon = Resources.Load<Sprite>("CharacterIcons/" + splited[1]);
        character.health = int.Parse(splited[3]);
        character.armor = int.Parse(splited[5]);
        character.jumpPower = int.Parse(splited[7]);
        character.hpReg = int.Parse(splited[9]);
        character.movementSpeed = int.Parse(splited[11]);
        character.shotGunDmg = int.Parse(splited[13]);
        character.sniperDmg = int.Parse(splited[15]);
        character.smgDmg = int.Parse(splited[17]);
        character.type = splited[19];
    }
}


