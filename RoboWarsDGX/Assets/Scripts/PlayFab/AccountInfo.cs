using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AccountInfo : MonoBehaviour
{
    public SignUpUI signUpUI;
    public LoginUI loginUI;

    private static readonly string loginGettingData = "Getting account's data...";
    private static readonly string loginGettingDataStart = "Getting Database's data (";
    private static readonly string loginGettingDataEnd = ")...";

    private static string GettingDataState { get => (loginGettingDataStart + loginGettingDataStateNumber + "/" + loginGettingDataMaxState + loginGettingDataEnd); }

    private static int loginGettingDataStateNumber = 1;
    private static readonly int loginGettingDataMaxState = 3;

    public delegate void SuccessRuneBuying(string id);
    public event SuccessRuneBuying SuccessRuneBuyingEvent;

    public delegate void SuccessCharacterBuying(string id);
    public event SuccessCharacterBuying SuccessCharacterBuyingEvent;

    public delegate void SuccessWeaponBuying(string id);
    public event SuccessWeaponBuying SuccessWeaponBuyingEvent;

    private int notReadyStores = 1;
    private int cost = 0;

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
    public List<Character> ownCharacters = new List<Character>();
    public List<Weapon> ownWeapons = new List<Weapon>();

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
        Instance.loginUI.LogInInProgress();

        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest()
        {
            Username = username,
            Password = password
        };

        PlayFabClientAPI.LoginWithPlayFab(request, OnLogInSuccess, OnLogInError);
    }
    private static void OnLogInSuccess(LoginResult result)
    {
        Instance.loginUI.LogInGettingData(loginGettingData);
        GetAccountInfo(result.PlayFabId);
    }

    private static void OnLogInError(PlayFabError error)
    {
        Instance.loginUI.LoginFail(error.ErrorMessage);
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
        Instance.loginUI.LogInGettingData(GettingDataState);
        loginGettingDataStateNumber++;

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
        Instance.loginUI.LogInGettingData(GettingDataState);
        loginGettingDataStateNumber++;

        InitOwnLists();
        Instance.store.InitReadyEvent += Instance.CreateStoreLists;
        Instance.store.ListsReadyEvent += Instance.StoreReady;
        Instance.store.InitStore();
    }

    private void CreateStoreLists()
    {
        Instance.loginUI.LogInGettingData(GettingDataState);

        Instance.store.CreateLists(Instance.catalog.notOwnedRunes, Instance.catalog.notOwnedCharacters, Instance.catalog.notOwnedWeapons);
    }

    private void StoreReady()
    {
        notReadyStores--;
        if(notReadyStores == 0)
        {
            SceneManager.LoadSceneAsync(SharedData.menuScene);
        }
    }
    #endregion

    #region Sign Up
    public static void SignUp(string username, string password, string email)
    {
        Instance.signUpUI.SignUpInProgress();

        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            Email = email,
            Username = username,
            Password = password,
            DisplayName = username
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, SignUpSuccess, SignUpError);

    }

    private static void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        Instance.signUpUI.AccountSetUp();
        Instance.SetUpAccount();
    }

    private static void SignUpError(PlayFabError error)
    {
        Instance.signUpUI.SignUpFail(error.ErrorMessage);
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
        Instance.signUpUI.SignUpSuccess();
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
                Instance.ownWeapons.Add(Instance.catalog.GetWeaponById(Instance.info.UserInventory[i].ItemId));
                Instance.catalog.RegistWeaponForOwn(Instance.info.UserInventory[i].ItemId);
            }
        }

    }

    public List<Rune> GetStoreRunes()
    {
        return Instance.store.runes;
     }

    public List<Character> GetStoreCharacters()
    {
        return Instance.store.characters;
    }

    public List<Weapon> GetStoreWeapons()
    {
        return Instance.store.weapons;
    }

    #region Buy items

    public void BuyRune(string id, int price)
    {
        cost = price;

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
        PlayerProfile.experience -= cost;
        for (int i = 0; i < result.Items.Count; i++)
        {
            ReplaceRune(result.Items[i].ItemId);
            SuccessRuneBuyingEvent(result.Items[i].ItemId);
        }
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

    public void BuyCharacter(string id, int price)
    {
        cost = price;

        //u can buy the same item more times...
        PurchaseItemRequest request = new PurchaseItemRequest()
        {
            ItemId = id,
            VirtualCurrency = SharedData.characterVirtualCurrency,
            Price = price,
            StoreId = SharedData.characterStoreName
        };
        PlayFabClientAPI.PurchaseItem(request, BuyCharacterSuccess, BuyCharacterFail);
    }

    private void BuyCharacterSuccess(PurchaseItemResult result)
    {
        PlayerProfile.gold -= cost;
        for (int i = 0; i < result.Items.Count; i++)
        {
            ReplaceCharacter(result.Items[i].ItemId);
            SuccessCharacterBuyingEvent(result.Items[i].ItemId);
        }
    }

    private void ReplaceCharacter(string id)
    {
        ownCharacters.Add(Instance.catalog.GetCharacterById(id));
        Instance.store.RemoveCharacter(id);
    }

    private void BuyCharacterFail(PlayFabError error)
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

    public void BuyWeapon(string id, int price)
    {
        cost = price;

        //u can buy the same item more times...
        PurchaseItemRequest request = new PurchaseItemRequest()
        {
            ItemId = id,
            VirtualCurrency = SharedData.weaponVirtualCurrency,
            Price = price,
            StoreId = SharedData.weaponStoreName
        };
        PlayFabClientAPI.PurchaseItem(request, BuyWeaponSuccess, BuyWeaponFail);
    }

    private void BuyWeaponSuccess(PurchaseItemResult result)
    {
        PlayerProfile.iron -= cost;
        for (int i = 0; i < result.Items.Count; i++)
        {
            ReplaceWeapon(result.Items[i].ItemId);
            SuccessWeaponBuyingEvent(result.Items[i].ItemId);
        }
    }

    private void ReplaceWeapon(string id)
    {
        ownWeapons.Add(Instance.catalog.GetWeaponById(id));
        Instance.store.RemoveWeapon(id);
    }

    private void BuyWeaponFail(PlayFabError error)
    {
        Debug.Log(error);
    }

    #endregion
}
