using UnityEngine;
using UnityEngine.UI;

public class PlayfabAuth : MonoBehaviour
{
    private Selectable selectedLoginInputField = null;
    private Selectable selectedSignUpInputField = null;

    private bool hasPreviousLogInData = false;

    public LoginMenuUI loginMenuUI;

    [Header("Log In Settings")]
    [SerializeField]
    private InputField LoginUsername;
    [SerializeField]
    private InputField LoginPassword;
    [SerializeField]
    private Toggle loginRememberMe;
    [SerializeField]
    private Button loginButton;

    [Header("Sign Up Settings")]
    [SerializeField]
    private InputField SignUpUsername;
    [SerializeField]
    private InputField SignUpPassword;
    [SerializeField]
    private InputField SignUpConfirmPassword;
    [SerializeField]
    private InputField SignUpEmail;
    [SerializeField]
    private Button signUpButton;

    private int logInFieldCount = 2;
    private bool correctLoginUserName = false;
    private bool correctLoginPassword = false;

    [Header("Sign up Error texts")]
    [SerializeField]
    private Text SignUpUsernameErrorText;
    [SerializeField]
    private Text SignUpPasswordErrorText;
    [SerializeField]
    private Text SignUpConfirmPasswordErrorText;
    [SerializeField]
    private Text SignUpEmailErrorText;

    private int signUpFieldCount = 4;
    private bool correctSignUpUserName = false;
    private bool correctSignUpPassword = false;
    private bool correctSignUpConfirmPassword = false;
    private bool correctSignUpEmail = false;

    public InputField LoginUsername1 { get => LoginUsername; set => LoginUsername = value; }
    public InputField LoginPassword1 { get => LoginPassword; set => LoginPassword = value; }
    public InputField SignUpUsername1 { get => SignUpUsername; set => SignUpUsername = value; }
    public InputField SignUpPassword1 { get => SignUpPassword; set => SignUpPassword = value; }
    public InputField SignUpConfirmPassword1 { get => SignUpConfirmPassword; set => SignUpConfirmPassword = value; }
    public InputField SignUpEmail1 { get => SignUpEmail; set => SignUpEmail = value; }
    public Text SignUpUsernameErrorText1 { get => SignUpUsernameErrorText; set => SignUpUsernameErrorText = value; }
    public Text SignUpPasswordErrorText1 { get => SignUpPasswordErrorText; set => SignUpPasswordErrorText = value; }
    public Text SignUpConfirmPasswordErrorText1 { get => SignUpConfirmPasswordErrorText; set => SignUpConfirmPasswordErrorText = value; }
    public Text SignUpEmailErrorText1 { get => SignUpEmailErrorText; set => SignUpEmailErrorText = value; }
    public Button SignUpButton { get => signUpButton; set => signUpButton = value; }
    public Button LoginButton { get => loginButton; set => loginButton = value; }

    public void Login()
    {
        if (loginRememberMe.isOn)
        {
            loginMenuUI.SavePlayerPrefs(LoginUsername.text, LoginPassword.text);
        }
        else
        {
            loginMenuUI.DeletePlayerPrefs();
        }
        AccountInfo.Login(LoginUsername1.text, LoginPassword1.text);
    }

    public void SignUp()
    {

        AccountInfo.SignUp(SignUpUsername1.text, SignUpPassword1.text, SignUpEmail1.text);
    }

    public void ResetSignUpFields()
    {
        SignUpUsername.text = "";
        SignUpPassword.text = "";
        SignUpConfirmPassword.text = "";
        SignUpEmail.text = "";

        SignUpUsernameErrorText.text = "";
        SignUpPasswordErrorText.text = "";
        SignUpConfirmPasswordErrorText.text = "";
        SignUpEmailErrorText.text = "";
    }
    #region SignUp check

    #region SignUp Username check

    public void SignUpUserNameValueEdit()
    {
        if (SignUpUsername.text.Length == 0)
        {
            SignUpUsernameErrorText.text = "Required!";
            SignUpUsernameError();
        }
        else if (SignUpUsername.text.Length < 3)
        {
            SignUpUsernameErrorText.text = "Username must be at least 3 characters long!";
            SignUpUsernameError();
        }
        else
        {
            SignUpUsernameErrorText.text = "";
            SignUpUsernameCorrect();
        }
    }

    private void SignUpUsernameError()
    {
        if (correctSignUpUserName)
        {
            SignUpFieldError();
            correctSignUpUserName = false;
        }
    }

    private void SignUpUsernameCorrect()
    {
        if (!correctSignUpUserName)
        {
            SignUpFieldCorrect();
            correctSignUpUserName = true;
        }
    }
    #endregion

    #region SignUp Password check

    public void SignUpPasswordValueEdit()
    {
        if (SignUpPassword.text.Length == 0)
        {
            SignUpPasswordErrorText.text = "Required!";
            SignUpPasswordError();
        }
        else if (SignUpPassword.text.Length < 6)
        {
            SignUpPasswordErrorText.text = "PW must be at least 6 characters!";
            SignUpPasswordError();
        }
        else
        {
            if (SignUpConfirmPassword.text.Length > 0)
            {
                if (SignUpConfirmPassword1.text != SignUpPassword1.text)
                {
                    SignUpPasswordErrorText.text = "PW and CPW must be the same!";
                    SignUpPasswordError();
                }
                else
                {
                    SignUpPasswordErrorText.text = "";
                    SignUpConfirmPasswordErrorText.text = "";
                    SignUpPasswordCorrect();
                    ConfirmPasswordCorrect();
                }
            }
            else
            {
                SignUpPasswordErrorText.text = "";
                SignUpPasswordCorrect();
            }
        }
    }

    private void SignUpPasswordError()
    {
        if (correctSignUpPassword)
        {
            correctSignUpPassword = false;
            SignUpFieldError();
        }
    }

    private void SignUpPasswordCorrect()
    {
        if (!correctSignUpPassword)
        {
            correctSignUpPassword = true;
            SignUpFieldCorrect();
        }
    }
    #endregion

    #region ConfirmPassword check

    public void ConfirmPasswordValueEdit()
    {
        if (SignUpConfirmPassword.text.Length == 0)
        {
            SignUpConfirmPasswordErrorText.text = "Required!";
            ConfirmPasswordError();
        }
        else if (SignUpConfirmPassword.text.Length < 6)
        {
            SignUpConfirmPasswordErrorText.text = "CPW must be at least 6 characters!";
            ConfirmPasswordError();
        }
        else
        {
            if (SignUpPassword.text.Length > 0)
            {
                if (SignUpConfirmPassword1.text != SignUpPassword1.text)
                {
                    SignUpConfirmPasswordErrorText.text = "PW and CPW must be the same!";
                    ConfirmPasswordError();
                }
                else
                {
                    SignUpPasswordErrorText.text = "";
                    SignUpConfirmPasswordErrorText.text = "";
                    SignUpPasswordCorrect();
                    ConfirmPasswordCorrect();
                }
            }
            else
            {
                SignUpConfirmPasswordErrorText.text = "";
                ConfirmPasswordCorrect();
            }
        }
    }

    private void ConfirmPasswordError()
    {
        if (correctSignUpConfirmPassword)
        {
            correctSignUpConfirmPassword = false;
            SignUpFieldError();
        }
    }

    private void ConfirmPasswordCorrect()
    {
        if (!correctSignUpConfirmPassword)
        {
            correctSignUpConfirmPassword = true;
            SignUpFieldCorrect();
        }
    }
    #endregion

    #region Email check

    public void EmailValueEdit()
    {
        string[] parts = SignUpEmail1.text.Split('@');
        if (parts.Length == 2)
        {
            string[] endparts = parts[1].Split('.');
            if (endparts.Length == 2)
            {
                SignUpEmailErrorText.text = "";
                EmailCorrect();
            }
            else
            {
                SignUpEmailErrorText.text = "Invalid E-mail address!";
                EmailError();
            }
        }
        else
        {
            SignUpEmailErrorText.text = "Invalid E-mail address!";
            EmailError();
        }
    }

    private void EmailError()
    {
        if (correctSignUpEmail)
        {
            correctSignUpEmail = false;
            SignUpFieldError();
        }
    }

    private void EmailCorrect()
    {
        if (!correctSignUpEmail)
        {
            correctSignUpEmail = true;
            SignUpFieldCorrect();
        }
    }
    #endregion

    private void SignUpFieldError()
    {
        signUpFieldCount++;
        if (signUpFieldCount == 1)
        {
            SignUpButton.interactable = false;
        }
    }

    private void SignUpFieldCorrect()
    {
        signUpFieldCount--;
        if (signUpFieldCount == 0)
        {
            SignUpButton.interactable = true;
        }
    }

    #endregion

    #region Login check

    #region Login Username check

    public void LoginUserNameValueEdit()
    {
        if (LoginUsername.text.Length < 3)
        {
            LoginUsernameError();
        }
        else
        {
            LoginUsernameCorrect();
        }
    }

    private void LoginUsernameError()
    {
        if (correctLoginUserName)
        {
            LoginFieldError();
            correctLoginUserName = false;
        }
    }

    private void LoginUsernameCorrect()
    {
        if (!correctLoginUserName)
        {
            LoginFieldCorrect();
            correctLoginUserName = true;
        }
    }
    #endregion

    #region Login Password check

    public void LoginPasswordValueEdit()
    {
        if (LoginPassword.text.Length < 6)
        {
            LoginPasswordError();
        }
        else
        {
            LoginPasswordCorrect();
        }
    }

    private void LoginPasswordError()
    {
        if (correctLoginPassword)
        {
            correctLoginPassword = false;
            LoginFieldError();
        }
    }

    private void LoginPasswordCorrect()
    {
        if (!correctLoginPassword)
        {
            correctLoginPassword = true;
            LoginFieldCorrect();
        }
    }
    #endregion

    private void LoginFieldError()
    {
        logInFieldCount++;
        if (logInFieldCount == 1)
        {
            LoginButton.interactable = false;
        }
    }

    private void LoginFieldCorrect()
    {
        logInFieldCount--;
        if (logInFieldCount == 0)
        {
            LoginButton.interactable = true;
        }
    }

    #endregion

    #region Navigation

    public void NextLoginField()
    {
        if (selectedLoginInputField != null)
        {
            Selectable next = selectedLoginInputField.FindSelectableOnDown();
            next.Select();
            selectedLoginInputField = next;
        }
    }

    public void NextSignUpField()
    {
        if (selectedSignUpInputField != null)
        {
            Selectable next = selectedSignUpInputField.FindSelectableOnDown();
            next.Select();
            selectedSignUpInputField = next;
        }
    }

    public void FirstLoginField()
    {
        if (selectedLoginInputField == null)
        {
            selectedLoginInputField = LoginUsername;
            LoginUsername.Select();
        }
    }

    public void FirstSignUpField()
    {
        if (selectedSignUpInputField == null)
        {
            selectedSignUpInputField = SignUpUsername;
            SignUpUsername.Select();
        }
    }

    public void SelectLoginSelected()
    {
        selectedLoginInputField.Select();
    }

    public void SelectSignUpSelected()
    {
        selectedSignUpInputField.Select();
    }

    public void SelectSignUpFirst()
    {
        selectedSignUpInputField = SignUpUsername;
        SignUpUsername.Select();
    }

    #endregion

    public void LoginInputFieldSetter(string username, string password)
    {
        LoginUsername.text = username;
        LoginPassword.text = password;

        selectedLoginInputField = LoginButton;
        hasPreviousLogInData = true;
    }

    private void Update()
    {
        if (hasPreviousLogInData)
        {
            hasPreviousLogInData = false;
            loginButton.interactable = true;

            LoginButton.Select();
            loginRememberMe.isOn = true;
        }
    }
}
