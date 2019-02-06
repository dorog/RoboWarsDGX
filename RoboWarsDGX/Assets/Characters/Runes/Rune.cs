using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rune
{
    public Sprite icon;
    public int health;                  //Health: +50  6
    public int armor;                   //Armor: +20   5
    public int jumpPower;             //Jump: +5%    4
    public uint hpReg;                   //Hp regeneration: 5%
    public int movemenetSpeed;          //Movement speed: +100%
    public int shotGunDmg;              //Shotgun dmg: +50
    public int sniperDmg;               //Sniper dmg: +50
    public int smgDmg;                  //Smg dmg: +10
    public int specialAbilityReduceTime;//Special ability time: -10%

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
}