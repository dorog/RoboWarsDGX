using UnityEngine;

public class BoneColliderHit : MonoBehaviour
{
    public CharacterData characterData;
    public TeamColorSetter teamColorSetter;
    public Bones boneType;

    public void GotShot(float dmg, string playerid, WeaponType weaponType)
    {
        characterData.GotShot(dmg, playerid, boneType, weaponType);
    }

    public void GotShot(float dmg, string playerid, TeamColor color, WeaponType weaponType)
    {
        if (teamColorSetter.TeamColor != color)
        {
            characterData.GotShot(dmg, playerid, boneType, weaponType);
        }
    }

    public void SpawnBlood(Vector3 hit, Vector3 normal)
    {
        characterData.SpawnWound(hit, normal, transform);
    }
}
