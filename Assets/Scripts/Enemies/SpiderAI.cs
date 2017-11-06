using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderAI : MonoBehaviour, IAI {

    public float speed = 0.001f;
    public GameObject player;
    public GameObject AI;
    private float distance;
    //private float time = 1f;
    private float attacktime = 2f;
    float checkTime;
    float checkTime2;
    public AudioSource bite;
    public int range;

    private NavMeshAgent nav;
    private Navigation navi;
    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        navi = GetComponent<Navigation>();
    }

    //find closest player
    void Find()
    {
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player"); //get all
        GameObject closestPlayer = null;
        float distance = Mathf.Infinity;

        foreach (GameObject p in players)
        {
            //get distance between each player and AI
            Vector3 difference = transform.position - p.transform.position;
            float distanceBetween = difference.sqrMagnitude;
            if (distanceBetween < distance)
            {
                closestPlayer = p;
                distance = distanceBetween;
            }
        }
        player = closestPlayer; 
    }
    void FixedUpdate()
    {
        Find();
    }

    public void Attack()
    {
        //attack cooldown
        Vector3 playerPos = player.transform.position;
        Vector3 AIPos = AI.transform.position;
        Vector3 distance = AIPos - playerPos;

        if ((distance.x < 3 && distance.x > -3) && (distance.z < 3 && distance.z > -3))
        {
            if (Time.time > checkTime2)
            {
                player.GetComponent<PlayerHealth>().Decrease(5);
               // Debug.Log(bite + " " + bite.isVirtual);
                bite.Play();
                checkTime2 = Time.time + attacktime;

            }
        }
    }

    public void Roam()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 AIPos = AI.transform.position;
        Vector3 distance = AIPos - playerPos;
        //Debug.Log(distance);

        //look at player
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 4);

       
        //check distance
        if ((distance.x < 3 && distance.x > -3) && (distance.z < 3 && distance.z > -3))
        {
            Attack();
        }
        else if ((distance.x < 8 && distance.x > -8) && (distance.z < 8 && distance.z > -8))
        {
            //Step();
        }
        
    }

    void Step()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        transform.position = new Vector3(transform.position.x, 1.7f, transform.position.z);
    }

    //not for prototype
    public void Spawn()
    {
        throw new NotImplementedException();
    }
    //not for prototype
    public void Flee()
    {
        throw new NotImplementedException();
    }


    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= range)
        {
            nav.enabled = true;
            navi.enabled = true;
           // nav.SetDestination(player.transform.position);
        }

        else
        {
            this.nav.enabled = false;
            navi.enabled = false;
        }

        Attack();
    }

    void Hunt()
    {
        throw new NotImplementedException();
    }
}
