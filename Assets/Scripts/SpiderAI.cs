using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAI : MonoBehaviour, IAI {

    public float speed = 0.001f;
    public GameObject player;
    public GameObject AI;
    private float distance;
    private float time = 1f;
    private float attacktime = 2f;
    float checkTime;
    float checkTime2;
    public AudioSource bite;


    public void Attack()
    {
        //attack cooldown
        if (Time.time > checkTime2)
        {
            player.GetComponent<PlayerHealth>().Decrease(5);
            bite.Play();
            checkTime2 = Time.time + attacktime;

        }
       
    }

    public void Roam()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 AIPos = AI.transform.position;
        Vector3 distance = AIPos - playerPos;
        Debug.Log(distance);

        //look at player
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 4);

        if ((distance.x < 3 && distance.x > -3) && (distance.z < 3 && distance.z > -3))
        {
            Attack();
        }
        else if ((distance.x < 8 && distance.x > -8) && (distance.z < 8 && distance.z > -8))
        {
            Step();
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
        Roam();
    }

    void Hunt()
    {

    }
}
