using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Linq;

public class GamePlayerScript : NetworkBehaviour
{
    [SyncVar] public string PlayerName;
    [SyncVar] public int ConnectionId;

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
    public override void OnStartAuthority()
    {
        gameObject.name = "LocalGamePlayer";
        Debug.Log("Labeling the local player: " + this.PlayerName);
    }
    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);
        Game.GamePlayers.Add(this);
        Debug.Log("Added to GamePlayer list: " + this.PlayerName);
    }
    public override void OnStopClient()
    {
        Debug.Log(PlayerName + " is quiting the game.");
        Game.GamePlayers.Remove(this);
        Debug.Log("Removed player from the GamePlayer list: " + this.PlayerName);
    }
    [Server]
    public void SetPlayerName(string playerName)
    {
        this.PlayerName = playerName;
    }
    [Server]
    public void SetConnectionId(int connId)
    {
        this.ConnectionId = connId;
    }
}
