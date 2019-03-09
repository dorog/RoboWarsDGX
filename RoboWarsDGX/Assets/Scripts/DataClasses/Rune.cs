using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class Rune
{
    public string id;
    public int price;
    public Sprite icon;
    public int health;
    public int armor;
    public int jumpPower;
    public uint hpReg;
    public int movemenetSpeed;
    public int shotGunDmg;
    public int sniperDmg;
    public int smgDmg;
    public int specialAbilityReduceTime;

    public List<RunStoreDescription> GetData()
    {
        List<RunStoreDescription> runStoreDescriptions = new List<RunStoreDescription>();
        if(health != 0)
        {
            runStoreDescriptions.Add(new RunStoreDescription("Health:", health > 0 ? true : false, ToPresage(health)));
        }
        if (armor != 0)
        {
            runStoreDescriptions.Add(new RunStoreDescription("Armor:", armor > 0 ? true : false, ToPresage(armor)));
        }
        if (jumpPower != 0)
        {
            runStoreDescriptions.Add(new RunStoreDescription("Jump Power:", jumpPower > 0 ? true : false, ToPresageAndPercentForm(jumpPower)));
        }
        if (hpReg != 0)
        {
            runStoreDescriptions.Add(new RunStoreDescription("HP regeneration:", true, "+" + hpReg + "%"));
        }
        if (movemenetSpeed != 0)
        {
            runStoreDescriptions.Add(new RunStoreDescription("Movemenet Speed:", movemenetSpeed > 0 ? true : false, ToPresageAndPercentForm(movemenetSpeed)));
        }
        if (shotGunDmg != 0)
        {
            runStoreDescriptions.Add(new RunStoreDescription("Shotgun dmg:", shotGunDmg > 0 ? true : false, ToPresage(shotGunDmg)));
        }
        if (sniperDmg != 0)
        {
            runStoreDescriptions.Add(new RunStoreDescription("Sniper dmg:", sniperDmg > 0 ? true : false, ToPresage(sniperDmg)));
        }
        if (smgDmg != 0)
        {
            runStoreDescriptions.Add(new RunStoreDescription("Smg dmg:", smgDmg > 0 ? true : false, ToPresage(smgDmg)));
        }
        if (specialAbilityReduceTime != 0)
        {
            runStoreDescriptions.Add(new RunStoreDescription("Special ability time:", specialAbilityReduceTime < 0 ? true : false, ToPresageAndPercentForm(specialAbilityReduceTime)));
        }

        return runStoreDescriptions;
    }

    public Rune()
    {
        health = 0;
        armor = 0;
        jumpPower = 0;
        hpReg = 0;
        movemenetSpeed = 0;
        shotGunDmg = 0;
        sniperDmg = 0;
        smgDmg = 0;
        specialAbilityReduceTime = 0;
    }

    private string ToPresage(int amount)
    {
        string s = amount > 0 ? "+" : "";
        return s + amount;
    }

    private string ToPresageAndPercentForm(int amount)
    {
        string s = amount > 0 ? "+" : "";
        return s + amount + "%";
    }

    public static Rune CatalogItemToRune(CatalogItem item)
    {
        Rune newRune = new Rune();

        string[] splited = SharedData.ParseJson(item.CustomData.ToString());

        uint amount = 0;
        item.VirtualCurrencyPrices.TryGetValue(SharedData.runeVirtualCurrency, out amount);

        newRune.id = item.ItemId;

        InitRune(newRune, splited);

        return newRune;
    }

    private static void InitRune(Rune newRune, string[] splited)
    {
        newRune.icon = Resources.Load<Sprite>("RuneIcons/" + splited[1]);
        newRune.health = int.Parse(splited[3]);
        newRune.armor = int.Parse(splited[5]);
        newRune.jumpPower = int.Parse(splited[7]);
        newRune.hpReg = uint.Parse(splited[9]);
        newRune.movemenetSpeed = int.Parse(splited[11]);
        newRune.shotGunDmg = int.Parse(splited[13]);
        newRune.sniperDmg = int.Parse(splited[15]);
        newRune.smgDmg = int.Parse(splited[17]);
        newRune.specialAbilityReduceTime = int.Parse(splited[19]);
    }

    public void AddStats(Rune rune)
    {
        health += rune.health;
        armor += rune.armor;
        jumpPower += rune.jumpPower;
        hpReg += rune.hpReg;
        movemenetSpeed += rune.movemenetSpeed;
        shotGunDmg += rune.shotGunDmg;
        sniperDmg += rune.sniperDmg;
        smgDmg += rune.smgDmg;
        specialAbilityReduceTime += rune.specialAbilityReduceTime;
    }
}