using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : Photon.MonoBehaviour
{
    public GameObject player;
    public GameObject AI;
    public Animator ani;
    public AudioSource effect;

    float fireRate = 5f;
    float nextFire = 0;

    bool hit = false;
    GameObject netPlay;
    int hitc=0;
    Vector3 impact = Vector3.zero;


    public void Attack()
    {
        if (PhotonNetwork.connected)
        {
            /*
             get all players in the room and check if there is a hit
             */
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject p in players)
            {
                Vector3 playerPos = p.transform.position;
                Vector3 AIPos = AI.transform.position;
                Vector3 distanceBetweenThem = AIPos - playerPos;

                if ((distanceBetweenThem.x < 6 && distanceBetweenThem.x > -6) && (distanceBetweenThem.z < 6 && distanceBetweenThem.z > -6))
                {
                    //Cool down period
                    if (Time.time > nextFire)
                    {
                        effect.Play();
                        photonView.RPC("Trigger", PhotonTargets.All, "Jump"); // notifies network
                        nextFire = Time.time + fireRate;
                        p.GetComponent<PlayerHealth>().Decrease(10); //decreases health
                        impact += -p.transform.forward * 50; //to move the person backwards
                        hitc += 1; // count how many players were hit
                        netPlay = p; // needed in the case of only one

                    }
                }
                
            }

        }
        else
        {

            Vector3 playerPos = player.transform.position;
            Vector3 AIPos = AI.transform.position;
            Vector3 distanceBetweenThem = AIPos - playerPos;

            if ((distanceBetweenThem.x < 6 && distanceBetweenThem.x > -6) && (distanceBetweenThem.z < 6 && distanceBetweenThem.z > -6))
            {
                if (Time.time > nextFire)
                {
                    effect.Play();
                    ani.SetTrigger("Jump");
                    nextFire = Time.time + fireRate;
                    player.GetComponent<PlayerHealth>().Decrease(10);
                    impact += -player.transform.forward * 50;
                    hit = true;

                }
            }
        }
    }
    [PunRPC]
    void Trigger(string x)
    {
        ani.SetTrigger(x);
    }

    void FixedUpdate()
    {
        Attack();

    }
    // Update is called once per frame
    void Update()
    {
        
           if(PhotonNetwork.connected)
            {
                if (hitc==2)
                {
                    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                    foreach (GameObject p in players)
                    {
                        p.GetComponent<CharacterController>().Move(impact * Time.deltaTime);  //moves player backwards
                        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
                    }
                }
            else if(hitc==1)
            {
                netPlay.GetComponent<CharacterController>().Move(impact * Time.deltaTime);
                impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
                
            }
            if (impact.magnitude < 0.2) //in order to stop the player from going too far back
            {
                hitc = 0;
            }


           }
            else
            {
                if (hit)
                {
                    player.GetComponent<CharacterController>().Move(impact * Time.deltaTime);
                    impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
                    if (impact.magnitude < 0.2)
                    {
                        hit = false;
                    }
                }
            
            }
        

    }

}


