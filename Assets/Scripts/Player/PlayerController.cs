using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Photon.MonoBehaviour {
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
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
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
        ani.SetBool("Walking", isWalking);
       
        if (Input.GetKey(KeyCode.LeftShift) && isWalking)
        {
            vert *= 2f;
        }

        if (Input.GetButtonDown("Jump") && cont.isGrounded)
        {
            velocity = jumpSpeed;
            if (PhotonNetwork.connected) { GetComponent<PhotonView>().RPC("Trigger", PhotonTargets.All,"Jump"); }
            else {
                ani.SetTrigger("Jump"); }
            
        }
       

        //Manipulate gravity
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            jumpSpeed = 8;
            
            if (PhotonNetwork.connected) { photonView.RPC("MoveObj", PhotonTargets.All, false); }
            else
            {
                GameObject[] platforms = GameObject.FindGameObjectsWithTag("GravityPlatform");
                foreach (GameObject platform in platforms)
                {
                    platform.GetComponent<GravityControl>().setGravity(false);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            jumpSpeed = 15;
            
            if (PhotonNetwork.connected) { photonView.RPC("MoveObj", PhotonTargets.All, false); }
            else
            {
                GameObject[] platforms = GameObject.FindGameObjectsWithTag("GravityPlatform");
                foreach (GameObject platform in platforms)
                {
                    platform.GetComponent<GravityControl>().setGravity(false);
                }
            }
        }   
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            jumpSpeed = 6;
            
            if (PhotonNetwork.connected) { photonView.RPC("MoveObj", PhotonTargets.All,true); }
            else
            {
                GameObject[] platforms = GameObject.FindGameObjectsWithTag("GravityPlatform");

                foreach (GameObject platform in platforms)
                {
                    platform.GetComponent<GravityControl>().setGravity(true);
                }
            }
        }

        Vector3 move;
        if (!ensared)
        {
            move = new Vector3(hori * speed, velocity, vert * speed);
        }
        else
        {
           
            move = new Vector3(0, 0, 0);
            if (PhotonNetwork.connected) { photonView.RPC("Trigger", PhotonTargets.All,"Idle"); }
            else{   ani.SetTrigger("Idle");
        }
           
        }
        cont.Move(transform.rotation * move * Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.Escape)) { Application.Quit();}
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
    //ani.SetTrigger("Jump"); 
    [PunRPC]
    void Trigger(string x)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject y in players)
        {
            if(y.GetPhotonView() == this.photonView)
            y.GetComponent<Animator>().SetTrigger(x);  
        }

        
    }
    [PunRPC]
    void shoot(bool g, bool g2)
    {

        GameObject[] x = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject y in x)
        {
            if (y.GetPhotonView() == this.photonView)
            {
                //Debug.Log(" mine");
                y.GetComponent<GunControls>().active.GetComponent<Shoot>().getLine(g);
                y.GetComponent<GunControls>().active.GetComponent<Shoot>().lightsource.SetActive(g2);
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
