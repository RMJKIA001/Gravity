using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControls : Photon.MonoBehaviour {

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
        gun1.SetActive(false);
        gun2.SetActive(false);
    }
	public void enableGun()
    {
        gunText.SetActive(true);
    }
	// Update is called once per frame
	void Update ()
    {
        if(moreThanOne)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                if (gun1.activeInHierarchy == true)
                {
                    
                    if (PhotonNetwork.connected)
                    {
                        photonView.RPC("gun", PhotonTargets.All, photonView.viewID, false, true);
                    }
                    else
                    {
                        gun1.SetActive(false);
                        gun2.SetActive(true);
                        active = gun2;
                    }

                }
                else
                {
                    
                    if (PhotonNetwork.connected)
                    {
                        photonView.RPC("gun", PhotonTargets.All, photonView.viewID, true, false);
                    }
                    else
                    {
                        gun1.SetActive(true);
                        gun2.SetActive(false);
                        active = gun1;
                    }

                }

            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                if (gun1.activeInHierarchy == true)
                {
                    if (PhotonNetwork.connected)
                    {
                        photonView.RPC("gun", PhotonTargets.All, photonView.viewID, false, true);
                    }
                    else
                    {
                        gun1.SetActive(false);
                        gun2.SetActive(true);
                        active = gun2;

                    }

                }
                else
                {
                    if (PhotonNetwork.connected)
                    {
                        photonView.RPC("gun", PhotonTargets.All, photonView.viewID, true, false);
                    }
                    else
                    {
                        gun1.SetActive(true);
                        gun2.SetActive(false);
                        active = gun1;

                    }

                }
            }
        }
        else
        {
            if(shotGun)
            {
                if (PhotonNetwork.connected)
                {
                    photonView.RPC("gun", PhotonTargets.All, photonView.viewID, true, false);
                }
                else
                {
                    gun1.SetActive(true);
                    gun2.SetActive(false);

                    active = gun1;
                }
            }
            else if(lazerGun)
            {
                if (PhotonNetwork.connected)
                {
                    photonView.RPC("gun", PhotonTargets.All, photonView.viewID, false, true);
                }
                else
                {
                    gun1.SetActive(false);
                    gun2.SetActive(true);
                    active = gun2;

                }

            }
        }
        if(shotGun || lazerGun)
        {
            Shoot curr = active.GetComponent<Shoot>();
            HUD.GetComponent<HUD>().create(curr.type, curr.bullets, curr.maxBul);
        }
        else
        {
            HUD.SetActive(false);
        }
    }
    [PunRPC]
    void gun(int a, bool g,bool g2)
    {
        
        GameObject[] x = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject y in x)
        {
            if (y.GetPhotonView().viewID == a)
            {
                //Debug.Log(" mine");
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
