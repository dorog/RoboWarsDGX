using PlayFab.ClientModels;
using System.Collections.Generic;

public class PlayerCharacters
{
    private List<ProfileCharacterData> profileCharacters = new List<ProfileCharacterData>();

    public List<ProfileCharacterData> ProfileCharacters{ get => profileCharacters; }

    private List<CharacterResult> characters = new List<CharacterResult>();

    public void InitCharacters(GetPlayerCombinedInfoResultPayload info)
    {
        if (info != null)
        {
            if (info.CharacterList != null)
            {
                characters = info.CharacterList;
                for (int i = 0; i < characters.Count; i++)
                {
                    profileCharacters.Add(CreateProfileCharacterData(characters[i]));
                }
            }
        }
    }

    private ProfileCharacterData CreateProfileCharacterData(CharacterResult result)
    {
        ProfileCharacterData character = new ProfileCharacterData();
        character.type = SharedData.CharacterStringToEnum(result.CharacterType);
        character.name = result.CharacterName;

        return character;
    }

    public bool IsOwned(CharacterType type)
    {
        for(int i=0; i<ProfileCharacters.Count; i++)
        {
            if(type == ProfileCharacters[i].type)
            {
                return true;
            }
        }
        return false;
    }
}
