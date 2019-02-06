using PlayFab.ClientModels;
using UnityEngine;

public class RuneStore : MonoBehaviour
{
    [SerializeField]
    private Database database;
    [SerializeField]
    private StoreRune storeRune;
    [SerializeField]
    private Transform runeStore;

    void Start()
    {
        for(int i=0; i < database.NotOwnedRuneStoreItems.Count; i++)
        {
            Rune newRune = CreateRune(database.NotOwnedRuneStoreItems[i]);
            GameObject storeRuneGO = Instantiate(storeRune.gameObject, runeStore);
            StoreRune newStoreRune = storeRuneGO.GetComponent<StoreRune>();
            newStoreRune.Rune = newRune;
            newStoreRune.InitRune();
        }
    }

    private Rune CreateRune(StoreItem item)
    {
        Rune newRune = new Rune();

        string data = item.CustomData.ToString();
        data = data.TrimStart('{');
        data = data.TrimEnd('}');
        string[] splited = data.Split(',', ':');
        for(int i=0; i<splited.Length; i++)
        {
            splited[i] = splited[i].TrimStart('"').TrimEnd('"');
        }

        newRune.icon = Resources.Load<Sprite>("RuneIcons/"+ splited[1]);
        newRune.health = int.Parse(splited[3]);
        newRune.armor = int.Parse(splited[5]);
        newRune.jumpPower = int.Parse(splited[7]);
        newRune.hpReg = uint.Parse(splited[9]);
        newRune.movemenetSpeed = int.Parse(splited[11]);
        newRune.shotGunDmg = int.Parse(splited[13]);
        newRune.sniperDmg = int.Parse(splited[15]);
        newRune.smgDmg = int.Parse(splited[17]);
        newRune.specialAbilityReduceTime = int.Parse(splited[19]);

        return newRune;
    }

}
