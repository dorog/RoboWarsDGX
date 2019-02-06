using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AccountInfo : MonoBehaviour
{
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
        get { return info; }
        set { info = value; }
    }

    private PlayerProfile playerProfile = new PlayerProfile();
    private PlayerCharacters playerCharacters = new PlayerCharacters();
    private PlayerRunes playerRunes = new PlayerRunes();

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
        SceneManager.LoadScene(SharedData.menuScene);
    }

    private static void OnLogInError(PlayFabError error)
    {
        //TODO: Impl it
        Debug.Log("Error during log in: " + error);
    }

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
            GetUserReadOnlyData = true,
            GetCharacterList = true
        };

        GetPlayerCombinedInfoRequest request = new GetPlayerCombinedInfoRequest()
        {
            PlayFabId = playfabId,
            InfoRequestParameters = paramInfo
        };

        PlayFabClientAPI.GetPlayerCombinedInfo(request, GetAccountInfoSuccess, GetAccountInfoFail);
    }

    private static void GetAccountInfoSuccess(GetPlayerCombinedInfoResult result)
    {
        Instance.info = result.InfoResultPayload;

        Instance.playerProfile.InitProfile(Instance.info);

        Instance.playerCharacters.InitCharacters(Instance.info);

        Instance.playerRunes.InitRunes(Instance.info);
    }

    private static void GetAccountInfoFail(PlayFabError error)
    {
        Debug.Log("AccountInfor error: " + error);
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

        PlayFabClientAPI.RegisterPlayFabUser(request, SignUpSuccess, SignUpError);
    }

    private static void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        Instance.SetUpAccount();
    }

    private static void SignUpError(PlayFabError error)
    {
        //TODO: Impl it
        Debug.Log("Error (sign up):" + error);
    }

    #region Account set up
    private void SetUpAccount()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();

        ProfileStats profileStats = new ProfileStats();
        data.Add(ProfileStats.killsName, profileStats.Kills.ToString());
        data.Add(ProfileStats.headShotsName, profileStats.HeadShots.ToString());
        data.Add(ProfileStats.deathsName, profileStats.Deaths.ToString());

        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = data
        };

        PlayFabClientAPI.UpdateUserData(request, SetUpAccountSuccess, SetUpAccountFail);
    }

    private void SetUpAccountSuccess(UpdateUserDataResult result)
    {
        Debug.Log("UpdateDataSuccess");
    }


    private void SetUpAccountFail(PlayFabError error)
    {
        Debug.Log("UpdateDataFail: " + error);
    }
    #endregion

    #endregion

    public bool IsOwnedCharacter(CharacterType type)
    {
        return playerCharacters.IsOwned(type);
    }

    public bool IsOwnedRune(RuneType type)
    {
        return playerRunes.IsOwned(type);
    }
}
