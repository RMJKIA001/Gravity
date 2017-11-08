using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControls : Photon.MonoBehaviour
{
    //variables
    public GameObject gun1;
    public GameObject gun2;
    public GameObject HUD;
    public GameObject active;
    public GameObject gunText;
    public bool shotGun;
    public bool lazerGun;
    public bool moreThanOne;

    // Use this for initialization
    void Start ()
    {
        //initially player has no guns
        gun1.SetActive(false);
        gun2.SetActive(false);
    }
	public void enableGun()
    {
        //set a gun as active
        gunText.SetActive(true);
    }
	// Update is called once per frame
	void Update ()
    {
        //two guns
        if(moreThanOne)
        {
            //accept input from scroll wheel
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                if (gun1.activeInHierarchy == true)
                {
                    //update view of other player
                    if (PhotonNetwork.connected)
                    {
                        photonView.RPC("gun", PhotonTargets.All, photonView.viewID, false, true);
                    }
                    else
                    {
                        //switch weapons
                        gun1.SetActive(false);
                        gun2.SetActive(true);
                        active = gun2;
                    }

                }
                else
                {
                    //update view of other player
                    if (PhotonNetwork.connected)
                    {
                        photonView.RPC("gun", PhotonTargets.All, photonView.viewID, true, false);
                    }
                    else
                    {
                        //switch weapons
                        gun1.SetActive(true);
                        gun2.SetActive(false);
                        active = gun1;
                    }

                }

            }
            //scroll the other way
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                if (gun1.activeInHierarchy == true)
                {
                    //set view of gun for other players
                    if (PhotonNetwork.connected)
                    {
                        photonView.RPC("gun", PhotonTargets.All, photonView.viewID, false, true);
                    }
                    else
                    {
                        //switcch gun
                        gun1.SetActive(false);
                        gun2.SetActive(true);
                        active = gun2;

                    }

                }
                else
                {
                    //set view of gun for other players
                    if (PhotonNetwork.connected)
                    {
                        photonView.RPC("gun", PhotonTargets.All, photonView.viewID, true, false);
                    }
                    else
                    {
                        //switch gun
                        gun1.SetActive(true);
                        gun2.SetActive(false);
                        active = gun1;

                    }

                }
            }
        }
        else
        {
            //shotgun equipped
            if(shotGun)
            {
                //update 2 player view
                if (PhotonNetwork.connected)
                {
                    photonView.RPC("gun", PhotonTargets.All, photonView.viewID, true, false);
                }
                else
                {
                    //set gun active
                    gun1.SetActive(true);
                    gun2.SetActive(false);

                    active = gun1;
                }
            }
            //lazer gun equipped
            else if(lazerGun)
            {
                //update two player view
                if (PhotonNetwork.connected)
                {
                    photonView.RPC("gun", PhotonTargets.All, photonView.viewID, false, true);
                }
                else
                {
                    //switch gun
                    gun1.SetActive(false);
                    gun2.SetActive(true);
                    active = gun2;

                }

            }
        }

        //either gun
        if(shotGun || lazerGun)
        {
            //add it to the HUD
            Shoot curr = active.GetComponent<Shoot>();
            HUD.GetComponent<HUD>().create(curr.type, curr.bullets, curr.maxBul);
        }
        else
        {
            //turn HUD off
            HUD.SetActive(false);
        }
    }
    [PunRPC]
    void gun(int a, bool g,bool g2)
    {
        //for each player
        GameObject[] x = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject y in x)
        {
            if (y.GetPhotonView().viewID == a)
            {
                //Debug.Log(" mine");

                //set gun active for each player
                y.GetComponent<GunControls>().gun1.SetActive(g);
                y.GetComponent<GunControls>().gun2.SetActive(g2);
                if (g)
                    y.GetComponent<GunControls>().active = y.GetComponent<GunControls>().gun1;
                else
                    y.GetComponent<GunControls>().active = y.GetComponent<GunControls>().gun2;
                break;
            }
        }

    }
}
