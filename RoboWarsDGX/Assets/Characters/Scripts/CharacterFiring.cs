﻿using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterFiring : MonoBehaviourPun, IPunObservable
{
    [Header("UI")]
    public Text ammoText;
    public Text extraAmmoText;

    [Header("Refactor under this")]
    public Animator firstPerson;
    public Animator thirdPerson;
    public Transform firePosition;
    private WeaponType weaponType;
    public LayerMask layerMask;

    public CharacterStats characterStat;
    private float dmg = 0;
    private float distance = 0;
    private float minTimeBetweenFire;
    private float rapidTime = 0;
    private bool inFire = false;
    private int maxAmmoAtOnce;
    private int ammo;
    private int extraAmmo;

    private delegate void Fire();
    private event Fire FireEvent;
    private bool weaponCanFire = true;

    [Header("Sniper settings")]
    public float scoopeFOV = 15f;
    private float originalF0V;
    private float scopeMouseIntensity;
    public Camera characterCamera;
    private bool scoped = false;

    [Header("Shotgun settings")]
    public float shotGunRadius;
    public float radiusDistance;
    public int bulletPartCount;
    public GameObject bulletPart;
    private GameObject[] bulletparts;


    [Header("Firing Animation Setting")]
    public string shotGunFire = "ShotgunFire";
    public string smgGunFire = "SmgFire";
    public string sniperGunFire = "SniperFire";

    [Header("First person")]
    public Transform firstPersonCam;
    public Transform rightHand;
    public WeaponInitData fpsWeaponInitData;

    private Transform thirdPersonSpine;
    private Transform thirdPersonSpine1;
    private Transform thirdPersonSpine2;

    private float thirdPersonAimX = 0;
    private float firstPersonAimX = 0;

    private float aimY = 0;

    [Header("Look up settings")]
    public float multiply = 1f;
    public float maxRotation = 10f;
    public float minRotation = -10f;
    public float firstPersonLookMultiply = 3f;

    private float cloneX = 0f;
    private string displayName;

    void Start()
    {

        thirdPersonSpine = thirdPerson.GetBoneTransform(HumanBodyBones.Spine);
        thirdPersonSpine1 = thirdPerson.GetBoneTransform(HumanBodyBones.Chest);
        thirdPersonSpine2 = thirdPerson.GetBoneTransform(HumanBodyBones.UpperChest);
        if (photonView.IsMine)
        {
            displayName = AccountInfo.Instance.Info.PlayerProfile.DisplayName;
            Cursor.lockState = CursorLockMode.Locked;
            originalF0V = characterCamera.fieldOfView;
            scopeMouseIntensity = scoopeFOV / originalF0V;
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            FireEvent();
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (extraAmmo == 0)
                {
                    return;
                }
                if (extraAmmo >= (maxAmmoAtOnce - ammo))
                {
                    extraAmmo -= (maxAmmoAtOnce - ammo);
                    ammo = maxAmmoAtOnce;
                    ammoText.text = "" + ammo;
                    extraAmmoText.text = "" + extraAmmo;
                }
                else
                {
                    ammo += extraAmmo;
                    extraAmmo = 0;
                    ammoText.text = "" + ammo;
                    extraAmmoText.text = "" + extraAmmo;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (photonView.IsMine)
        {
            float rotationY = Input.GetAxis("Mouse Y");
            float rotationX = Input.GetAxis("Mouse X");

            if (scoped)
            {
                rotationY *= scopeMouseIntensity;
                rotationX *= scopeMouseIntensity;
            }

            if (thirdPersonAimX - rotationY * multiply > maxRotation)
            {
                firstPersonAimX = maxRotation * firstPersonLookMultiply;
                thirdPersonAimX = maxRotation;
            }
            else if (thirdPersonAimX - rotationY * multiply < minRotation)
            {
                firstPersonAimX = minRotation * firstPersonLookMultiply;
                thirdPersonAimX = minRotation;
            }
            else
            {
                firstPersonAimX -= rotationY * multiply * firstPersonLookMultiply;
                thirdPersonAimX -= rotationY * multiply;
            }

            aimY += rotationX;


            firstPersonCam.rotation = Quaternion.Euler(firstPersonAimX, aimY, 0);

            transform.rotation = Quaternion.Euler(0, aimY, 0);

        }
        else
        {
            thirdPersonSpine.Rotate(new Vector3(cloneX, 0, 0));
            thirdPersonSpine1.Rotate(new Vector3(cloneX, 0, 0));
            thirdPersonSpine2.Rotate(new Vector3(cloneX, 0, 0));
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(thirdPersonAimX);
        }
        else if (stream.IsReading)
        {
            cloneX = (float)stream.ReceiveNext();
        }
    }

    public void SetWeaponType(WeaponType type, string weaponName)
    {
        weaponType = type;
        switch (type)
        {
            case WeaponType.Shotgun:
                FireEvent += ShotgunFire;
                break;
            case WeaponType.SMG:
                FireEvent += SmgFire;
                break;
            case WeaponType.Sniper:
                FireEvent += SniperFire;
                break;
        }

        if (photonView.IsMine)
        {
            dmg = characterStat.GetDmg(type);
            distance = characterStat.GetWeaponDistance();
            minTimeBetweenFire = characterStat.GetRapidTime();
            ammo = characterStat.GetAmmo();
            maxAmmoAtOnce = ammo;
            extraAmmo = characterStat.GetExtraAmmo();
            ammoText.text = "" + ammo;
            extraAmmoText.text = "" + extraAmmo;
            if (type == WeaponType.Shotgun)
            {
                PrepareShotgunFire();
            }
            GameObject weaponPrefab = Resources.Load<GameObject>("Weapons/" + weaponName);
            GameObject weapon = Instantiate(weaponPrefab, rightHand);
            weapon.transform.localPosition = fpsWeaponInitData.GetWeaponPosition(weaponName);
            weapon.transform.localRotation = Quaternion.Euler(fpsWeaponInitData.GetWeaponRotation(weaponName));
        }
    }

    private void PrepareShotgunFire()
    {
        bulletparts = new GameObject[bulletPartCount];

        GameObject circleCenterBullet = Instantiate(bulletPart, firePosition);

        Vector3 centerPosition = Vector3.forward * radiusDistance;
        circleCenterBullet.transform.localPosition = Vector3.forward * radiusDistance;

        //Vector3 firstAngle = Vector3.forwarVector3.forward * radiusDistance;d * radiusDistance + Vector3.up * shotGunRadius;
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

    private void ShotgunFire()
    {
        /*for (int i = 0; i < bulletparts.Length; i++)
        {
            Debug.DrawLine(firePosition.position, bulletparts[i].transform.position, Color.red);
        }*/

        if (Input.GetMouseButtonDown(0))
        {
            if (ammo == 0)
            {
                return;
            }
            if (weaponCanFire)
            {
                List<ShotGunHit> shotGunHits = new List<ShotGunHit>();
                for (int i = 0; i < bulletPartCount; i++)
                {
                    Vector3 forward = (bulletparts[i].transform.position - firePosition.position).normalized;
                    BoneColliderHit boneColliderHit = ShotGunFireRaycast(firePosition.position, forward, dmg, distance);
                    if (boneColliderHit != null)
                    {
                        bool inTheList = false;
                        for(int j = 0; j < shotGunHits.Count; j++)
                        {
                            if(boneColliderHit.characterData == shotGunHits[j].characterData)
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
                foreach (var player in shotGunHits)
                {
                    player.characterData.GotShotByMoreBones(dmg, displayName, player.bones);
                }

                ammo--;
                ammoText.text = "" + ammo;
                weaponCanFire = false;
                Invoke("WeaponCanFire", minTimeBetweenFire);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void WeaponCanFire()
    {
        weaponCanFire = true;
    }

    private void SmgFire()
    {
        if (Input.GetMouseButton(0))
        {
            if (ammo == 0)
            {
                return;
            }
            if (!inFire && weaponCanFire)
            {
                //firstPerson.SetBool("Firing", true);
                thirdPerson.SetBool(smgGunFire, true);
                inFire = true;

                InstantFire(firePosition.position, firePosition.forward, dmg, distance, displayName);
                ammo--;
                ammoText.text = "" + ammo;
                Invoke("WeaponCanFire", minTimeBetweenFire);
                weaponCanFire = false;
            }
            else
            {
                if (weaponCanFire)
                {
                    InstantFire(firePosition.position, firePosition.forward, dmg, distance, displayName);
                    ammo--;
                    ammoText.text = "" + ammo;
                    Invoke("WeaponCanFire", minTimeBetweenFire);
                    weaponCanFire = false;
                }
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            //firstPerson.SetBool("Firing", false);
            thirdPerson.SetBool(smgGunFire, false);
            inFire = false;
        }
    }

    private void SniperFire()
    {        
        if (Input.GetMouseButtonDown(0))
        {
            if (ammo == 0)
            {
                return;
            }
            if (weaponCanFire)
            {
                InstantFire(firePosition.position, firePosition.forward, dmg, distance, displayName);
                ammo--;
                ammoText.text = "" + ammo;
                Invoke("WeaponCanFire", minTimeBetweenFire);
                weaponCanFire = false;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (scoped)
            {
                characterCamera.fieldOfView = originalF0V;
            }
            else
            {
                characterCamera.fieldOfView = scoopeFOV;
            }
            scoped = !scoped;
        }
    }

    private void InstantFire(Vector3 position, Vector3 forward, float dmg, float distance, string playerid)
    {
        RaycastHit hit;
        if (Physics.Raycast(position, forward, out hit, distance, layerMask))
        {
            BoneColliderHit boneColliderHit = hit.collider.gameObject.GetComponent<BoneColliderHit>();
            // hit.point: Spawn blood
            if (boneColliderHit == null)
            {
                return;
            }

            boneColliderHit.GotShot(dmg, playerid);
        }
    }

    private BoneColliderHit ShotGunFireRaycast(Vector3 position, Vector3 forward, float dmg, float distance)
    {
        RaycastHit hit;
        if (Physics.Raycast(position, forward, out hit, distance, layerMask))
        {
            BoneColliderHit boneColliderHit = hit.collider.gameObject.GetComponent<BoneColliderHit>();
            // hit.point: Spawn blood
            if (boneColliderHit == null)
            {
                return null;
            }
            return boneColliderHit;
        }
        return null;
    }

    private class ShotGunHit{
        public CharacterData characterData;
        public List<Bones> bones;

        public ShotGunHit()
        {
            bones = new List<Bones>();
        }
    }
}
