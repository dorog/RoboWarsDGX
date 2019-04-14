using UnityEngine;
using UnityEngine.UI;

public class LoginMenuUI : MonoBehaviour
{
    private const string usernameKey = "username";
    private const string passwordKey = "password";

    public PlayfabAuth auth;

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
    private Image loginButtonImage;

    [Header("Sign up menu")]
    [SerializeField]
    private GameObject signUpMenu;
    [SerializeField]
    private Image signUpButtonImage;
    [SerializeField]
    private Button signUpButton;

    private MenuType selectedMenu;

    private MenuType StartMenu { get => startMenu; set => startMenu = value; }
    public Color InActiveButtonColor { get => inActiveButtonColor; set => inActiveButtonColor = value; }
    public Color ActiveButtonColor { get => activeButtonColor; set => activeButtonColor = value; }
    public GameObject LoginMenu { get => loginMenu; set => loginMenu = value; }
    public Image LoginButtonImage { get => loginButtonImage; set => loginButtonImage = value; }
    public GameObject SignUpMenu { get => signUpMenu; set => signUpMenu = value; }
    public Image SignUpButtonImage { get => signUpButtonImage; set => signUpButtonImage = value; }
    public Button SignUpButton { get => signUpButton; set => signUpButton = value; }

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
        LoadRememberMe();

        selectedMenu = MenuType.Login;
       
        LoginMenu.SetActive(true);
        signUpButton.interactable = true;
        SignUpMenu.SetActive(false);
        SignUpButtonImage.color = InActiveButtonColor;
        LoginButtonImage.color = ActiveButtonColor;

        auth.FirstLoginField();
    }

    public void ShowSignUp()
    {
        selectedMenu = MenuType.SignUp;

        SignUpMenu.SetActive(true);
        signUpButton.interactable = false;
        SignUpButtonImage.color = ActiveButtonColor;
        LoginMenu.SetActive(false);
        LoginButtonImage.color = InActiveButtonColor;

        auth.FirstSignUpField();
    }

    private enum MenuType
    {
        Login, SignUp
    }

    #region Navigation

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(selectedMenu == MenuType.Login)
            {
                auth.NextLoginField();
            }
            else
            {
                auth.NextSignUpField();
            }
        }
    }

    #endregion

    #region Remember me

    private void LoadRememberMe()
    {
        if (PlayerPrefs.HasKey(usernameKey) && PlayerPrefs.HasKey(passwordKey))
        {
            auth.LoginInputFieldSetter(PlayerPrefs.GetString(usernameKey), PlayerPrefs.GetString(passwordKey));
        }
    }

    public void SavePlayerPrefs(string username, string password)
    {
        PlayerPrefs.SetString(usernameKey, username);
        PlayerPrefs.SetString(passwordKey, password);
        PlayerPrefs.Save();
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteKey(usernameKey);
        PlayerPrefs.DeleteKey(passwordKey);
    }

    #endregion
}
