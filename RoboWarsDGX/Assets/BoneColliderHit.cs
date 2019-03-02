using UnityEngine;

public class BoneColliderHit : MonoBehaviour
{
    public CharacterStates characterState;
    public Bones boneType;

    public void GotShot(float dmg, string playerid)
    {
        characterState.GotShot(dmg, playerid, boneType);
    }
}
