using UnityEngine;
using UnityEngine.UI;

public class PlayfabAuth : MonoBehaviour
{
    [Header("Log In Settings")]
    [SerializeField]
    private InputField LoginUsername;
    [SerializeField]
    private InputField LoginPassword;
    [Header("Sign Up Settings")]
    [SerializeField]
    private InputField SignUpUsername;
    [SerializeField]
    private InputField SignUpPassword;
    [SerializeField]
    private InputField SignUpConfirmPassword;
    [SerializeField]
    private InputField SignUpEmail;

    public InputField LoginUsername1 { get => LoginUsername; set => LoginUsername = value; }
    public InputField LoginPassword1 { get => LoginPassword; set => LoginPassword = value; }
    public InputField SignUpUsername1 { get => SignUpUsername; set => SignUpUsername = value; }
    public InputField SignUpPassword1 { get => SignUpPassword; set => SignUpPassword = value; }
    public InputField SignUpConfirmPassword1 { get => SignUpConfirmPassword; set => SignUpConfirmPassword = value; }
    public InputField SignUpEmail1 { get => SignUpEmail; set => SignUpEmail = value; }

    public void Login()
    {
        AccountInfo.Login(LoginUsername1.text, LoginPassword1.text);
    }

    public void SignUp()
    {
        if(SignUpConfirmPassword1.text == SignUpPassword1.text)
        {
            AccountInfo.SignUp(SignUpUsername1.text, SignUpPassword1.text, SignUpEmail1.text);
        }
        else
        {
            Debug.Log("Not the same pw!");
        }
    }
}
