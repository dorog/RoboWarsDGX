using UnityEngine;
using UnityEngine.UI;

public class SignUpUI : MonoBehaviour
{
    private readonly string signUpInProgress = "Sign Up in progress...";
    private readonly string accountSetUp = "Setting up account...";

    [SerializeField]
    private PlayfabAuth auth;
    [Header ("Disabled Buttons")]
    [SerializeField]
    private Button loginMenuButton;
    [SerializeField]
    private Button signUpButton;
    [Header ("UIs")]
    [SerializeField]
    private GameObject signUpInProgressUI;
    [SerializeField]
    private Text progressStateText;

    [SerializeField]
    private GameObject successUI;

    [SerializeField]
    private GameObject failUI;
    [SerializeField]
    private Text failErrorText;
    [SerializeField]
    private Button failButton;

    public Button LoginMenuButton { get => loginMenuButton; set => loginMenuButton = value; }
    public Button SignUpButton { get => signUpButton; set => signUpButton = value; }
    public GameObject SignUpInProgressUI { get => signUpInProgressUI; set => signUpInProgressUI = value; }
    public GameObject SuccessUI { get => successUI; set => successUI = value; }
    public GameObject FailUI { get => failUI; set => failUI = value; }
    public Text FailErrorText { get => failErrorText; set => failErrorText = value; }
    public Text ProgressStateText { get => progressStateText; set => progressStateText = value; }
    public PlayfabAuth Auth { get => auth; set => auth = value; }
    public Button FailButton1 { get => failButton; set => failButton = value; }

    private void Start()
    {
        SignUpViewReset();
    }

    public void SignUpInProgress()
    {
        ProgressStateText.text = signUpInProgress;
        LoginMenuButton.interactable = false;
        SignUpButton.interactable = false;
        SignUpInProgressUI.SetActive(true); 
    }

    public void AccountSetUp()
    {
        ProgressStateText.text = accountSetUp;
    }

    public void SignUpSuccess()
    {
        LoginMenuButton.interactable = true;
        SignUpButton.interactable = true;
        SignUpInProgressUI.SetActive(false);
        SuccessUI.SetActive(true);

        Auth.ResetSignUpFields();
    }

    public void SignUpViewReset()
    {
        signUpInProgressUI.SetActive(false);
        successUI.SetActive(false);
        FailUI.SetActive(false);

        signUpButton.interactable = false;

        Auth.ResetSignUpFields();
    }

    public void SignUpFail(string error)
    {
        signUpInProgressUI.SetActive(false);
        FailUI.SetActive(true);
        FailErrorText.text = error + "!";

        FailButton1.Select();
    }

    public void FailButton()
    {
        LoginMenuButton.interactable = true;
        signUpButton.interactable = true;
        FailUI.SetActive(false);
    }
}
