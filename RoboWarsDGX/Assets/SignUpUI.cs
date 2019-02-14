using UnityEngine;
using UnityEngine.UI;

public class SignUpUI : MonoBehaviour
{
    [SerializeField]
    private Button loginMenuButton;
    [SerializeField]
    private Button signupMenuButton;
    [SerializeField]
    private Button signUpButton;
    [SerializeField]
    private Transform image;
    [SerializeField]
    private float rotationSpeed = 5f;

    private bool inLoading = false;

    public Button LoginMenuButton { get => loginMenuButton; set => loginMenuButton = value; }
    public Button SignupMenuButton { get => signupMenuButton; set => signupMenuButton = value; }
    public Button SignUpButton { get => signUpButton; set => signUpButton = value; }
    public Transform Image { get => image; set => image = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }

    void Update()
    {
        if (inLoading)
        {
            Image.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime));
        }
    }

    public void SignUpInProgress()
    {
        LoginMenuButton.interactable = false;
        SignUpButton.interactable = false;
        SignupMenuButton.interactable = false;
        Image.gameObject.SetActive(true);
        inLoading = true;
    }

    public void SignUpSuccess()
    {
        LoginMenuButton.interactable = true;
        SignUpButton.interactable = true;
        SignupMenuButton.interactable = true;
        Image.gameObject.SetActive(false);
        inLoading = false;

        //TODO: pipa megjelenites
    }
}
