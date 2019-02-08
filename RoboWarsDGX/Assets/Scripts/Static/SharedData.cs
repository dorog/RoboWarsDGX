
public class SharedData
{
    public static readonly string runeVirtualCurrency = "XP";
    public static readonly string characterVirtualCurrency = "GD";
    public static readonly string weaponVirtualCurrency = "IR";

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
}
