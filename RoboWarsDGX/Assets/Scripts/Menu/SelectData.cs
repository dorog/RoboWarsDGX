using System.Collections;

public class SelectData
{
    public static Character selectedCharacter = null;
    public static SelectItemCharacter selectedItemCharacter = null;

    public static Weapon selectedWeapon = null;
    public static SelectItemWeapon selectedItemWeapon = null;

    public static Hashtable deathHistory;

    public static Rune[] selectedRunes = new Rune[3] { null, null, null};

    public static TeamColor teamColor = TeamColor.Null;
}
