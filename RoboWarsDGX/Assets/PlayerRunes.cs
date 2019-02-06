using PlayFab.ClientModels;
using System.Collections.Generic;

public class PlayerRunes
{
    private static readonly string runeClass = "Rune";

    private List<ProfileRuneData> profileRunData = new List<ProfileRuneData>();

    private List<ItemInstance> runes = new List<ItemInstance>();

    public void InitRunes(GetPlayerCombinedInfoResultPayload info)
    {
        if (info != null)
        {
            if (info.UserInventory != null)
            {
                runes = info.UserInventory;
                for (int i = 0; i < runes.Count; i++)
                {
                    if (runes[i].ItemClass == runeClass)
                    {
                        profileRunData.Add(CreateProfileRuneData(runes[i]));
                    }
                }
            }
        }
    }

    private ProfileRuneData CreateProfileRuneData(ItemInstance item)
    {
        ProfileRuneData runeData = new ProfileRuneData();
        runeData.type = SharedData.RuneStringToEnum(item.ItemId);

        return runeData;
    }

    public bool IsOwned(RuneType type)
    {
        for(int i=0; i< profileRunData.Count; i++)
        {
            if(profileRunData[i].type == type)
            {
                return true;
            }
        }
        return false;
    }
}
