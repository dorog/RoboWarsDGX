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

    void Start()
    {
        runeStore.SetActive(false);
        characterStore.SetActive(false);
        chestStore.SetActive(false);

        if (defaultStore == StoreType.RuneStore)
        {
            runeStore.SetActive(true);
            activeStore = runeStore;
        }
        else if(defaultStore == StoreType.CharacterStore)
        {
            characterStore.SetActive(true);
            activeStore = characterStore;
        }
        else
        {
            chestStore.SetActive(true);
            activeStore = chestStore;
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
