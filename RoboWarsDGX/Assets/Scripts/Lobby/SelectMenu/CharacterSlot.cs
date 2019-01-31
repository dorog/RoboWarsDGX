using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private GameObject sphere;
    [SerializeField]
    private GameObject cylinder;

    private int id;

    public ProfileStats CharacterProfileStats { get; private set; } = null;

    public void Init(int id)
    {
        this.id = id;

        ProfileCharacterData result = null;
        if(StaticProfile.profileCharacterData == null)
        {
            return;
        }
        for(int i=0; i<StaticProfile.profileCharacterData.Length; i++)
        {
            if (StaticProfile.profileCharacterData[i].SlotNumber==id)
            {
                result = StaticProfile.profileCharacterData[i];
                break;
            }
        }

        if (result != null)
        {
            CharacterProfileStats = result.characterProfileStat;
            InstantiateCharacter(result.type);
        }
    }

    public void InstantiateCharacter(CharacterType type)
    {
        switch (type)
        {
            case CharacterType.cube:
                Instantiate(cube, transform);
                break;
            case CharacterType.cylinder:
                Instantiate(cylinder, transform);
                break;
            case CharacterType.sphere:
                Instantiate(sphere, transform);
                break;
            default:
                break;
        }
    }
}
