using UnityEngine;

public class StoreSubMenus : MonoBehaviour
{
    [SerializeField]
    private GameObject runeStore;
    [SerializeField]
    private GameObject characterStore;
    [SerializeField]
    private GameObject chestStore;

    [SerializeField]
    private StoreType defaultStore = StoreType.RuneStore;

    private GameObject activeStore = null;

    public GameObject RuneStore { get => runeStore; set => runeStore = value; }
    public GameObject CharacterStore { get => characterStore; set => characterStore = value; }
    public GameObject ChestStore { get => chestStore; set => chestStore = value; }

    void Start()
    {
        RuneStore.SetActive(false);
        CharacterStore.SetActive(false);
        ChestStore.SetActive(false);

        if (defaultStore == StoreType.RuneStore)
        {
            RuneStore.SetActive(true);
            activeStore = RuneStore;
        }
        else if(defaultStore == StoreType.CharacterStore)
        {
            CharacterStore.SetActive(true);
            activeStore = CharacterStore;
        }
        else
        {
            ChestStore.SetActive(true);
            activeStore = ChestStore;
        }
    }

    public void ShowStore(GameObject store)
    {
        if(activeStore == store)
        {
            return;
        }
        else if(activeStore != null)
        {
            activeStore.SetActive(false);
        }
        store.SetActive(true);
        activeStore = store;
    }
}
