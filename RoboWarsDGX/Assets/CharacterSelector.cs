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

    private CharacterSlot[] characterSlots;

    void Start()
    {
        FileManager.Load();
        CreateSlots();
        Rotate();
        SlotCheck();
    }

    private void CreateSlots()
    {
        float alphaDelta = 360 / slotNumber;
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
            transform.Rotate(0, StaticProfile.choosedCharacterSlot * slotNumber, 0);
        }
    }

    private void SlotCheck()
    {
        if(characterSlots[StaticProfile.choosedCharacterSlot].CharacterProfileStats == null){
            createButton.SetActive(true);
        }
        else
        {
            createButton.SetActive(false);
        }
    }

    public void CreateCharacter(CharacterType type)
    {
        characterSlots[StaticProfile.choosedCharacterSlot].InstantiateCharacter(type);
        createButton.SetActive(false);
    }
}
