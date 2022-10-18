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

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        myIdentity = GetComponent<NetworkIdentity>();
    }

    // Update is called once per frame
    void Update()
    {
        //placeholder
        if (Input.GetMouseButton(0) && myIdentity.hasAuthority)
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
}
