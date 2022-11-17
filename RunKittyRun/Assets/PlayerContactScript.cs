using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class PlayerContactScript : NetworkBehaviour
{
    NavMeshAgent navMeshAgent;
    bool cMoving, firstclick = true;
    NetworkIdentity myIdentity;
    Rigidbody rb;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            killPlayer(collision.gameObject.GetComponent<NPCScript>().Levelno);

        }
    }

    private void killPlayer(int level)
    {
        Debug.Log(level);

        switch (level)
        {
            case 1:
                    gameObject.transform.position = new Vector3(-67.5f, 0f, 63.2f);
                break;
            case 2:
                gameObject.transform.position = new Vector3(62.2f, 0f, 63.2f);
                break;
            case 3:
                gameObject.transform.position = new Vector3(-62.2f, 0f, -54.9f);
                break;
            case 4:
                gameObject.transform.position = new Vector3(-58.2f, 0f, -57.52f);
                break;
            case 5:
                gameObject.transform.position = new Vector3(-58.2f, 0f, 34f);
                break;
            case 6:
                gameObject.transform.position = new Vector3(36.3f, 0f, 36f);
                break;
            case 7:
                gameObject.transform.position = new Vector3(36.3f, 0f, -29.5f);
                break;
            case 8:
                gameObject.transform.position = new Vector3(-30.8f, 0f, -29.5f);
                break;
        }
    }
}
