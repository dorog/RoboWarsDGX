using UnityEngine;
using UnityEngine.UI;

public class LoginMenuUI : MonoBehaviour
{
    [SerializeField]
    private MenuType startMenu = MenuType.Login;
    [SerializeField]
    private Color inActiveButtonColor = Color.blue;
    [SerializeField]
    private Color activeButtonColor = Color.black;

    [Header("Login menu")]
    [SerializeField]
    private GameObject loginMenu;
    [SerializeField]
    private Image loginButton;

    [Header("Sign up menu")]
    [SerializeField]
    private GameObject signUpMenu;
    [SerializeField]
    private Image signUpButton;

    private MenuType StartMenu { get => startMenu; set => startMenu = value; }
    public Color InActiveButtonColor { get => inActiveButtonColor; set => inActiveButtonColor = value; }
    public Color ActiveButtonColor { get => activeButtonColor; set => activeButtonColor = value; }
    public GameObject LoginMenu { get => loginMenu; set => loginMenu = value; }
    public Image LoginButton { get => loginButton; set => loginButton = value; }
    public GameObject SignUpMenu { get => signUpMenu; set => signUpMenu = value; }
    public Image SignUpButton { get => signUpButton; set => signUpButton = value; }

    void Start()
    {
        if(StartMenu == MenuType.Login)
        {
            ShowLogIn();
        }
        else
        {
            ShowSignUp();
        }
    }

    public void ShowLogIn()
    {
        LoginMenu.SetActive(true);
        LoginButton.color = ActiveButtonColor;
        SignUpMenu.SetActive(false);
        SignUpButton.color = InActiveButtonColor;
    }

    public void ShowSignUp()
    {
        SignUpMenu.SetActive(true);
        SignUpButton.color = ActiveButtonColor;
        LoginMenu.SetActive(false);
        LoginButton.color = InActiveButtonColor;
    }

    private enum MenuType
    {
        Login, SignUp
    }
}
