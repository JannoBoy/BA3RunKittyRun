using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyPlayerScript : NetworkBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject PlayerLobyUI;
    [SerializeField] private GameObject Player1ReadyPanel;
    [SerializeField] private GameObject Player2ReadyPanel;
    [SerializeField] private GameObject Player3ReadyPanel;
    [SerializeField] private GameObject Player4ReadyPanel;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private Button readyButton;

    [SyncVar] public int ConnectionId;

    [Header("Game Info")]
    public bool IsGameLeader = false;
    [SyncVar(hook = nameof(HandlePlayerNameUpdate))] public string PlayerName;
    [SyncVar(hook = nameof(HandlePlayerReadyStatusUpdate))] public bool IsReady = false;

    private NetworkManagerRKR game;
    private NetworkManagerRKR Game
    {
        get
        {
            if (game != null)
            {
                return game;
            }
            return game = NetworkManagerRKR.singleton as NetworkManagerRKR;
        }
    }
    public void UpdateLobbyUI()
    {
        Debug.Log("Updating UI for: " + this.PlayerName);
        GameObject localPlayer = GameObject.Find("LocalLobbyPlayer");
        if (localPlayer != null)
        {
            localPlayer.GetComponent<LobbyPlayerScript>().ActivateLobbyUI();
        }
    }

    public void HandlePlayerNameUpdate(string oldValue, string newValue)
    {
        Debug.Log("Player name has been updated for: " + oldValue + " to new value: " + newValue);
        UpdateLobbyUI();
    }
    public void HandlePlayerReadyStatusUpdate(bool oldValue, bool newValue)
    {
        Debug.Log("Player ready status has been has been updated for " + this.PlayerName + ": " + oldValue + " to new value: " + newValue);
        UpdateLobbyUI();
    }

    public void ActivateLobbyUI()
    {
        Debug.Log("Activating lobby UI");
        if (!PlayerLobyUI.activeInHierarchy)
            PlayerLobyUI.SetActive(true);
        if (Game.LobbyPlayers.Count() > 0)
        {
            Player1ReadyPanel.SetActive(true);
            Debug.Log("Player1 Ready Panel activated");
            Player2ReadyPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Player1 Ready Panel not activated. Player count: " + Game.LobbyPlayers.Count().ToString());
        }
        if (Game.LobbyPlayers.Count() > 1)
        {
            Player2ReadyPanel.SetActive(true);
            Debug.Log("Player2 Ready Panel activated");
        }
        else
        {
            Debug.Log("Player2 Ready Panel not activated. Player count: " + Game.LobbyPlayers.Count().ToString());
        }
        if (Game.LobbyPlayers.Count() > 2)
        {
            Player3ReadyPanel.SetActive(true);
            Debug.Log("Player3 Ready Panel activated");
        }
        else
        {
            Debug.Log("Player3 Ready Panel not activated. Player count: " + Game.LobbyPlayers.Count().ToString());
        }
        if (Game.LobbyPlayers.Count() > 3)
        {
            Player4ReadyPanel.SetActive(true);
            Debug.Log("Player4 Ready Panel activated");
        }
        else
        {
            Debug.Log("Player4 Ready Panel not activated. Player count: " + Game.LobbyPlayers.Count().ToString());
        }
        UpdatePlayerReadyText();
    }


    public void UpdatePlayerReadyText()
    {
        if (Player1ReadyPanel.activeInHierarchy && Game.LobbyPlayers.Count() > 0)
        {
            foreach (Transform childText in Player1ReadyPanel.transform)
            {
                if (childText.name == "Player1Name")
                    childText.GetComponent<TMP_Text>().text = Game.LobbyPlayers[0].PlayerName;
                //childText.GetComponent<Text>().text = Game.LobbyPlayers[0].PlayerName;
                if (childText.name == "Player1ReadyText")
                {
                    bool isPlayerReady = Game.LobbyPlayers[0].IsReady;
                    Game.myLobbyNumber = 0;
                    if (isPlayerReady)
                    {
                        childText.GetComponent<TMP_Text>().text = "Ready";
                        childText.GetComponent<TMP_Text>().color = Color.green;
                    }
                    else
                    {
                        childText.GetComponent<TMP_Text>().text = "Not Ready";
                        childText.GetComponent<TMP_Text>().color = Color.red;
                    }
                }
            }
        }
        if (Player2ReadyPanel.activeInHierarchy && Game.LobbyPlayers.Count() > 1)
        {
            foreach (Transform childText in Player2ReadyPanel.transform)
            {
                if (childText.name == "Player2Name")
                    childText.GetComponent<TMP_Text>().text = Game.LobbyPlayers[1].PlayerName;
                if (childText.name == "Player2ReadyText")
                {
                    bool isPlayerReady = Game.LobbyPlayers[1].IsReady;
                    Game.myLobbyNumber = 1;
                    if (isPlayerReady)
                    {
                        childText.GetComponent<TMP_Text>().text = "Ready";
                        childText.GetComponent<TMP_Text>().color = Color.green;
                    }
                    else
                    {
                        childText.GetComponent<TMP_Text>().text = "Not Ready";
                        childText.GetComponent<TMP_Text>().color = Color.red;
                    }
                }
                Debug.Log("Updated Player2 Ready panel with player name: " + Game.LobbyPlayers[1].PlayerName + " and ready status: " + Game.LobbyPlayers[1].IsReady);
            }
        }
        if (Player3ReadyPanel.activeInHierarchy && Game.LobbyPlayers.Count() > 2)
        {
            foreach (Transform childText in Player3ReadyPanel.transform)
            {
                if (childText.name == "Player3Name")
                    childText.GetComponent<TMP_Text>().text = Game.LobbyPlayers[2].PlayerName;
                if (childText.name == "Player3ReadyText")
                {
                    Game.myLobbyNumber = 2;
                    bool isPlayerReady = Game.LobbyPlayers[2].IsReady;
                    if (isPlayerReady)
                    {
                        childText.GetComponent<TMP_Text>().text = "Ready";
                        childText.GetComponent<TMP_Text>().color = Color.green;
                    }
                    else
                    {
                        childText.GetComponent<TMP_Text>().text = "Not Ready";
                        childText.GetComponent<TMP_Text>().color = Color.red;
                    }
                }
                Debug.Log("Updated Player3 Ready panel with player name: " + Game.LobbyPlayers[2].PlayerName + " and ready status: " + Game.LobbyPlayers[2].IsReady);
            }
        }
        if (Player4ReadyPanel.activeInHierarchy && Game.LobbyPlayers.Count() > 3)
        {
            foreach (Transform childText in Player4ReadyPanel.transform)
            {
                if (childText.name == "Player4Name")
                    childText.GetComponent<TMP_Text>().text = Game.LobbyPlayers[3].PlayerName;
                if (childText.name == "Player4ReadyText")
                {
                    Game.myLobbyNumber = 3;
                    bool isPlayerReady = Game.LobbyPlayers[3].IsReady;
                    if (isPlayerReady)
                    {
                        childText.GetComponent<TMP_Text>().text = "Ready";
                        childText.GetComponent<TMP_Text>().color = Color.green;
                    }
                    else
                    {
                        childText.GetComponent<TMP_Text>().text = "Not Ready";
                        childText.GetComponent<TMP_Text>().color = Color.red;
                    }
                }
                Debug.Log("Updated Player4 Ready panel with player name: " + Game.LobbyPlayers[3].PlayerName + " and ready status: " + Game.LobbyPlayers[3].IsReady);
            }
        }
        if (IsReady)
        {
            readyButton.GetComponentInChildren<TMP_Text>().text = "Unready";
        }
        else
        {
            readyButton.GetComponentInChildren<TMP_Text>().text = "Ready Up";
        }

        
    }


    public void Start()
    {
        ResetReadyButton();
    }
    public void ResetReadyButton()
    {

            if (gameObject.name != "LocalLobbyPlayer")
            {
            readyButton.gameObject.SetActive(false);
            }
        
    }

    [Command]
    public void CmdReadyUp()
    {
        IsReady = !IsReady;
        CheckIfAllPlayersAreReady();
        Debug.Log("Ready status changed for: " + PlayerName);
    }

    public override void OnStartAuthority()
    {
        CmdSetPlayerName(Game.myName);
        if (!PlayerLobyUI.activeInHierarchy)
            PlayerLobyUI.SetActive(true);
        gameObject.name = "LocalLobbyPlayer";
        ResetReadyButton();
    }
    [Command]
    private void CmdSetPlayerName(string playerName)
    {
        PlayerName = playerName;
        Debug.Log("Player display name set to: " + playerName);
    }

    public void CheckIfAllPlayersAreReady()
    {
        Debug.Log("Checking if all players are ready.");
        bool arePlayersReady = false;
        foreach (LobbyPlayerScript player in Game.LobbyPlayers)
        {
            if (!player.IsReady)
            {
                Debug.Log(player.PlayerName + "is not ready.");
                arePlayersReady = false;
                startGameButton.SetActive(false);
                break;
            }
            else
            {
                arePlayersReady = true;
            }

        }
        if (arePlayersReady)
            Debug.Log("All players are ready");

        if (arePlayersReady && IsGameLeader && Game.LobbyPlayers.Count() >= Game.minPlayers)
        {
            Debug.Log("All players are ready and minimum number of players in game. Activating the StartGame button on Game leader's UI.");
            startGameButton.SetActive(true);
        }
        else
        {
            startGameButton.SetActive(false);
        }

    }

    public override void OnStartClient()
    {
        Game.LobbyPlayers.Add(this);
        Debug.Log("Added to GamePlayer list: " + this.PlayerName);
    }
    public override void OnStopClient()
    {
        Debug.Log(PlayerName + " is quiting the game.");
        Game.LobbyPlayers.Remove(this);
        Debug.Log("Removed player from the GamePlayer list: " + this.PlayerName);
    }

    //[Command]
    public void CmdStartGame()
    {
        Game.StartGame();
    }

    public void QuitLobby()
    {
        if (hasAuthority)
        {
            if (IsGameLeader)
            {

                Game.StopHost();
            }
            else
            {

                Game.StopClient();
            }
        }
    }

    private void OnDestroy()
    {
        //if (isOwned)
          //  LobbyUIManager.instance.ReturnToMainMenu();
    }

}
