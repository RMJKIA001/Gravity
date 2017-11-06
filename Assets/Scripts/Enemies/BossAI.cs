using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    public GameObject player;
    public GameObject AI;
    public Animator ani;

    float fireRate = 5f;
    float nextFire = 0;

    bool hit = false;
    Vector3 impact = Vector3.zero;


    public void Attack()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 AIPos = AI.transform.position;
        Vector3 distanceBetweenThem = AIPos - playerPos;//new Vector3(2, 2, 2);

        if ((distanceBetweenThem.x < 10 && distanceBetweenThem.x > -10) && (distanceBetweenThem.z < 10 && distanceBetweenThem.z > -10))
        {
             if (Time.time > nextFire)
            {
                ani.SetTrigger("Jump");
                nextFire = Time.time + fireRate;
                player.GetComponent<PlayerHealth>().Decrease(10);
                impact += -player.transform.forward * 50;
                hit = true;
    
            }
        }

    }


    void FixedUpdate()
    {
        Attack();

    }
    // Update is called once per frame
    void Update()
    {
        
        if (hit)
        {
             player.GetComponent<CharacterController>().Move(impact*Time.deltaTime);
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);

            if (impact.magnitude < 0.2)
            {
                hit = false;
            }
        }

    }

}


