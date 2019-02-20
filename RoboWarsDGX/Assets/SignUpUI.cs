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
    private Button signupMenuButton;
    [SerializeField]
    private Button signUpButton;
    [Header ("Sub UIs")]
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
    private GameObject background;

    public Button LoginMenuButton { get => loginMenuButton; set => loginMenuButton = value; }
    public Button SignupMenuButton { get => signupMenuButton; set => signupMenuButton = value; }
    public Button SignUpButton { get => signUpButton; set => signUpButton = value; }
    public GameObject SignUpInProgressUI { get => signUpInProgressUI; set => signUpInProgressUI = value; }
    public GameObject SuccessUI { get => successUI; set => successUI = value; }
    public GameObject Background { get => background; set => background = value; }
    public GameObject FailUI { get => failUI; set => failUI = value; }

    private void Start()
    {
        SignUpViewReset();
    }

    public void SignUpInProgress()
    {
        progressStateText.text = signUpInProgress;
        LoginMenuButton.interactable = false;
        SignUpButton.interactable = false;
        SignupMenuButton.interactable = false;
        SignUpInProgressUI.SetActive(true);
        Background.SetActive(true);
    }

    public void AccountSetUp()
    {
        progressStateText.text = accountSetUp;
    }

    public void SignUpSuccess()
    {
        LoginMenuButton.interactable = true;
        SignUpButton.interactable = true;
        SignupMenuButton.interactable = true;
        SignUpInProgressUI.SetActive(false);
        SuccessUI.SetActive(true);

        auth.ResetSignUpFields();
    }

    public void SignUpViewReset()
    {
        signUpInProgressUI.SetActive(false);
        successUI.SetActive(false);
        FailUI.SetActive(false);
        background.SetActive(false);

        signUpButton.interactable = false;

        auth.ResetSignUpFields();
    }

    public void SignUpFail(string error)
    {
        signUpInProgressUI.SetActive(false);
        FailUI.SetActive(true);
        failErrorText.text = error + "!";
    }

    public void FailButton()
    {
        LoginMenuButton.interactable = true;
        SignUpButton.interactable = true;
        SignupMenuButton.interactable = true;
        background.SetActive(false);
        FailUI.SetActive(false);
    }
}
