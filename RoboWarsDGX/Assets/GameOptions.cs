using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOptions : MonoBehaviour
{
    public GameObject background;

    void Start()
    {
        background.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!background.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                background.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                background.SetActive(false);
            }
        }
    }

    public void Disconnect()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
            yield return null;
        SceneManager.LoadSceneAsync(SharedData.menuScene);
    }
}
