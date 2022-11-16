using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    CinemachineVirtualCamera vc;
    bool gotPlayer;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        vc = GetComponent<CinemachineVirtualCamera>();

        player = GameObject.Find("LocalGamePlayer").transform;
        vc.LookAt = GameObject.Find("LocalGamePlayer").transform;
        if(vc.Follow == null)
        {
            gotPlayer = false;
        }
        else
        {
            gotPlayer = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 20, 0);
    }
}
