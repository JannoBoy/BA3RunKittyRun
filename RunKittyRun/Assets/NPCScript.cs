using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCScript : MonoBehaviour
{
    public int Levelno;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private Vector3 destination;
    private bool active = false;
    public NavMeshAgent agent;
    public float travelDistanceTime =3;
    void Start()
    {
        travelDistanceTime = Random.Range(1f, 3f);
        StartCoroutine(setGoal());
    }
    void Awake()
    {
         agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
        }
    }

    public void setMinMaxValues(float v1, float v2,float v3,float v4)
    {
        minX = v1;
        maxX = v2;
        minY = v3;
        maxY = v4;
        destination = (new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minY, maxY)));
        agent.destination = destination;
        active = true;
    }

    IEnumerator setGoal()
    {
        
        yield return new WaitForSeconds(travelDistanceTime);
        travelDistanceTime = Random.Range(1f, 3f);
        destination = (new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minY, maxY)));
        agent.destination = destination;
            StartCoroutine(setGoal());
        
    }
}
