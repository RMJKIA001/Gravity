using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float fireRate = 20;
    public Camera myCamera;
    public AudioSource soundeffect;
    public GameObject HUD;
    public int bullets;
    public GameObject lightsource;
    public int damage;
    public int maxBul;
    public string type;

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
        lightsource.SetActive(false);
        linedraw = GetComponent<LineRenderer>();
        
    }
    public void getLine(bool x)
    {
        linedraw.enabled = x;
    }
	void Update ()
    {
        //transform.up = hand.up;
        //transform.right = hand.right;
        if (PhotonNetwork.connected) {
            if (photonView.isMine)
            {
                transform.position = hand.position;

                float time = Time.time;

                if (time > lightofftime)
                {
                   // lightsource.SetActive(false);
                    //linedraw.enabled = false;
                    photonView.RPC("shoot", PhotonTargets.All, false,false);
                }

                if ((Input.GetMouseButtonDown(0)) && (time > nextFire) && (bullets > 0))
                {
                    photonView.RPC("Trigger", PhotonTargets.All, "Shoot"); 
                    

                    //Debug.Log(bullets);
                    HUD.GetComponent<HUD>().decrease(1);
                    bullets = bullets - 1;

                    
                    //lightofftime = Time.time + lightofftime;

                    soundeffect.Play();
                    Vector3 crosshair = myCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); //middle of the screen
                    RaycastHit hit;
                    //lightsource.SetActive(true);
                    //linedraw.enabled = true;
                    photonView.RPC("shoot", PhotonTargets.All, true, true);


                    linedraw.SetPosition(0, barrel.position);

                    if (Physics.Raycast(crosshair, myCamera.transform.forward, out hit, 500))
                    {
                       // Debug.Log("Hit");
                        nextFire = Time.time + fireRate;

                        linedraw.SetPosition(1, hit.point);
                        if (hit.collider.GetComponent<Shootable>() != null)
                        {
                            Shootable myObject = hit.collider.GetComponent<Shootable>();
                            myObject.Hit(damage);
                            // Debug.DrawLine(transform.position, new Vector3(0.5f, 0.5f, 0), Color.red, 10f, true);
                        }
                    }

                    nextFire = Time.time + fireRate;
                    lightofftime = Time.time + 0.1;

                }

            }
        }
        else
        {
            transform.position = hand.position;

            float time = Time.time;

            if (time > lightofftime)
            {
                lightsource.SetActive(false);
                linedraw.enabled = false;
            }

            if ((Input.GetMouseButtonDown(0)) && (time > nextFire) && (bullets > 0))
            {
                if (PhotonNetwork.connected) { photonView.RPC("Trigger", PhotonTargets.All, "Shoot"); }
                else { ani.SetTrigger("Shoot"); }

                //Debug.Log(bullets);
                HUD.GetComponent<HUD>().decrease(1);
                bullets = bullets - 1;

                lightsource.SetActive(true);
                //lightofftime = Time.time + lightofftime;

                soundeffect.Play();
                Vector3 crosshair = myCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); //middle of the screen
                RaycastHit hit;

                linedraw.enabled = true;
                linedraw.SetPosition(0, barrel.position);

                if (Physics.Raycast(crosshair, myCamera.transform.forward, out hit, 500))
                {
                   // Debug.Log("Hit");
                    nextFire = Time.time + fireRate;

                    linedraw.SetPosition(1, hit.point);

                    if (hit.collider.GetComponent<Shootable>() != null)
                    {
                        Shootable myObject = hit.collider.GetComponent<Shootable>();
                        myObject.Hit(damage);
                        // Debug.DrawLine(transform.position, new Vector3(0.5f, 0.5f, 0), Color.red, 10f, true);
                    }
                }

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
        }
        

    }
    
}
