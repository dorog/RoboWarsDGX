
public class SharedData
{
    public static readonly string runeVirtualCurrency = "XP";
    public static readonly string characterVirtualCurrency = "GD";
    public static readonly string weaponVirtualCurrency = "FE";

    public static readonly string loginScene = "Login";
    public static readonly string menuScene = "Menu";

    private const string rusherName = "Rusher";
    private const string boosterName = "Booster";

    public static readonly string runeClass = "Rune";
    public static readonly string characterClass = "Character";
    public static readonly string weaponClass = "Weapon";
    public static readonly string itemName = "Items";

    public static readonly string runeStoreName = "RuneStore";
    public static readonly string characterStoreName = "CharacterStore";
    public static readonly string weaponStoreName = "WeaponStore";

    public static string[] ParseJson(string json)
    {
        string data = json;
        data = data.TrimStart('{');
        data = data.TrimEnd('}');
        string[] splited = data.Split(',', ':');
        for (int i = 0; i < splited.Length; i++)
        {
            splited[i] = splited[i].TrimStart('"').TrimEnd('"');
        }
        return splited;
    }

    public static string desertMap = "Desert";

    public const string deathMatch = "Death Match";
    public const string teamDeathMatch = "Team Death Match";
    public const string battleRoyal = "Battle Royal";
    public const string searchAndDestroy = "Search and Destroy";

    public static GameMode GetGameMode(string gameMode)
    {
        switch (gameMode)
        {
            case deathMatch:
                return GameMode.DeathMatch;
            case teamDeathMatch:
                return GameMode.TeamDeathMatch;
            case battleRoyal:
                return GameMode.BattleRoyal;
            case searchAndDestroy:
                return GameMode.SearchAndDestroy;
            default:
                return GameMode.DeathMatch;
        }
    }

    public static string GetGameModeString(GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameMode.DeathMatch:
                return deathMatch;
            case GameMode.TeamDeathMatch:
                return teamDeathMatch;
            case GameMode.BattleRoyal:
                return battleRoyal;
            default:
                return deathMatch;
        }
    }

    public static string GameModeKey = "GameMode";
    public static string MapKey = "Map";

    public static string NotRenderInIsMine = "NotRenderInIsMine";
}
