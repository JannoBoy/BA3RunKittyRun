using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    public static LobbyUIManager instance;

    [SerializeField] private NetworkManagerRKR networkManager;

    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject PlayerNamePanel;
    [SerializeField] private GameObject HostOrJoinPanel;
    [SerializeField] private GameObject EnterIPAddressPanel;

    [Header("PlayerName UI")]
    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] private TMP_Text shownPlayerName;

    [Header("Enter IP UI")]
    [SerializeField] private TMP_InputField IpAddressField;

    [Header("Misc. UI")]
    [SerializeField] private Button returnToMainMenu;


    private const string PlayerPrefsNameKey = "Player Name";

    public void ReturnToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        PlayerNamePanel.SetActive(false);
        HostOrJoinPanel.SetActive(false);
        EnterIPAddressPanel.SetActive(false);
        returnToMainMenu.gameObject.SetActive(false);
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        //SceneManager.LoadScene("Gameplay");
        mainMenuPanel.SetActive(false);
        PlayerNamePanel.SetActive(true);
        GetSavedPlayerName();
        returnToMainMenu.gameObject.SetActive(true);
    }
    private void GetSavedPlayerName()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsNameKey))
        {
            playerNameInputField.text = PlayerPrefs.GetString(PlayerPrefsNameKey);
        }
    }
    public void SavePlayerName()
    {
        string playerName = null;
        if (!string.IsNullOrEmpty(playerNameInputField.text))
        {
            playerName = playerNameInputField.text;
            PlayerPrefs.SetString(PlayerPrefsNameKey, playerName);
            networkManager.myName = playerNameInputField.text;
            shownPlayerName.text = "Choosen Name: " + playerNameInputField.text;
        }
    }

    public void GoToConnectionMenu()
    {
        PlayerNamePanel.SetActive(false);
        HostOrJoinPanel.SetActive(true);
    }

    public void HostGame()
    {
        Debug.Log("Hosting a game...");
        networkManager.StartHost();
        HostOrJoinPanel.SetActive(false);
        returnToMainMenu.gameObject.SetActive(false);
    }
    public void JoinGame()
    {
        HostOrJoinPanel.SetActive(false);
        EnterIPAddressPanel.SetActive(true);
    }

    public void ConnectToGame()
    {
        if (!string.IsNullOrEmpty(IpAddressField.text))
        {
            Debug.Log("Client will connect to: " + IpAddressField.text);
            networkManager.networkAddress = IpAddressField.text;
            networkManager.StartClient();
        }
        EnterIPAddressPanel.SetActive(false);
        returnToMainMenu.gameObject.SetActive(false);
    }
}
