using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AccountInfo : MonoBehaviour
{
    private int notReadyStores = 1;
    private GameObject runeForDelete = null;

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

    public List<Rune> ownRunes = new List<Rune>();

    private List<Character> ownCharacters = new List<Character>();

    private PlayerProfile playerProfile = new PlayerProfile();

    private Catalog catalog = new Catalog();
    private Store store = new Store();

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
        //Loading screen, like loading
        GetAccountInfo(result.PlayFabId);
    }

    private static void OnLogInError(PlayFabError error)
    {
        //TODO: Impl it: bad username/password etc
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

        Instance.catalog.CatalogInitReadyEvent += Instance.CatalogInitReady;
        Instance.catalog.CatalogInit();

        Instance.playerProfile.InitProfile(Instance.info);
    }

    private static void GetAccountInfoFail(PlayFabError error)
    {
        Debug.Log("AccountInfor error: " + error);
    }

    private void CatalogInitReady()
    {
        InitOwnLists();
        Instance.store.InitReadyEvent += Instance.CreateStoreLists;
        Instance.store.ListsReadyEvent += Instance.StoreReady;
        Instance.store.InitStore();
    }

    private void CreateStoreLists()
    {
        Instance.store.CreateLists(Instance.catalog.notOwnedRunes, Instance.catalog.notOwnedCharacters, Instance.catalog.notOwnedWeapons);
    }

    private void StoreReady()
    {
        notReadyStores--;
        if(notReadyStores == 0)
        {
            //Make it async honey :D
            SceneManager.LoadScene(SharedData.menuScene);
        }
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

    private static void InitOwnLists()
    {
        if(Instance.info.UserInventory == null)
        {
            Debug.Log("UserInventory is null!");
            return;
        }
        for(int i = 0; i < Instance.info.UserInventory.Count; i++)
        {
            if(Instance.info.UserInventory[i].ItemClass == SharedData.runeClass)
            {
                Rune rune = Instance.catalog.GetRuneById(Instance.info.UserInventory[i].ItemId);
                Instance.ownRunes.Add(rune);
                Instance.catalog.RegistRuneForOwn(rune.id);
            }
            else if (Instance.info.UserInventory[i].ItemClass == SharedData.characterClass)
            {
                Instance.ownCharacters.Add(Instance.catalog.GetCharacterById(Instance.info.UserInventory[i].ItemId));
                Instance.catalog.RegistCharacterForOwn(Instance.info.UserInventory[i].ItemId);
            }
            else if (Instance.info.UserInventory[i].ItemClass == SharedData.weaponClass)
            {
                Debug.Log("Not implemented!");
            }
        }

        Debug.Log(Instance.catalog.notOwnedRunes.Count);
    }

    public List<Rune> GetStoreRunes()
    {
        return Instance.store.runes;
     }

    public void BuyRune(string id, int price, GameObject rune)
    {
        runeForDelete = rune;

        //u can buy the same item more times...
        PurchaseItemRequest request = new PurchaseItemRequest()
        {
            ItemId = id,
            VirtualCurrency = SharedData.runeVirtualCurrency,
            Price = price,
            StoreId = SharedData.runeStoreName
        };
        PlayFabClientAPI.PurchaseItem(request, BuyRuneSuccess, BuyRuneFail);
    }

    private void BuyRuneSuccess(PurchaseItemResult result)
    {
        for (int i = 0; i < result.Items.Count; i++)
        {
            ReplaceRune(result.Items[i].ItemId);
        }
        Destroy(runeForDelete);
    }

    private void ReplaceRune(string id)
    {
        ownRunes.Add(Instance.catalog.GetRuneById(id));
        Instance.store.RemoveRune(id);
    }

    private void BuyRuneFail(PlayFabError error)
    {
        Debug.Log(error);
        switch (error.Error)
        {
            case PlayFabErrorCode.InsufficientFunds:
                Debug.Log("Cheater! :D");
                break;
            default:
                break;
        }
    }
}
