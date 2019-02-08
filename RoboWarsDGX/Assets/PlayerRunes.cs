using PlayFab.ClientModels;
using System.Collections.Generic;

public class PlayerRunes
{
    private static readonly string runeClass = "Rune";

    private List<ItemInstance> runes = new List<ItemInstance>();

    public void InitRunes(GetPlayerCombinedInfoResultPayload info)
    {
        if (info != null)
        {
            if (info.UserInventory != null)
            {
                for(int i=0; i< info.UserInventory.Count; i++)
                {
                    if(info.UserInventory[i].ItemClass == runeClass)
                    {
                        runes.Add(info.UserInventory[i]);
                    }
                }
            }
        }
    }

    public bool IsOwned(string id)
    {
        for(int i=0; i< runes.Count; i++)
        {
            if(runes[i].ItemId == id)
            {
                return true;
            }
        }
        return false;
    }

    public void AddRune(ItemInstance item)
    {
        runes.Add(item);
    }
}
