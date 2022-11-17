using UnityEngine;
using Mirror;
using System;
using TMPro;
using UnityEngine.UI;


public class ChatBehaviour : NetworkBehaviour
{
    [SerializeField]
    private GameObject chatUI;

    [SerializeField]
    private TMP_Text chatText;

    [SerializeField]
    TMP_InputField inputField;

    [SerializeField]
    Button sendButton;

    private static event Action<string> OnMessage;

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
        chatUI.SetActive(true);

        OnMessage += HandleNewMessage;
    }

    [ClientCallback]

    private void OnDestroy()
    {
        if (!hasAuthority)
        {
            return;
        }

        OnMessage -= HandleNewMessage;
    }

    private void HandleNewMessage(string message)
    {
        chatText.text += message;
    }

    public void CheckIfServerOrClient()
    {
        if (isServer)
        {
            SendServer("huh");
        }
        else if (isClient)
        {
            Send("hu");
        }
    }

    [Client]
    public void Send(string message)
    {
        CmdSendMessage(inputField.text, Game.myName);
        Debug.Log("Hello message");
        inputField.text = string.Empty;
    }

    [Server]
    public void SendServer(string message)
    {
        //string myMessage = ($"[{connectionToClient.connectionId}]: {inputField.text}");
        CmdSendMessage(inputField.text, Game.myName);
        //chatText.text += ($"\n{myMessage}");
        inputField.text = string.Empty;
    }

    [Command]
    private void CmdSendMessage(string message, string name)
    {
        RpcHandleMessage($"[{name}]: {message}");
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }
}
