using UnityEngine;
using UnityEngine.UI;

public class PlayfabAuth : MonoBehaviour
{
    private Selectable selectedLoginInputField = null;
    private Selectable selectedSignUpInputField = null;

    [Header("Log In Settings")]
    [SerializeField]
    private InputField LoginUsername;
    [SerializeField]
    private InputField LoginPassword;
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

    [Header("Sign up Error texts")]
    [SerializeField]
    private Text SignUpUsernameErrorText;
    [SerializeField]
    private Text SignUpPasswordErrorText;
    [SerializeField]
    private Text SignUpConfirmPasswordErrorText;
    [SerializeField]
    private Text SignUpEmailErrorText;

    private int fieldCount = 4;
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

    public void Login()
    {
        AccountInfo.Login(LoginUsername1.text, LoginPassword1.text);
    }

    public void SignUp()
    {
        if (SignUpConfirmPassword1.text == SignUpPassword1.text)
        {
            AccountInfo.SignUp(SignUpUsername1.text, SignUpPassword1.text, SignUpEmail1.text);
        }
        else
        {
            Debug.Log("Not the same pw!");
        }
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

    #region Username check

    public void UserNameValueEdit()
    {
        if (SignUpUsername.text.Length == 0)
        {
            SignUpUsernameErrorText.text = "Required!";
            UsernameError();
        }
        else if (SignUpUsername.text.Length < 3)
        {
            SignUpUsernameErrorText.text = "Username must be at least 3 characters long!";
            UsernameError();
        }
        else
        {
            SignUpUsernameErrorText.text = "";
            UsernameCorrect();
        }
    }

    private void UsernameError()
    {
        if (correctSignUpUserName)
        {
            FieldError();
            correctSignUpUserName = false;
        }
    }

    private void UsernameCorrect()
    {
        if (!correctSignUpUserName)
        {
            FieldCorrect();
            correctSignUpUserName = true;
        }
    }
    #endregion

    #region Password check

    public void PasswordValueEdit()
    {
        if (SignUpPassword.text.Length == 0)
        {
            SignUpPasswordErrorText.text = "Required!";
            PasswordError();
        }
        else if (SignUpPassword.text.Length < 6)
        {
            SignUpPasswordErrorText.text = "Password must be at least 6 characters long!";
            PasswordError();
        }
        else
        {
            if (SignUpConfirmPassword.text.Length > 0)
            {
                if (SignUpConfirmPassword1.text != SignUpPassword1.text)
                {
                    SignUpPasswordErrorText.text = "Password and Confirm password must be the same!";
                    PasswordError();
                }
                else
                {
                    SignUpPasswordErrorText.text = "";
                    PasswordCorrect();
                }
            }
            else
            {
                SignUpPasswordErrorText.text = "";
                PasswordCorrect();
            }
        }
    }

    private void PasswordError()
    {
        if (correctSignUpPassword)
        {
            correctSignUpPassword = false;
            FieldError();
        }
    }

    private void PasswordCorrect()
    {
        if (!correctSignUpPassword)
        {
            correctSignUpPassword = true;
            FieldCorrect();
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
            SignUpConfirmPasswordErrorText.text = "Confirm password must be at least 6 characters long!";
            ConfirmPasswordError();
        }
        else
        {
            if (SignUpPassword.text.Length > 0)
            {
                if (SignUpConfirmPassword1.text != SignUpPassword1.text)
                {
                    SignUpConfirmPasswordErrorText.text = "Password and Confirm password must be the same!";
                    ConfirmPasswordError();
                }
                else
                {
                    SignUpConfirmPasswordErrorText.text = "";
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
            FieldError();
        }
    }

    private void ConfirmPasswordCorrect()
    {
        if (!correctSignUpConfirmPassword)
        {
            correctSignUpConfirmPassword = true;
            FieldCorrect();
        }
    }
    #endregion

    #region Email check

    public void EmailValueEdit()
    {
        string [] parts = SignUpEmail1.text.Split('@');
        if(parts.Length == 2)
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
            FieldError();
        }
    }

    private void EmailCorrect()
    {
        if (!correctSignUpEmail)
        {
            correctSignUpEmail = true;
            FieldCorrect();
        }
    }
    #endregion

    private void FieldError()
    {
        fieldCount++;
        if (fieldCount == 1)
        {
            signUpButton.interactable = false;
        }
    }

    private void FieldCorrect()
    {
        fieldCount--;
        if (fieldCount == 0)
        {
            signUpButton.interactable = true;
        }
    }

    #region Navigation

    public void NextLoginField()
    {
        if(selectedLoginInputField == null)
        {
            selectedLoginInputField = LoginUsername;
            LoginUsername.Select();
        }
        else
        {
            Selectable next = selectedLoginInputField.FindSelectableOnDown();
            next.Select();
            selectedLoginInputField = next;
        }
    }

    public void NextSignUpField()
    {
        if(selectedSignUpInputField == null)
        {
            selectedSignUpInputField = SignUpUsername;
            SignUpUsername.Select();
        }
        else
        {
            Selectable next = selectedSignUpInputField.FindSelectableOnDown();
            next.Select();
            selectedSignUpInputField = next;
        }
    }

    #endregion
}
