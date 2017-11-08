using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //variables
    public float fireRate = 20;
    public Camera myCamera;
    public AudioSource soundeffect;
    public GameObject HUD;
    public int bullets;
    public GameObject lightsource;
    public int damage;
    public int maxBul;
    public string type;

    //variables
    private double lightofftime;
    private float lightoff;
    private float nextFire;
    public Animator ani;
    public Transform hand;
    private LineRenderer linedraw;
    public Transform barrel;
    public PhotonView photonView;

    private void Start()
    {
        //initialise light source and linedraw for particle effects
        lightsource.SetActive(false);
        linedraw = GetComponent<LineRenderer>();
        
    }

    //set linedraw active/not active
    public void getLine(bool x)
    {
        linedraw.enabled = x;
    }

	void Update ()
    {
        //transform.up = hand.up;
        //transform.right = hand.right;
        //if two player
        if (PhotonNetwork.connected)
        {
            if (photonView.isMine)
            {
                transform.position = hand.position;

                float time = Time.time;

                //if light needs to be turned off (past its time)
                if (time > lightofftime)
                {
                   // lightsource.SetActive(false);
                    //linedraw.enabled = false;
                    photonView.RPC("shoot", PhotonTargets.All, false,false);
                }

                //if player shoots
                if ((Input.GetMouseButtonDown(0)) && (time > nextFire) && (bullets > 0))
                {
                    //send to other players
                    photonView.RPC("Trigger", PhotonTargets.All, "Shoot"); 
                                        

                    //Debug.Log(bullets);
                    HUD.GetComponent<HUD>().decrease(1);
                    bullets = bullets - 1;

                    
                    //lightofftime = Time.time + lightofftime;

                    //get the crosshair position
                    Vector3 crosshair = myCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); //middle of the screen
                    RaycastHit hit;
                    //lightsource.SetActive(true);
                    //linedraw.enabled = true;
                    photonView.RPC("shoot", PhotonTargets.All, true, true);
                    photonView.RPC("shootSound", PhotonTargets.All);

                    //set the particle effect position
                    linedraw.SetPosition(0, barrel.position);


                    //perform raycast
                    if (Physics.Raycast(crosshair, myCamera.transform.forward, out hit, 500))
                    {
                       // Debug.Log("Hit");
                        nextFire = Time.time + fireRate;

                        //draw effect
                        linedraw.SetPosition(1, hit.point);

                        //check if hit shootable object
                        if (hit.collider.GetComponent<Shootable>() != null)
                        {
                            //hit that object for certain damage.
                            Shootable myObject = hit.collider.GetComponent<Shootable>();
                            myObject.Hit(damage);
                            // Debug.DrawLine(transform.position, new Vector3(0.5f, 0.5f, 0), Color.red, 10f, true);
                        }
                    }

                    //update timers
                    nextFire = Time.time + fireRate;
                    lightofftime = Time.time + 0.1;

                }

            }
        }
        else
        {
            //put gun in hand
            transform.position = hand.position;

            float time = Time.time;


            //check time
            if (time > lightofftime)
            {
                lightsource.SetActive(false);
                linedraw.enabled = false;
            }

            //if player shoots (left click)
            if ((Input.GetMouseButtonDown(0)) && (time > nextFire) && (bullets > 0))
            {
                //if two player
                //send to other players
                if (PhotonNetwork.connected) { photonView.RPC("Trigger", PhotonTargets.All, "Shoot"); }
                else { ani.SetTrigger("Shoot"); }

                //Debug.Log(bullets);
                HUD.GetComponent<HUD>().decrease(1);
                bullets = bullets - 1;

                lightsource.SetActive(true);
                //lightofftime = Time.time + lightofftime;

                //play shoot effect
                soundeffect.Play();

                //get cross hair
                Vector3 crosshair = myCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); //middle of the screen
                RaycastHit hit;

                //set barrel position of gun
                linedraw.enabled = true;
                linedraw.SetPosition(0, barrel.position);

                //perform raycast
                if (Physics.Raycast(crosshair, myCamera.transform.forward, out hit, 500))
                {
                   // Debug.Log("Hit");
                   //update timer
                    nextFire = Time.time + fireRate;

                    //set position for particle effect
                    linedraw.SetPosition(1, hit.point);

                    //apply damage to object if it is shootable
                    if (hit.collider.GetComponent<Shootable>() != null)
                    {
                        Shootable myObject = hit.collider.GetComponent<Shootable>();
                        myObject.Hit(damage);
                        // Debug.DrawLine(transform.position, new Vector3(0.5f, 0.5f, 0), Color.red, 10f, true);
                    }
                }

                //update timer
                nextFire = Time.time + fireRate;
                lightofftime = Time.time + 0.1;

            }
        }
    }
    [PunRPC]
    void Trigger(string x)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject y in players)
        {
            //Debug.Log("Trigger");
            if (y.GetPhotonView() == this.photonView)
            {
                y.GetComponent<Animator>().SetTrigger(x);
                

            }
            //
        }
        

    }
    
}
