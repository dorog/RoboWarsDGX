using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab.ClientModels;

public class UIManager : MonoBehaviour
{
    private GetPlayerCombinedInfoResultPayload info;

    private static readonly string goldCode = "GD";
    public static readonly string exp = "Exp";

    private static UIManager instance;

    public static UIManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            info = FindObjectOfType<AccountInfo>().Info;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private Text level;
    [SerializeField]
    private Text coins;

    private void Update()
    {
        if(info == null)
        {
            return;
        }


    }

    private void UpdateText()
    {
        if (info != null)
        {
            int amount = 0;
            if(info.UserVirtualCurrency != null)
            {
                if (info.UserVirtualCurrency.TryGetValue(goldCode, out amount))
                {
                    coins.text = amount.ToString();
                }
            }

            UserDataRecord record = new UserDataRecord();


            if(info.UserData != null)
            {
                if (info.UserData.TryGetValue(goldCode, out record))
                {
                    level.text = record.Value;
                }
            }
        }
    }
}
