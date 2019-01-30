using UnityEngine;

public class SelectCamera : MonoBehaviour
{
    [Header("Select Menu Settings")]
    [SerializeField]
    private GameObject selectCharacterMenuCamera;
    [SerializeField]
    private GameObject selectMenuUI;

    [Header("Create Menu Settings")]
    [SerializeField]
    private GameObject createCharacterMenuCamera;
    [SerializeField]
    private GameObject createMenuUI;

    [SerializeField]
    private float timeBetweenCameraChanging = 2.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GoSelectMenu();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GoCreateMenu();
        }
    }

    public void GoSelectMenu()
    {
        createMenuUI.SetActive(false);

        createCharacterMenuCamera.SetActive(false);
        selectCharacterMenuCamera.SetActive(true);


        //TimeLine or animation?
        Invoke("ActiveSelectMenuUI", timeBetweenCameraChanging);
    }

    public void GoCreateMenu()
    {
        selectMenuUI.SetActive(false);

        selectCharacterMenuCamera.SetActive(false);
        createCharacterMenuCamera.SetActive(true);

        //TimeLine or animation?
        Invoke("ActivateCreateMenuUI", timeBetweenCameraChanging);
    }

    private void ActivateCreateMenuUI()
    {
        createMenuUI.SetActive(true);
    }

    private void ActiveSelectMenuUI()
    {
        selectMenuUI.SetActive(true);
    }
}
