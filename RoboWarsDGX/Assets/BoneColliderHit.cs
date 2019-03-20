using UnityEngine;

public class BoneColliderHit : MonoBehaviour
{
    public CharacterData characterData;
    public TeamColorSetter teamColorSetter;
    public Bones boneType;


    public void GotShot(float dmg, string playerid)
    {
        characterData.GotShot(dmg, playerid, boneType);
    }

    public void GotShot(float dmg, string playerid, TeamColor color)
    {
        if (teamColorSetter.TeamColor != color)
        {
            characterData.GotShot(dmg, playerid, boneType);
        }
    }
}
