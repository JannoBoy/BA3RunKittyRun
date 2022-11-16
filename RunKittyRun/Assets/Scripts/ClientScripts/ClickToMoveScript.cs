using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class ClickToMoveScript : NetworkBehaviour
{
    NavMeshAgent navMeshAgent;
    bool cMoving;
    NetworkIdentity myIdentity;
    Camera myCam;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        myIdentity = GetComponent<NetworkIdentity>();
        myCam = GetComponentInChildren<Camera>();
        if (!myIdentity.hasAuthority)
        {
           // myCam.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //placeholder
        if (Input.GetMouseButton(0) && myIdentity.hasAuthority)
        {
            SelectNewPosition();
        }
        if (Input.GetKeyDown(KeyCode.S) && myIdentity.hasAuthority)
        {
            SelectNewPosition();
        }
    }


    void SelectNewPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, 100f))
        {
            navMeshAgent.destination = hit.point;
            cMoving = true;
        }
        else
        {
            cMoving = false;
        }
    }

    void StopMoving()
    {
        navMeshAgent.Stop();
        cMoving = false;
    }

    public void CheckForNavMesh()
    {
        if (navMeshAgent.enabled && !navMeshAgent.isOnNavMesh)
        {
            var position = transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(position, out hit, 10.0f, NavMesh.AllAreas);
            position = hit.position; // usually this barely changes, if at all
            navMeshAgent.Warp(position);
        }
    }
}
