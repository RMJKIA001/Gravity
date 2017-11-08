using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    public GameObject target;
    NavMeshAgent agent;
	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Find();
        agent.SetDestination(target.transform.position); //sets player as the agents target
	}

    void Find()
    {
        //finds the closest player
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closestPlayer = null;
        float distance = Mathf.Infinity;

        foreach (GameObject p in players)
        {
            //calculate distance Between  player and AI
            Vector3 difference = transform.position - p.transform.position;
            float distanceBetween = difference.sqrMagnitude;
            if (distanceBetween < distance)
            {
                closestPlayer = p;
                distance = distanceBetween;
            }
        }
        target = closestPlayer;

    }
}
