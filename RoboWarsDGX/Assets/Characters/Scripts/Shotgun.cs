using System.Collections.Generic;
using UnityEngine;

public class Shotgun : FiringWeapon
{
    [Header ("Shotgun settings")]
    public int bulletPartCount = 7;
    public float shotGunRadius;
    public float radiusDistance;

    public Transform effectTransform;

    public override bool FireCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!CanFire())
            {
                return false;
            }
            if (weaponCanFire)
            {
                ShowEffect();

                List<ShotGunHit> shotGunHits = new List<ShotGunHit>();
                for (int i = 0; i < bulletPartCount; i++)
                {
                    Vector2 range = Random.insideUnitCircle * shotGunRadius;
                    Vector3 bulletPosition = firePosition.position + range.x * firePosition.right + range.y * firePosition.up + firePosition.forward * radiusDistance;

                    Vector3 forward = (bulletPosition - firePosition.position).normalized;
                    BoneColliderHit boneColliderHit = InstantFire(firePosition.position, forward, distance);
                    if (boneColliderHit != null)
                    {
                        if (!teamGame || (boneColliderHit.teamColorSetter.TeamColor != SelectData.teamColor))
                        {
                            bool inTheList = false;
                            for (int j = 0; j < shotGunHits.Count; j++)
                            {
                                if (boneColliderHit.characterData == shotGunHits[j].characterData)
                                {
                                    shotGunHits[j].bones.Add(boneColliderHit.boneType);
                                    inTheList = true;
                                    break;
                                }
                            }
                            if (!inTheList)
                            {
                                ShotGunHit newHit = new ShotGunHit();
                                newHit.characterData = boneColliderHit.characterData;
                                newHit.bones.Add(boneColliderHit.boneType);
                                shotGunHits.Add(newHit);
                            }
                        }
                    }
                }
                foreach (var player in shotGunHits)
                {
                    player.characterData.GotShotByMoreBones(dmg, displayName, player.bones, WeaponType.Shotgun);
                }

                Fire();
                weaponCanFire = false;
                Invoke("WeaponCanFire", minTimeBetweenFire);
                return true;
            }
        }
        return false;
    }

    public override void ShowEffect()
    {
        GameObject effect = Instantiate(fireEffect.gameObject, effectTransform.position, Quaternion.identity);
        Destroy(effect, 2);
    }

    private class ShotGunHit
    {
        public CharacterData characterData;
        public List<Bones> bones;

        public ShotGunHit()
        {
            bones = new List<Bones>();
        }
    }
}
