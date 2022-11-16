using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
public class NetworkManagerRKR : NetworkManager
{
    [SerializeField] public int minPlayers = 1;
    [SerializeField] private LobbyPlayerScript lobbyPlayerPrefab;
    [SerializeField] private GamePlayerScript gamePlayerPrefab;

    public string myName;

    public int myLobbyNumber;
    public List<LobbyPlayerScript> LobbyPlayers { get; } = new List<LobbyPlayerScript>();
    public List<GamePlayerScript> GamePlayers { get; } = new List<GamePlayerScript>();

    public override void OnStartClient()
    {
        Debug.Log("Starting client...");
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        Debug.Log("Connecting to server...");
        if (numPlayers >= maxConnections) // prevents players joining if the game is full
        {
            Debug.Log("Too many players. Disconnecting user.");
            conn.Disconnect();
            return;
        }
        if (SceneManager.GetActiveScene().name != "LobbyScene") // prevents players from joining a game that has already started. When the game starts, the scene will no longer be the "TitleScreen"
        {
            Debug.Log("Player did not load from correct scene. Disconnecting user. Player loaded from scene: " + SceneManager.GetActiveScene().name);
            conn.Disconnect();
            return;
        }
        Debug.Log("Server Connected");
        myLobbyNumber = LobbyPlayers.Count;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Debug.Log("Checking if player is in correct scene. Player's scene name is: " + SceneManager.GetActiveScene().name.ToString() + ". Correct scene name is: LobbyScene");
        if (SceneManager.GetActiveScene().name == "LobbyScene")
        {
            bool isGameLeader = LobbyPlayers.Count == 0; // isLeader is true if the player count is 0, aka when you are the first player to be added to a server/room
            LobbyPlayerScript lobbyPlayerInstance = Instantiate(lobbyPlayerPrefab);

            lobbyPlayerInstance.IsGameLeader = isGameLeader;
            lobbyPlayerInstance.ConnectionId = conn.connectionId;

            NetworkServer.AddPlayerForConnection(conn, lobbyPlayerInstance.gameObject);
            Debug.Log("Player added. Player name: " + lobbyPlayerInstance.PlayerName + ". Player connection id: " + lobbyPlayerInstance.ConnectionId.ToString());
        }
    }

    private bool CanStartGame()
    {
        if (numPlayers < minPlayers)
        {
            return false;
        }
        foreach (LobbyPlayerScript player in LobbyPlayers)
        {
            if (!player.IsReady)
            {
                return false;
            }
        }
        Debug.Log("Try start game");
        return true;
    }

    public void StartGame()
    {
        if (CanStartGame() && SceneManager.GetActiveScene().name == "LobbyScene")
        {
            ServerChangeScene("SampleScene"); //YOOO PUT GAMEPLAY SCENE HERE
        }
    }

    public override void ServerChangeScene(string newSceneName)
    {
        //Changing from the menu to the scene
        if (SceneManager.GetActiveScene().name == "LobbyScene" && newSceneName == "SampleScene") //YOOO PUT GAMEPLAY SCENE HERE
        {
            for (int i = LobbyPlayers.Count - 1; i >= 0; i--)
            {
                var conn = LobbyPlayers[i].connectionToClient;
                var gamePlayerInstance = Instantiate(gamePlayerPrefab);

                gamePlayerInstance.SetPlayerName(LobbyPlayers[i].PlayerName);
                gamePlayerInstance.SetConnectionId(LobbyPlayers[i].ConnectionId);

                NetworkServer.Destroy(conn.identity.gameObject);
                NetworkServer.ReplacePlayerForConnection(conn, gamePlayerInstance.gameObject, true);
            }
        }
        base.ServerChangeScene(newSceneName);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if (conn.identity != null)
        {
            LobbyPlayerScript player = conn.identity.GetComponent<LobbyPlayerScript>();
            LobbyPlayers.Remove(player);
        }
        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        LobbyPlayers.Clear();
    }


}
