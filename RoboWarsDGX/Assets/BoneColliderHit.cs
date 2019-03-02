using UnityEngine;

public class BoneColliderHit : MonoBehaviour
{
    public float intensity = 1;
    public CharacterStates characterState;
    public CharacterStats stats;
    public Bones boneType;
    public bool head = false;


    private void Start()
    {
        if (!head)
        {
            intensity = stats.GetBoneIntensity(boneType);
        }
        else
        {
            intensity = 100;
        }
    }

    public void GotShot(float dmg, string playerid)
    {
        Debug.Log("Got shot " + gameObject.name);
        if (head)
        {
            Debug.Log("Head");
            //characterState.HeadShot(playerid);
            characterState.GotShot(dmg * intensity, playerid);
        }
        else
        {
            Debug.Log("Else " + dmg);
            characterState.GotShot(dmg * intensity, playerid);
        }
    }
}
