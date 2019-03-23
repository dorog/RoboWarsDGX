using System.Collections.Generic;
using UnityEngine;

public class Shotgun : FiringWeapon
{
    [Header ("Shotgun settings")]
    public int bulletPartCount = 7;
    public float shotGunRadius;
    public float radiusDistance;
    public GameObject bulletPart;

    private GameObject[] bulletparts;

    public Transform effectTransform;

    public override void SetData(FiringWeaponData data)
    {
        base.SetData(data);
        PrepareShotgunFire();
    }

    public override bool FireCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!HasAmmo())
            {
                return false;
            }
            if (weaponCanFire)
            {
                ShowEffect();

                List<ShotGunHit> shotGunHits = new List<ShotGunHit>();
                for (int i = 0; i < bulletPartCount; i++)
                {
                    Vector3 forward = (bulletparts[i].transform.position - firePosition.position).normalized;
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

    private void PrepareShotgunFire()
    {
        bulletparts = new GameObject[bulletPartCount];

        GameObject circleCenterBullet = Instantiate(bulletPart, firePosition);

        Vector3 centerPosition = Vector3.forward * radiusDistance;
        circleCenterBullet.transform.localPosition = Vector3.forward * radiusDistance;

        Vector3 firstAngle = Vector3.up * shotGunRadius;
        float angle = 360 / (bulletPartCount - 1);
        float actualAngle = 0;
        bulletparts[0] = circleCenterBullet;

        for (int i = 1; i < bulletPartCount; i++)
        {
            Vector3 position = Quaternion.Euler(new Vector3(0, 0, actualAngle)) * firstAngle;
            GameObject bulletPartGO = Instantiate(bulletPart, firePosition);
            bulletPartGO.transform.localPosition = centerPosition + position;
            actualAngle += angle;
            bulletparts[i] = bulletPartGO;
        }
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
