using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CharacterFiring : MonoBehaviourPun, IPunObservable
{
    [Header("UI")]
    public Text ammoText;
    public Text extraAmmoText;

    private FiringWeapon ownWeapon;
    public FiringWeapon thirdPersonWeapon;

    private float originalCameraFOV;

    [Header("Refactor under this")]
    public Animator firstPerson;
    public Animator thirdPerson;
    public Transform firePosition;
    public LayerMask layerMask;

    public CharacterStats characterStat;

    public Transform thirdPersonEffectPosition;

    [Header("First person")]
    public Camera firstPersonCam;
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

    private float increasedUpAll = 0;
    private float increasedUp = 0;
    private float increasingTime = 0;
    private float originalIncreasingTime = 0;
    private float increasingSpeed = 0;
    private float increaseAmount = 0;
    private float fireUpDecreaseSpeed;
    private float timeBetweenIncreaseAndDistance;
    private float originalTimeBetweenIncreaseAndDistance;
    private float maxIncrease;
    private int maxIncreaseMultiply = 5;

    void Start()
    {
        thirdPersonSpine = thirdPerson.GetBoneTransform(HumanBodyBones.Spine);
        thirdPersonSpine1 = thirdPerson.GetBoneTransform(HumanBodyBones.Chest);
        thirdPersonSpine2 = thirdPerson.GetBoneTransform(HumanBodyBones.UpperChest);
        if (photonView.IsMine)
        {
            originalCameraFOV = firstPersonCam.fieldOfView;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (ownWeapon.FireCheck())
            {
                photonView.RPC("ShowThirdPersonWeaponEffect", RpcTarget.Others);
                firstPerson.SetTrigger("Fire");

                increasedUp += increaseAmount;

                increasingTime += originalIncreasingTime;
                timeBetweenIncreaseAndDistance = originalTimeBetweenIncreaseAndDistance;
            }
            ownWeapon.ReloadCheck();
        }
    }

    [PunRPC]
    private void ShowThirdPersonWeaponEffect()
    {
        thirdPersonWeapon.ShowEffect();
    }

    private void LateUpdate()
    {
        if (photonView.IsMine)
        {
            float rotationY = Input.GetAxis("Mouse Y");
            float rotationX = Input.GetAxis("Mouse X");

            rotationY *= firstPersonCam.fieldOfView / originalCameraFOV;
            rotationX *= firstPersonCam.fieldOfView / originalCameraFOV;

            rotationY = AddFiringEffects(rotationY);

            BorderCheck(rotationY);

            aimY += rotationX;

            firstPersonCam.transform.rotation = Quaternion.Euler(firstPersonAimX, aimY, 0);

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

    public void SetWeaponType(WeaponType type, string weaponName, bool tfGame = false)
    {
        if (photonView.IsMine)
        {
            FiringWeaponData data = new FiringWeaponData();
            data.dmg = characterStat.GetDmg(type);
            data.distance = characterStat.GetWeaponDistance();
            data.minTimeBetweenFire = characterStat.GetRapidTime();
            data.ammo = characterStat.GetAmmo();
            data.maxAmmoAtOnce = data.ammo;
            data.extraAmmo = characterStat.GetExtraAmmo();
            data.ammoText = ammoText;
            data.extraAmmoText = extraAmmoText;
            data.firePosition = firePosition;
            data.layerMask = layerMask;
            data.displayName = AccountInfo.Instance.Info.PlayerProfile.DisplayName;
            data.teamGame = tfGame;

            GameObject weaponPrefab = Resources.Load<GameObject>("Weapons/" + weaponName);
            GameObject weapon = Instantiate(weaponPrefab, rightHand);
            weapon.transform.localPosition = fpsWeaponInitData.GetWeaponPosition(weaponName);
            weapon.transform.localRotation = Quaternion.Euler(fpsWeaponInitData.GetWeaponRotation(weaponName));
            weapon.transform.localScale = new Vector3(100, 100, 100);

            ownWeapon = weapon.GetComponent<FiringWeapon>();
            ownWeapon.SetData(data);

            firstPerson.runtimeAnimatorController = ownWeapon.animatorController;

            originalIncreasingTime = ownWeapon.timeForUp;
            increaseAmount = ownWeapon.fireUpDistance;

            originalTimeBetweenIncreaseAndDistance = ownWeapon.timeBetweenIncreaseAndDistance;


            increasingSpeed = ownWeapon.fireUpDistance / ownWeapon.timeForUp;
            fireUpDecreaseSpeed = ownWeapon.fireUpDistance / ownWeapon.timeForDown;

            maxIncrease = ownWeapon.fireUpDistance * maxIncreaseMultiply;
        }
    }

    private void BorderCheck(float rotationY)
    {

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
    }

    private float AddFiringEffects(float rotationY)
    {
        if (increasedUp != 0)
        {
            float amount = increasingSpeed * Time.deltaTime;

            if (increasedUp - amount <= 0)
            {
                increasedUp = 0;
                if(increasedUpAll + increasedUp >= maxIncrease)
                {
                    increasedUpAll = maxIncrease;
                    return rotationY + (maxIncrease - increasedUpAll);
                }
                else
                {
                    increasedUpAll += increasedUp;
                    return rotationY + increasedUp;
                }
            }
            else
            {
                increasedUp -= amount;
                if (increasedUpAll + amount >= maxIncrease)
                {
                    increasedUpAll = maxIncrease;
                    return rotationY + (maxIncrease - increasedUpAll);
                }
                else
                {
                    increasedUpAll += amount;
                    return rotationY + amount;
                }
            }
        }
        else if (timeBetweenIncreaseAndDistance != 0)
        {
            float amount = timeBetweenIncreaseAndDistance - Time.deltaTime;
            if (amount <= 0)
            {
                timeBetweenIncreaseAndDistance = 0;
            }
            else
            {
                timeBetweenIncreaseAndDistance = amount;
            }
        }
        else if (increasedUpAll != 0)
        {
            float amount = fireUpDecreaseSpeed * Time.deltaTime;
            if (increasedUpAll - amount <= 0)
            {
                increasedUpAll = 0;
                return rotationY - increasedUpAll;
            }
            else
            {
                increasedUpAll -= amount;
                return rotationY - amount;
            }
        }
        return rotationY;
    
    }
}
