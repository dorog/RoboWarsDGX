using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class FileManager
{
    static private string fileName = "RoboWarsDGX";

    public static void Save()
    {
        /*PlayerData.allCoin += PlayerData.actualCoin;
        PlayerData.actualCoin = 0;
        if (PlayerData.actualGameScore > PlayerData.topScore)
        {
            PlayerData.topScore = PlayerData.actualGameScore;
        }
        string deviceFileLocation = Application.persistentDataPath + "/" + fileName;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(deviceFileLocation, FileMode.Open);
        Data data = (Data)bf.Deserialize(file);
        file.Close();

        data.topScore = PlayerData.topScore;
        data.allCoin = PlayerData.allCoin;
        data.shieldLevel = PlayerData.shieldLevel;
        data.reviveItemCount = PlayerData.reviveItemCount;

        FileStream fileForSave = File.Create(deviceFileLocation);
        bf.Serialize(fileForSave, data);
        fileForSave.Close();*/
    }

    public static void Load()
    {
        string deviceFileLocation = Application.persistentDataPath + "/" + fileName;

        if (File.Exists(deviceFileLocation))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(deviceFileLocation, FileMode.Open);
            Profile profile = (Profile)bf.Deserialize(file);
            file.Close();

            StaticProfile.choosedCharacterSlot = profile.choosedCharacterSlot;
            StaticProfile.profileStats = profile.profileStats;
            StaticProfile.profileCharacterData = profile.profileCharacterData;

        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(deviceFileLocation, FileMode.Create);
            Profile profile = new Profile();

            profile.choosedCharacterSlot = 0;
            profile.profileStats = new ProfileStats();
            profile.profileCharacterData = null;

            bf.Serialize(file, profile);
            file.Close();
        }
    }

    public static void CreateCharacter(ProfileCharacterData characterData)
    {
        string deviceFileLocation = Application.persistentDataPath + "/" + fileName;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(deviceFileLocation, FileMode.Open);
        Profile profile = (Profile)bf.Deserialize(file);
        file.Close();

        if(profile.profileCharacterData == null)
        {
            profile.profileCharacterData = new ProfileCharacterData[1];
            profile.profileCharacterData[0] = characterData;
        }
        else
        {
            ProfileCharacterData[] data = profile.profileCharacterData;
            profile.profileCharacterData = new ProfileCharacterData[profile.profileCharacterData.Length];
            for(int i=0; i< data.Length; i++)
            {
                profile.profileCharacterData[i] = data[i];
            }
            profile.profileCharacterData[profile.profileCharacterData.Length - 1] = characterData;
        }

        FileStream fileForSave = File.Create(deviceFileLocation);
        bf.Serialize(fileForSave, profile);
        fileForSave.Close();
    }
}