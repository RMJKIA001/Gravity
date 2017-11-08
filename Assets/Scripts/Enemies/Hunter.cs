using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Hunter : Photon.MonoBehaviour , IAI
{
    public float speed;
    public float travelled = 0;
    public int distance;
    public GameObject AI;
    private GameObject player;

    bool right = true;
    bool turn = false;
    private int step = 1;

    bool IsAttacking  = false;
    //public GameObject Bullet ;
    float fireRate = 10f;
    float nextFire = 0;
    public float bulletTime = 50f;
    public float disableBullet = 0;
    public AudioSource trappedeffect;
    public ParticleSystem particles;

    public int range;


    public GameObject trappedText;
    public Animator ani;
    private NavMeshAgent nav;
    private Navigation navi;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        navi = GetComponent<Navigation>();
    }

 
    //find the closeset player
    void Find()
    {
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
        player = closestPlayer;
        // Bullet.GetComponent<Bullet>().setPlayer(player);
        if (PhotonNetwork.connected) {
            if(player.GetPhotonView().isMine) // only show it if i am the owner(ie not accross the network)
            {
                trappedText = player.GetComponentInChildren<Canvas>().GetComponent<CanComp>().Trapped;
            }
        }
        else
        {
            trappedText = player.GetComponentInChildren<Canvas>().GetComponent<CanComp>().Trapped;
        }
    }
    void FixedUpdate()
    {
        Find();//called less often
    }
    public void Attack()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 AIPos = AI.transform.position;
        Vector3 distanceBetweenThem = AIPos - playerPos;//new Vector3(2, 2, 2);

        if ((distanceBetweenThem.x < 10 && distanceBetweenThem.x > -10) && (distanceBetweenThem.z < 10 && distanceBetweenThem.z > -10))
        {
            ani.SetBool("Distance", true);
            if (Time.time > nextFire)
            {
                var RayDirection = player.transform.position - transform.position; //Raycast from hunter to player
                RaycastHit hit;

                if (Physics.Raycast(transform.position, RayDirection, out hit, 500))
                {
                    if (hit.transform == player.transform)
                    {

                        nextFire = Time.time + fireRate; 
                        trappedeffect.time = 2f;
                        trappedeffect.Play();
                        particles.Emit(50);

                        if (PhotonNetwork.connected) { GetComponent<PhotonView>().RPC("Trigger", PhotonTargets.All, "Ens"); }
                        else
                        {
                            ani.SetTrigger("Ens");
                        }
                        // Debug.Log("Hit Player");
                        if (PhotonNetwork.connected)
                        {
                            
                            if (player.GetPhotonView().isMine)
                            {
                                //Debug.Log("Is Mine");
                                trappedText.GetComponent<Text>().enabled = true;
                                trappedText.GetComponent<EnsnaredTimer>().timeLeft = 5f;
                                //photonView.RPC("Ensnared", PhotonTargets.All, player.GetPhotonView().viewID);
                                player.GetComponent<PlayerController>().ensared = true;
                                //Debug.Log(player.GetComponent<PlayerController>().ensared);
                                player.GetComponent<PlayerHealth>().Decrease(10);
                            }
                            
                        }
                        else
                        {
                            trappedText.GetComponent<Text>().enabled = true;
                            trappedText.GetComponent<EnsnaredTimer>().timeLeft = 5f;
                            player.GetComponent<PlayerController>().ensared = true;
                            player.GetComponent<PlayerHealth>().Decrease(10);
                        }
                    }
                }

            }
        }
        else
        {
            ani.SetBool("Distance", false);
        }
           
    }
    //moves up and down the room
    /*NOT USED IN THIS BUILD*/
    public void Roam()
    {
        Vector3 otherPosn = AI.transform.position;
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 4);

        if (right)
        {
            AI.transform.position = new Vector3(otherPosn.x, otherPosn.y, otherPosn.z + speed);
            travelled = travelled + speed;
        }
        else
        {
            AI.transform.position = new Vector3(otherPosn.x, otherPosn.y, otherPosn.z - speed);
            travelled = travelled - speed;
        }


        //if (AI.transform.position.z > x1)
        {
            right = false;
            AI.transform.rotation = new Quaternion(0, 180, 0, 0);
            Step();
        }


        //if (AI.transform.position.z < x2) 
        {
            right = true;
            AI.transform.rotation = new Quaternion(0, 0, 0, 0);
            Step();
        }

    }
    /*Not used in this build*/
    //moves left or right 
    void Step()
    {
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
	void Update ()
    {
        //Roam();
        // Hunt();
        var distance = Vector3.Distance(player.transform.position, transform.position);
        //only enable the nav mesh and nav agent if player in range
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
    /*Not in this build*/
    //find the distance beteen the player and the hunter.
    void Hunt()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 AIPos = AI.transform.position;
        Vector3 distanceBetweenThem = AIPos - playerPos;//new Vector3(2, 2, 2);
        if ((distanceBetweenThem.x <5 && distanceBetweenThem.x >-5) && (distanceBetweenThem.z<5 && distanceBetweenThem.z>-5)  )
        {
            IsAttacking = true;
        }
        else
        {
            IsAttacking = false;
        }
    }
   
    [PunRPC]
    void Trigger(string x)
    {
        ani.SetTrigger(x);
    }

}


