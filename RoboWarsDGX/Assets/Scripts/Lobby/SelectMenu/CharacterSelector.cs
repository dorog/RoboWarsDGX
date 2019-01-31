using System.Collections;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField]
    private int slotNumber = 5;
    [SerializeField]
    private float distanceFromCenter = 700;
    [SerializeField]
    private float y = 0;
    [SerializeField]
    private CharacterSlot slot;
    [SerializeField]
    private GameObject createButton;
    [SerializeField]
    private GameObject playButton;
    [SerializeField]
    private float rotationTime;

    private CharacterSlot[] characterSlots;

    private float aimRotation = 0;

    void Start()
    {
        FileManager.Load();
        CreateSlots();
        Rotate();
        SlotCheck();
    }

    private void CreateSlots()
    {
        float alphaDelta = -360 / slotNumber;
        float alpha = 0;

        characterSlots = new CharacterSlot[slotNumber];

        for (int i = 0; i < slotNumber; i++)
        {
            float alphaRad = alpha * Mathf.PI / 180;
            float z = Mathf.Cos(alphaRad) * distanceFromCenter;
            float x = Mathf.Sin(alphaRad) * distanceFromCenter;
            GameObject instance = Instantiate(slot.gameObject, new Vector3(-x, y, -z), Quaternion.identity, transform);
            CharacterSlot characterSlot = instance.GetComponent<CharacterSlot>();
            characterSlots[i] = characterSlot;
            characterSlot.Init(i);
            alpha += alphaDelta;
        }
    }

    private void Rotate()
    {
        if(StaticProfile.choosedCharacterSlot != 0)
        {
            float rotation = StaticProfile.choosedCharacterSlot * 360/slotNumber;
            transform.Rotate(new Vector3(0, rotation, 0));
            aimRotation = rotation;
        }
    }

    private void SlotCheck()
    {
        if(characterSlots[StaticProfile.choosedCharacterSlot].CharacterProfileStats == null){
            createButton.SetActive(true);
            playButton.SetActive(false);
        }
        else
        {
            createButton.SetActive(false);
            playButton.SetActive(true);
        }
    }

    public void CreateCharacter(CharacterType type)
    {
        characterSlots[StaticProfile.choosedCharacterSlot].InstantiateCharacter(type);
        createButton.SetActive(false);
    }

    public void RotateLeft()
    {
        StartCoroutine(RotateTransform(RotationDirection.left));
    }

    public void RotateRight()
    {
        StartCoroutine(RotateTransform(RotationDirection.right));
    }

    private IEnumerator RotateTransform(RotationDirection direction)
    {
        float rotationAngle = 0;
        createButton.SetActive(false);
        playButton.SetActive(false);

        if (direction == RotationDirection.left)
        {
            rotationAngle = -(360 / slotNumber);
            if(StaticProfile.choosedCharacterSlot == 0)
            {
                StaticProfile.choosedCharacterSlot = slotNumber-1;
            }
            else
            {
                StaticProfile.choosedCharacterSlot--;
            }
        }
        else
        {
            rotationAngle = (360 / slotNumber);
            if (StaticProfile.choosedCharacterSlot == slotNumber - 1)
            {
                StaticProfile.choosedCharacterSlot = 0;
            }
            else
            {
                StaticProfile.choosedCharacterSlot++;
            }
        }
        aimRotation += rotationAngle;
        
        var t = 0f;
        while (t < 1)
        {
            if(t + Time.deltaTime > 1)
            {
                transform.rotation = Quaternion.Euler(0, aimRotation, 0);
            }
            else
            {
                transform.Rotate(new Vector3(0, rotationAngle / rotationTime * Time.deltaTime, 0));
            }
            t += Time.deltaTime / rotationTime;
            yield return null;
        }
        if(characterSlots[StaticProfile.choosedCharacterSlot].CharacterProfileStats == null)
        {
            createButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(true);
        }
    }

    public void PlayButton()
    {
        //Save the choosedCharacter!
        Debug.Log("PlayButton!");
    }

    private enum RotationDirection
    {
        left, right
    }

    public void CreateSuccess()
    {
        Invoke("SuccessCreating", 2);
    }

    public void CreateCanceled()
    {
        Invoke("CanceledCreating", 2);
    }

    private void SuccessCreating()
    {
        createButton.SetActive(false);
        playButton.SetActive(true);
    }

    private void CanceledCreating()
    {
        createButton.SetActive(true);
        playButton.SetActive(false);
    }
}
