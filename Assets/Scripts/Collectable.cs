using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Photon.MonoBehaviour
{
    public int value;
    public string type;
    public AudioSource effect;
    public GameObject particleEffect;
    private string pref;
    private bool GunEnabled = false;
    public void Start()
    {
        
    }
    //called when an object collides with this object
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //if player collides 
        {
            GameObject playerObj= other.gameObject;

            if (type == "ShotGun" || type == "LazerGun")
            {
                if(!GunEnabled)
                {
                  
                    playerObj.GetComponent<GunControls>().enableGun();
                    GunEnabled = true;
                }
                if (type == "ShotGun")
                {
                    if(playerObj.GetComponent<GunControls>().shotGun==false)
                    {
                        playerObj.GetComponent<GunControls>().shotGun = true;

                        if (playerObj.GetComponent<GunControls>().lazerGun)
                        {
                            playerObj.GetComponent<GunControls>().moreThanOne = true;
                        }
                        if (PhotonNetwork.connected)
                        {
                            //  if (photonView.isMine)
                            // {
                            photonView.RPC("Collected", PhotonTargets.All, gameObject.tag, photonView.viewID);
                            //effect.Play();
                            //}
                        }
                        else
                        {
                            effect.Play();
                        }
                        gameObject.SetActive(false);
                        
                    }
                }
                else
                {
                    if(!playerObj.GetComponent<GunControls>().lazerGun)
                    {
                        playerObj.GetComponent<GunControls>().lazerGun = true;
                        if (playerObj.GetComponent<GunControls>().shotGun)
                        {
                            playerObj.GetComponent<GunControls>().moreThanOne = true;
                        }
                        if (PhotonNetwork.connected)
                        {
                            //  if (photonView.isMine)
                            // {
                            photonView.RPC("Collected", PhotonTargets.All, gameObject.tag, photonView.viewID);
                            //effect.Play();
                            //}
                        }
                        else
                        {
                            effect.Play();
                        }
                        gameObject.SetActive(false);
                        
                    }  
                }
            }//gun
            else
            {
                bool possible = false;
                if (type == "Health" || type == "Armor")
                {
                    HUD[] x = playerObj.GetComponentsInChildren<HUD>();    
                    foreach (HUD h in x)
                    {
                        if (h.iname == "Health")
                        {
                            if (h.increase(value, type))
                            {
                                possible = true;
                                break;
                            }
                        }
                       

                    }
                    if (possible)
                    {
                        playerObj.GetComponent<PlayerHealth>().increase(value, type);
                    }
                }
                else if (type == "Ammo")
                {                    
                    if (playerObj.GetComponentInParent<GunControls>().lazerGun || playerObj.GetComponentInParent<GunControls>().shotGun)
                    {
                        possible = true;
                    }   
                    if (possible)
                    {
                        if(playerObj.GetComponent<GunControls>().active.GetComponent<Shoot>().bullets == playerObj.GetComponent<GunControls>().active.GetComponent<Shoot>().maxBul)
                        {
                            possible = false;
                        }
                        else
                        {
                            playerObj.GetComponent<GunControls>().active.GetComponent<Shoot>().bullets = playerObj.GetComponent<GunControls>().active.GetComponent<Shoot>().maxBul;

                        }
                    }   
                }

                if (possible)
                {
                    if (PhotonNetwork.connected)
                    {
                            photonView.RPC("Collected", PhotonTargets.All,gameObject.tag,photonView.viewID);
                    
                    }
                    else
                    {
                        effect.Play();
                    }
                    particleEffect.transform.position = this.transform.position;
                    pref = gameObject.tag + "Particle";
                    if (PhotonNetwork.connected) { photonView.RPC("DestObj", PhotonTargets.MasterClient, photonView.viewID, gameObject.tag ,pref); }
                    else
                    {
                        gameObject.SetActive(false);
                        
                        Instantiate(particleEffect);
                    }
                }
            }//not gun
        }//if player
    }
    [PunRPC]
    void Collected(string t,int objId)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(t);
        foreach (GameObject y in players)
        {
            if (y.GetPhotonView().viewID == objId)
            {
                effect.Play();
            }
        }
        
    }
    //destroys the object across the network
    [PunRPC]
    void DestObj(int objId,string t,string prefab)
    {
        GameObject dest = null;
        GameObject[] objs = GameObject.FindGameObjectsWithTag(t);
        foreach (GameObject curr in objs)
        {
            //curr.
            if (curr.GetPhotonView().viewID== objId)
            {
                dest = curr;
                break;
            }
        }
        //particleEffect.transform.position = this.transform.position;
        PhotonNetwork.Instantiate(prefab,particleEffect.transform.position, particleEffect.transform.rotation,0); // .Instantiate(particleEffect);
        PhotonNetwork.Destroy(dest);
    }
}
