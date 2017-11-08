using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : Photon.MonoBehaviour
{
    //variables used throughout class
    private CharacterController cont;
    private float currCamPosition = 0;
    private float vert, hori;
    private Animator ani;
    public Camera cam;
    public float mouseSensitiviy = 1;
    public float velocity = 0;
    public float jumpSpeed;
    public float speed = 5;
    public bool ensared = false;
    private bool gun;

    // Use this for initialization
    void Start ()
    {
        //set cursor status
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //get character controller and animator
        cont = GetComponent<CharacterController>();
        ani = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //mouse movement, look around
        float rotX = Input.GetAxis("Mouse X") * mouseSensitiviy;
        float rotY = Input.GetAxis("Mouse Y") * mouseSensitiviy;
        transform.Rotate(0, rotX, 0);

        currCamPosition -= rotY;
        float newRot = Mathf.Clamp(currCamPosition, -30, 33);
        cam.transform.localRotation = Quaternion.Euler(newRot, 0, 0);
        //cam.transform.localRotation = Quaternion.Euler(newRot, transform.rotation.y,transform.);//transform.rotation;
        //keyboard inut , move around
        vert = Input.GetAxis("Vertical");
        hori = Input.GetAxis("Horizontal");
        velocity += Physics.gravity.y * Time.deltaTime;
        ani.SetBool( "Gun",GetComponent<GunControls>().shotGun || GetComponent<GunControls>().lazerGun);
        
        bool isWalking = (vert != 0 || hori != 0);

        //set animation
        ani.SetBool("Walking", isWalking);
       
        //increase speed if sprinting
        if (Input.GetKey(KeyCode.LeftShift) && isWalking)
        {
            vert *= 2f;
        }

        //jump
        if (Input.GetButtonDown("Jump") && cont.isGrounded)
        {
            velocity = jumpSpeed;
            //update for two players
            if (PhotonNetwork.connected) { GetComponent<PhotonView>().RPC("Trigger", PhotonTargets.All,"Jump"); }
            else {
                ani.SetTrigger("Jump"); }
            
        }
       

        //set gravity back to normal
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            //move objects for both players
            if (PhotonNetwork.connected) {
                photonView.RPC("MoveObj", PhotonTargets.All, false);
                photonView.RPC("Jump",PhotonTargets.All,7);
            }
            else
            {
                //normal jump height
                jumpSpeed = 7;

                //adjust platforms for both players
                GameObject[] platforms = GameObject.FindGameObjectsWithTag("GravityPlatform");
                foreach (GameObject platform in platforms)
                {
                    platform.GetComponent<GravityControl>().setGravity(false);
                }
            }
        }
        //2 pressed
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            
            if (PhotonNetwork.connected) {
                photonView.RPC("MoveObj", PhotonTargets.All, false);
                photonView.RPC("Jump", PhotonTargets.All, 15);
            }
            else
            {
                //set both players to jump higher
                jumpSpeed = 15;
                GameObject[] platforms = GameObject.FindGameObjectsWithTag("GravityPlatform");

                //move all platforms back to normal
                foreach (GameObject platform in platforms)
                {
                    platform.GetComponent<GravityControl>().setGravity(false);
                }
            }
        }   
        //if 3 pressed
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //set for both players
            if (PhotonNetwork.connected) {
                photonView.RPC("MoveObj", PhotonTargets.All,true);
                photonView.RPC("Jump", PhotonTargets.All, 7);
            }
            else
            {
                //normal jump height
                jumpSpeed = 7;
                GameObject[] platforms = GameObject.FindGameObjectsWithTag("GravityPlatform");

                //adjust platforms
                foreach (GameObject platform in platforms)
                {
                    platform.GetComponent<GravityControl>().setGravity(true);
                }
            }
        }

        Vector3 move;
        if (!ensared)
        {
            //move speed
            move = new Vector3(hori * speed, velocity, vert * speed);
        }
        else
        {
            //dont allow player to move if ensnared
            move = new Vector3(0, 0, 0);
            if (PhotonNetwork.connected) { photonView.RPC("Trigger", PhotonTargets.All,"Idle"); }
            else{   ani.SetTrigger("Idle");
        }
           
        }
        //animations
        cont.Move(transform.rotation * move * Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.Escape)) { SceneManager.LoadScene("Menu"); Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; }
    }
    //Synchronises the movement of gravity platforms
    [PunRPC]
    void MoveObj(bool grav)
    {
        GameObject[] x = GameObject.FindGameObjectsWithTag("GravityPlatform");
        foreach (GameObject y in x)
        {
            y.GetComponent<GravityControl>().setGravity(grav);
     
        }
    }
    [PunRPC]
    void Jump(int x)
    {
        //synchronize players according to gravity
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject y in players)
        {
            y.GetComponent<PlayerController>().jumpSpeed = x;
        }


    }
    //ani.SetTrigger("Jump"); 
    [PunRPC]
    void Trigger(string x)
    {
        //set animation for player
        //jump
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject y in players)
        {
            if(y.GetPhotonView() == this.photonView)
            y.GetComponent<Animator>().SetTrigger(x);  
        }

        
    }
    [PunRPC]
    //shoot
    void shoot(bool g, bool g2)
    {
        //get players
        GameObject[] x = GameObject.FindGameObjectsWithTag("Player");

        //show lightsource shooting for each player
        foreach (GameObject y in x)
        {
            if (y.GetPhotonView() == this.photonView)
            {
                //Debug.Log(" mine");
                y.GetComponent<GunControls>().active.GetComponent<Shoot>().getLine(g);
                y.GetComponent<GunControls>().active.GetComponent<Shoot>().lightsource.SetActive(g2);
               // y.GetComponent<GunControls>().active.GetComponent<Shoot>().soundeffect.Play();
                break;
            }
        }

    }
    [PunRPC]
    void shootSound()
    {
        //for each player
        //show sound of other player shooting
        GameObject[] x = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject y in x)
        {
            if (y.GetPhotonView() == this.photonView)
            {
                //Debug.Log(" mine");
                //y.GetComponent<GunControls>().active.GetComponent<Shoot>().getLine(g);
                //y.GetComponent<GunControls>().active.GetComponent<Shoot>().lightsource.SetActive(g2);
                y.GetComponent<GunControls>().active.GetComponent<Shoot>().soundeffect.Play();
                break;
            }
        }

    }
}

/*if (PhotonNetwork.connected)
           {
               GameObject[] x = GameObject.FindGameObjectsWithTag("Player");
               foreach (GameObject y in x)
               {
                   if (y.Equals(this))
                   {

                   }
                   else
                   {
                       Vector3 playerPos = transform.position;
                       Vector3 AIPos = y.transform.position;
                       Vector3 distanceBetweenThem = AIPos - playerPos;//new Vector3(2, 2, 2);
                       if ((distanceBetweenThem.x < 5 && distanceBetweenThem.x > -5) && (distanceBetweenThem.z < 5 && distanceBetweenThem.z > -5))
                       {
                           ensared = false;
                       }
                   }

               }
           }*/
