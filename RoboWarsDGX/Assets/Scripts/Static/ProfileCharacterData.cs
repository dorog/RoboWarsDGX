using System;

[Serializable]
public class ProfileCharacterData
{
    public int SlotNumber { get; set; }
    public CharacterType type { get; set; }
    public ProfileStats characterProfileStat { get; set; }
}
