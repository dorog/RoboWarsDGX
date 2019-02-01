using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    [SerializeField]
    private InputField userName;
    [SerializeField]
    private InputField password;
    [SerializeField]
    private InputField confirmPassword;
    [SerializeField]
    private InputField email;

    public void CreateAccount()
    {
        if(password.text == confirmPassword.text)
        {
            RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
            request.Username = userName.text;
            request.Password = confirmPassword.text;
            request.Email = email.text;
            request.DisplayName = userName.text;

            PlayFabClientAPI.RegisterPlayFabUser(request, result =>
            {
                Alerts a = new Alerts();
                StartCoroutine(a.CreateNewAlert(result.Username + " Has been created!"));
            }, error =>
            {
                Alerts a = new Alerts();
                StartCoroutine(a.CreateNewAlert(error.ErrorMessage));
            });
        }
        else
        {
            Alerts a = new Alerts();
            StartCoroutine(a.CreateNewAlert("Confirm password and password are not the same!"));
        }
    }
}
