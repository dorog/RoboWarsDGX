using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private StatBar statBar;

    [SerializeField]
    private Transform showPlace;

    [SerializeField]
    private GameObject createButton;

    [SerializeField]
    private CharacterSelector characterSelector;

    [SerializeField]
    private SelectCamera selectCamera;

    private CharacterType choosedType = CharacterType.Null;

    public void InitStats(CharacterData characterData)
    {
        if (choosedType == CharacterType.Null)
        {
            createButton.SetActive(true);
        }
        choosedType = characterData.type;
        StatBarValues[] stats = characterData.GetStats();
        if (transform.childCount > 0)
        {
            for (int i = transform.childCount-1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        for(int i=0; i<stats.Length; i++)
        {
            GameObject statBarGameObjectInstance = Instantiate(statBar.gameObject, transform);
            StatBar statBarInstance = statBarGameObjectInstance.GetComponent<StatBar>();
            statBarInstance.statName = stats[i].name;
            statBarInstance.statValue = stats[i].value;
            statBarInstance.InitTexts();
        }

        if (showPlace.childCount > 0)
        {
            Destroy(showPlace.GetChild(0).gameObject);
        }
        GameObject instance = Instantiate(characterData.gameObject, showPlace);
    }

    public void CreateCharacter()
    {
        ProfileCharacterData profileCharacterData = new ProfileCharacterData();
        profileCharacterData.SlotNumber = StaticProfile.choosedCharacterSlot;
        profileCharacterData.type = choosedType;
        profileCharacterData.characterProfileStat = new ProfileStats();

        FileManager.CreateCharacter(profileCharacterData);

        characterSelector.CreateCharacter(choosedType);

        selectCamera.GoSelectMenu();
    }
}
