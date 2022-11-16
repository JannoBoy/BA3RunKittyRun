using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.SceneManagement;

public class GoalScript : NetworkBehaviour
{
    [SerializeField]
    private TMP_Text wtxt, wUItxt;

    private int nrInGoal;

    [SerializeField]
    private GameObject doneUi;

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("PlayerReached");

            //ReachedGoalServerCall(other.gameObject.GetComponent<GamePlayerScript>().PlayerName);
            ReachedGoalClientCall(other.gameObject.GetComponent<GamePlayerScript>().PlayerName);
        }
        Debug.Log("PlayerReached");
    }

    private void CheckForDone()
    {
        if (nrInGoal == Game.GamePlayers.Count)
        {
            string lastName = "last";
            foreach(GamePlayerScript player in Game.GamePlayers)
            {
                if (player.reachedGoal)
                {
                    lastName = player.PlayerName;
                }


            }
            wtxt.text += ("#" + nrInGoal + 1 + "  " + lastName + "\n");
            wUItxt.text = wtxt.text;
            doneUi.SetActive(true);
        }

    }

    public void BackToTitle()
    {
        if (!isServer) 
        {
           Game.StopClient();
        }
        else
        {
            HostDcBackToTitle();
            Game.StopServer();
        }
        SceneManager.LoadScene("LobbyScene");
    }

    [Server]
    public void ReachedGoalServerCall(string name)
    {
        wtxt.text += ("#" + (nrInGoal + 1) + "  " + name + "\n");
        nrInGoal++;
    }
    [Client]
    public void ReachedGoalClientCall(string name)
    {
        wtxt.text += ("#" + (nrInGoal + 1) + "  " + name + "\n");
        nrInGoal++;
        CheckForDone();
    }

    [Client]
    public void HostDcBackToTitle()
    {
        if (!isServer)
        {
            Game.StopClient();
            SceneManager.LoadScene("LobbyScene");
        }
    }
}
