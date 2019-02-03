using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AccountInfo : MonoBehaviour
{
    private static readonly string lobbyScene = "Lobby";

    [SerializeField]
    private static AccountInfo instance;

    public static AccountInfo Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    [SerializeField]
    private GetPlayerCombinedInfoResultPayload info;

    public GetPlayerCombinedInfoResultPayload Info
    {
        get { return info;  } set { info = value;  }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region Log in
    public static void Login(string username, string password)
    {
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest()
        {
            Username = username,
            Password = password
        };

        PlayFabClientAPI.LoginWithPlayFab(request, OnLogInSuccess, OnLogInError);
    }
    private static void OnLogInSuccess(LoginResult result)
    {
        GetAccountInfo(result.PlayFabId);
        Database.UpdateDatabase();
        SceneManager.LoadScene(lobbyScene);
    }

    private static void OnLogInError(PlayFabError error)
    {
        Debug.Log("Error during log in: " + error);
    }

    #endregion

    #region Sign Up
    public static void SignUp(string username, string password, string email)
    {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            Email = email,
            Username = username,
            Password = password
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterError);
    }

    private static void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Success sign up!");
        Instance.SetUpAccount();
    }

    private static void OnRegisterError(PlayFabError error)
    {
        Debug.Log("Error (sign up):" + error);
    }
    #endregion

    public static void GetAccountInfo(string playfabId)
    {
        GetPlayerCombinedInfoRequestParams paramInfo = new GetPlayerCombinedInfoRequestParams
        {
            GetTitleData = true,
            GetUserInventory = true,
            GetUserAccountInfo = true,
            GetUserVirtualCurrency = true,
            GetPlayerProfile = true,
            GetPlayerStatistics = true,
            GetUserData = true,
            GetUserReadOnlyData = true
        };

        GetPlayerCombinedInfoRequest request = new GetPlayerCombinedInfoRequest()
        {
            PlayFabId = playfabId,
            InfoRequestParameters = paramInfo
        };

        PlayFabClientAPI.GetPlayerCombinedInfo(request, GotAccountInfo, DontGetAccountInfo);
    }

    private static void GotAccountInfo(GetPlayerCombinedInfoResult result)
    {
        Instance.info = result.InfoResultPayload;
    }

    private static void DontGetAccountInfo(PlayFabError error)
    {
        Debug.Log("AccountInfor error: " + error);
    }

    private void SetUpAccount()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add(UIManager.exp, "0");

        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = data
        };

        PlayFabClientAPI.UpdateUserData(request, UpdateDataSuccess, UpdateDataFail);
    }

    private void UpdateDataSuccess(UpdateUserDataResult result)
    {
        Debug.Log("UpdateDataSuccess");
    }


    private void UpdateDataFail(PlayFabError error)
    {
        Debug.Log("UpdateDataFail: " + error);
    }
}
