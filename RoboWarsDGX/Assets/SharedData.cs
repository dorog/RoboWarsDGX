using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedData
{
    public static readonly string loginScene = "Login";
    public static readonly string menuScene = "Menu";

    //Character types
    //public static readonly int characterNumber = 2;

    /*public static readonly string rusher = "Rusher";
    public static readonly string rusherDisplayName = "Amy";

    public static readonly string booster = "Booster";
    public static readonly string boosterDisplayName = "Alex";*/

    private const string rusherName = "Rusher";
    private const string boosterName = "Booster";

    public static CharacterType CharacterStringToEnum(string type)
    {
        switch (type)
        {
            case rusherName:
                return CharacterType.Rusher;
            case boosterName:
                return CharacterType.Booster;
            default:
                return CharacterType.Null;
        }
    }

    public static RuneType RuneStringToEnum(string type)
    {
        switch (type)
        {
            default:
                return RuneType.Null;
        }
    }
}
