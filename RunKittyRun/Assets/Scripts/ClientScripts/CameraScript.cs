using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    bool gotPlayer;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("LocalGamePlayer").transform;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 20, 0);
    }
}
