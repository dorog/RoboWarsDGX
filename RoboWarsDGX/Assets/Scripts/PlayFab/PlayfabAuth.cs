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

    public void Login()
    {
        AccountInfo.Login(LoginUsername.text, LoginPassword.text);
    }

    public void SignUp()
    {
        if(SignUpConfirmPassword.text == SignUpPassword.text)
        {
            AccountInfo.SignUp(SignUpUsername.text, SignUpPassword.text, SignUpEmail.text);
        }
        else
        {
            Debug.Log("Not the same pw!");
        }
    }
}
