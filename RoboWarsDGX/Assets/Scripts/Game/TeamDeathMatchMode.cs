using Photon.Pun;
using UnityEngine;

public class TeamDeathMatchMode : IGameMode
{
    public SpawnedPlayerData SpawnPlayer(Vector3 spawnPoint)
    {
        GameObject character = PhotonNetwork.Instantiate("Characters/" + SelectData.selectedCharacter.id, spawnPoint, Quaternion.identity, 0);
        int characterID = character.GetComponent<PhotonView>().ViewID;

        GameObject weapon = PhotonNetwork.Instantiate("Weapons/" + SelectData.selectedWeapon.id, Vector3.zero, Quaternion.identity, 0);
        int weaponID = weapon.GetComponent<PhotonView>().ViewID;

        character.GetComponent<CharacterFiring>().SetWeaponType(SelectData.selectedWeapon.type, SelectData.selectedWeapon.id, true);
        character.GetComponent<TeamColorSetter>().ChangeClothes(SelectData.teamColor);

        Vector3 position = SelectData.selectedWeapon.prefab.transform.position;
        Quaternion rotation = SelectData.selectedWeapon.prefab.transform.rotation;

        SpawnedPlayerData spawnedPlayerData = new SpawnedPlayerData
        {
            characterID = characterID,
            weaponID = weaponID,
            characterName = SelectData.selectedCharacter.id,
            weaponName = SelectData.selectedWeapon.id,
        };

        return spawnedPlayerData;
    }
}
