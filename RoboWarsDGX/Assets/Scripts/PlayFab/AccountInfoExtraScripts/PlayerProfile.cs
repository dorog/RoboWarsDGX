using PlayFab.ClientModels;

public class PlayerProfile
{
    //Profile data
    public static int choosedCharacterSlot = 0;
    public static int gold = 0;
    public static int experience = 0;
    public static int iron = 0;

    public static ProfileStats profileStats;
    //

    public void InitProfile(GetPlayerCombinedInfoResultPayload info)
    {
        if (info != null)
        {
            int amount = 0;
            if(info.UserVirtualCurrency != null)
            {
                amount = 0;
                if (info.UserVirtualCurrency.TryGetValue(SharedData.characterVirtualCurrency, out amount))
                {
                    gold = amount;
                }
                amount = 0;
                if (info.UserVirtualCurrency.TryGetValue(SharedData.runeVirtualCurrency, out amount))
                {
                    experience = amount;
                }
                amount = 0;
                if (info.UserVirtualCurrency.TryGetValue(SharedData.weaponVirtualCurrency, out amount))
                {
                    iron = amount;
                }
            }

            UserDataRecord record = new UserDataRecord();
            profileStats = new ProfileStats();

            SetProfilStats(info);
        }
    }

    private void SetProfilStats(GetPlayerCombinedInfoResultPayload info)
    {
        for (int i = 0; i < info.PlayerStatistics.Count; i++)
        {
            switch (info.PlayerStatistics[i].StatisticName)
            {
                case "Headshots":
                    profileStats.HeadShots = info.PlayerStatistics[i].Value;
                    break;
                case "Kills":
                    profileStats.Kills = info.PlayerStatistics[i].Value;
                    break;
                case "Deaths":
                    profileStats.Deaths = info.PlayerStatistics[i].Value;
                    break;
                case "Assists":
                    profileStats.Assists = info.PlayerStatistics[i].Value;
                    break;
                default:
                    break;
            }
        }
    }
}
