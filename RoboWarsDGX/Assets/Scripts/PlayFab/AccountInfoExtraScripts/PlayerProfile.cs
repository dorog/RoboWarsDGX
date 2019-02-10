﻿using PlayFab.ClientModels;

public class PlayerProfile
{
    //Profile data
    public static int choosedCharacterSlot = 0;
    public static int gold = 0;
    public static int experience = 0;
    public static ProfileStats profileStats;
    //

    public void InitProfile(GetPlayerCombinedInfoResultPayload info)
    {
        if (info != null)
        {
            int amount = 0;
            if(info.UserVirtualCurrency != null)
            {
                if (info.UserVirtualCurrency.TryGetValue(SharedData.characterVirtualCurrency, out amount))
                {
                    gold = amount;
                }
                if (info.UserVirtualCurrency.TryGetValue(SharedData.runeVirtualCurrency, out amount))
                {
                    experience = amount;
                }
            }

            UserDataRecord record = new UserDataRecord();
            profileStats = new ProfileStats();

            if (info.UserData != null)
            {
                if (info.UserData.TryGetValue(ProfileStats.killsName, out record))
                {
                    profileStats.Kills = int.Parse(record.Value);
                }
                if (info.UserData.TryGetValue(ProfileStats.headShotsName, out record))
                {
                    profileStats.HeadShots = int.Parse(record.Value);
                }
                if (info.UserData.TryGetValue(ProfileStats.deathsName, out record))
                {
                    profileStats.Deaths = int.Parse(record.Value);
                }
            }
        }
    }
}
