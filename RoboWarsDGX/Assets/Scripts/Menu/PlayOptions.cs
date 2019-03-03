using UnityEngine;
using UnityEngine.UI;

public class PlayOptions : MonoBehaviour
{
    [SerializeField]
    private Options startOption = Options.Join;

    private Options actualMenu;

    [Header ("Join menu")]
    public GameObject joinMenu;
    public Button joinButton;

    [Header("Join menu")]
    public GameObject createMenu;
    public Button createButton;

    [Header("Playguard")]
    public GameObject playGuard;
    private bool playGuardOff = false;

    void Start()
    {
        if (startOption == Options.Join)
        {
            joinMenu.SetActive(true);
            createMenu.SetActive(false);

            joinButton.interactable = false;

            actualMenu = Options.Join;
        }
        else
        {
            joinMenu.SetActive(false);
            createMenu.SetActive(true);

            createButton.interactable = false;

            actualMenu = Options.Create;
        }
    }

    private void OnEnable()
    {
        if (!playGuardOff)
        {
            PlayGuardCheck();
        }
    }

    private void PlayGuardCheck()
    {
        if (AccountInfo.Instance.ownCharacters.Count > 0 && AccountInfo.Instance.ownWeapons.Count > 0)
        {
            playGuard.SetActive(false);
            playGuardOff = true;
        }
    }

    public void ChangeMenu()
    {
        if (actualMenu == Options.Join)
        {
            joinMenu.SetActive(false);
            createMenu.SetActive(true);
            actualMenu = Options.Create;

            joinButton.interactable = true;
            createButton.interactable = false;
        }
        else
        {
            joinMenu.SetActive(true);
            createMenu.SetActive(false);
            actualMenu = Options.Join;

            joinButton.interactable = false;
            createButton.interactable = true;
        }
    }

    private enum Options
    {
        Join, Create
    }
}
