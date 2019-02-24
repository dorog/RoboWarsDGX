using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    private readonly string loginInProgress = "Login in progress...";

    [Header("Disable buttons")]
    [SerializeField]
    private Button loginButton;
    [SerializeField]
    private Button signUpMenuButton;

    [Header("UIs")]
    [SerializeField]
    private GameObject loginInProgressUI;
    [SerializeField]
    private Text loginInStateText;
    [SerializeField]
    private GameObject failUI;
    [SerializeField]
    private Text failErrorText;
    [SerializeField]
    private Button failButton;

    public Button LoginButton { get => loginButton; set => loginButton = value; }
    public Button SignUpMenuButton { get => signUpMenuButton; set => signUpMenuButton = value; }
    public GameObject LoginInProgressUI { get => loginInProgressUI; set => loginInProgressUI = value; }
    public Text LoginInStateText { get => LoginInStateText1; set => LoginInStateText1 = value; }
    public Text LoginInStateText1 { get => loginInStateText; set => loginInStateText = value; }
    public GameObject FailUI { get => failUI; set => failUI = value; }
    public Text FailErrorText { get => failErrorText; set => failErrorText = value; }
    public Button FailButton1 { get => failButton; set => failButton = value; }

    private void Start()
    {
        loginInProgressUI.SetActive(false);
        failUI.SetActive(false);
        loginButton.interactable = false;
    }

    public void LogInInProgress()
    {
        loginInProgressUI.SetActive(true);
        LoginInStateText.text = loginInProgress;
    }

    public void LogInGettingData(string msg)
    {
        LoginInStateText.text = msg;
    }

    public void LoginFail(string error)
    {
        loginInProgressUI.SetActive(false);
        FailUI.SetActive(true);
        FailErrorText.text = error + "!";

        FailButton1.Select();
    }

    public void FailButton()
    {
        FailUI.SetActive(false);
        SignUpMenuButton.interactable = true;
        loginButton.interactable = true;
    }
}
