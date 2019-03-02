using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CharacterFiring : MonoBehaviourPun, IPunObservable
{
    [Header ("UI")]
    public Text ammoText;
    public Text extraAmmoText;

    [Header ("Refactor under this")]
    public Animator firstPerson;
    public Animator thirdPerson;
    public Transform firePosition;
    private WeaponType weaponType;
    public LayerMask layerMask;

    public CharacterStats characterStat;
    private float dmg = 0;
    private float distance = 0;
    private float rapidFireTime;
    private float rapidTime;
    private bool inFire = false;
    private int ammo;
    private int extraAmmo;

    private delegate void Fire();
    private event Fire FireEvent;

    [Header("Firing Animation Setting")]
    public string shotGunFire = "ShotgunFire";
    public string smgGunFire = "SmgFire";
    public string sniperGunFire = "SniperFire";

    [Header("First person spines")]
    public Transform firstPersonSpine;
    public Transform firstPersonSpine1;
    public Transform firstPersonSpine2;

    private Transform thirdPersonSpine;
    private Transform thirdPersonSpine1;
    private Transform thirdPersonSpine2;

    private float thirdPersonAimX = 0;
    private float firstPersonAimX = 0;

    private float aimY = 0;

    [Header ("Look up settings")]
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
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            FireEvent();
        }
    }

    private void LateUpdate()
    {
        if (photonView.IsMine)
        {
            float rotationY = Input.GetAxis("Mouse Y");
            float rotationX = Input.GetAxis("Mouse X");

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


            firstPersonSpine.rotation = Quaternion.Euler(firstPersonAimX, aimY, 0);
            firstPersonSpine1.rotation = Quaternion.Euler(firstPersonAimX, aimY, 0);
            firstPersonSpine2.rotation = Quaternion.Euler(firstPersonAimX, aimY, 0);

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

    public void SetWeaponType(WeaponType type)
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
            rapidFireTime = characterStat.GetRapidTime();
            ammo = characterStat.GetAmmo();
            extraAmmo = characterStat.GetExtraAmmo();
            ammoText.text = "" + ammo;
            extraAmmoText.text = "" + extraAmmo;
        }
    }

    private void ShotgunFire()
    {
        Debug.Log("ShotgunFire");
    }

    private void SmgFire()
    {
        if (Input.GetMouseButton(0))
        {
            if(ammo == 0)
            {
                return;
            }
            if (!inFire)
            {
                rapidTime = rapidFireTime;
                //firstPerson.SetBool("Firing", true);
                thirdPerson.SetBool(smgGunFire, true);
                inFire = true;

                InstantFire(firePosition.position, firePosition.forward, dmg, distance, displayName);
                ammo--;
                ammoText.text = "" + ammo;
            }
            else
            {
                float time = rapidTime - Time.deltaTime;
                if (time <= 0)
                {
                    rapidTime = rapidFireTime;

                    InstantFire(firePosition.position, firePosition.forward, dmg, distance, displayName);
                    ammo--;
                    ammoText.text = "" + ammo;
                }
                else
                {
                    rapidTime = time;
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
        Debug.Log("SniperFire");
        /*
        if (Input.GetMouseButtonDown(0))
        {
            //firstPerson.SetBool("Firing", true);
            thirdPerson.SetBool(smgGunFire, true);
            GameModeManager.Instance.SpawnBullet(firePosition.position, firePosition.forward);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //firstPerson.SetBool("Firing", false);
            thirdPerson.SetBool(smgGunFire, false);
        }*/
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
}
