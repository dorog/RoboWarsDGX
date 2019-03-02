using PlayFab.ClientModels;
using UnityEngine;
using System;

[Serializable]
public class Character
{
    public string id;
    public int price;
    public Sprite icon;
    public float health;
    public int armor;
    public float jumpPower;
    public int hpReg;
    public int movementSpeed;
    public int shotGunDmg;
    public int sniperDmg;
    public int smgDmg;
    public string type;
    public GameObject prefab;
    public GameObject previewPrefab;

    public float footIntensity;
    public float downLegIntensity;
    public float upLegIntensity;
    public float spineIntensity;
    public float chestIntensity;
    public float upArmIntensity;
    public float downArmIntensity;

    public static Character CatalogItemToCharacter(CatalogItem item)
    {
        Character newCharacter = new Character();

        string[] splited = SharedData.ParseJson(item.CustomData.ToString());

        newCharacter.id = item.ItemId;

        InitCharacter(newCharacter, splited);

        return newCharacter;
    }

    private static void InitCharacter(Character character, string[] splited)
    {
        character.icon = Resources.Load<Sprite>("CharacterIcons/" + splited[1]);
        character.health = float.Parse(splited[3]);
        character.armor = int.Parse(splited[5]);
        character.jumpPower = int.Parse(splited[7]);
        character.hpReg = int.Parse(splited[9]);
        character.movementSpeed = int.Parse(splited[11]);
        character.shotGunDmg = int.Parse(splited[13]);
        character.sniperDmg = int.Parse(splited[15]);
        character.smgDmg = int.Parse(splited[17]);
        character.type = splited[19];
        character.prefab = Resources.Load<GameObject>("Characters/" + splited[21]);
        character.previewPrefab = Resources.Load<GameObject>("CharactersPreview/" + splited[21]);

        character.footIntensity = int.Parse(splited[23]);
        character.downLegIntensity = float.Parse(splited[25]) / 100;
        character.upLegIntensity = float.Parse(splited[27]) / 100;
        character.spineIntensity = float.Parse(splited[29]) / 100;
        character.chestIntensity = float.Parse(splited[31]) / 100;
        character.upArmIntensity = float.Parse(splited[33]) / 100;
        character.downArmIntensity = float.Parse(splited[35]) / 100;
    }
}


