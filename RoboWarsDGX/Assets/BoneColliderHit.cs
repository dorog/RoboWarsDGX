using UnityEngine;

public class BoneColliderHit : MonoBehaviour
{
    public CharacterData characterData;
    public Bones boneType;


    public void GotShot(float dmg, string playerid)
    {
        characterData.GotShot(dmg, playerid, boneType);
    }
}
