using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rune", menuName = "Rune")]
public class Rune : ScriptableObject
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
}
