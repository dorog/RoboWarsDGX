using System;

[Serializable]
public class ProfileStats
{
    public int Kills { get; set; }
    public int HeadShots { get; set; }
    public int Deaths { get; set; }
    // dmg dealt
    // time in game

    public ProfileStats()
    {
        Kills = 0;
        HeadShots = 0;
        Deaths = 0;
    }
}
