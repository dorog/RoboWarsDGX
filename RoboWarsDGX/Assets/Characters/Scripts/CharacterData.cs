using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public int health;
    public int armor;
    public float jumpPower;
    public float reducedReloadTime;
    public int hpReg;
    public int movemenetSpeed;
    public int shotGunDmg;
    public int sniperDmg;
    public int smgDmg;
    public CharacterType type;

    public StatBarValues[] GetStats()
    {
        StatBarValues[] stats = { new StatBarValues("HP:", "" + health), new StatBarValues("Armor:", "" + armor) };
        return stats;
    }
}

public class StatBarValues
{
    public string name;
    public string value;

    public StatBarValues(string name, string value)
    {
        this.name = name;
        this.value = value;
    }
}
