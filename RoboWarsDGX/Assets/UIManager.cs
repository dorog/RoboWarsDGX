using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void Register()
    {
        SceneManager.LoadSceneAsync("Register", LoadSceneMode.Additive);
    }

    public void Login()
    {
        SceneManager.LoadSceneAsync("Login", LoadSceneMode.Additive);
    }
}
