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
        //Debug.Log("Start");
       // effect = gameObject.GetComponent<AudioSource>();
    }
    //called when an object collides with this object
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject playerObj= other.gameObject;
            
            if (type == "ShotGun" || type == "LazerGun")
            {
                if(!GunEnabled)
                {
                    playerObj.GetComponent<GunControls>().enableGun();
                    enabled = true;
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
                    //Debug.Log("befor: "+ effect+" "+ effect.isPlaying);
                    //gameObject.GetComponent<AudioSource>().Play();
                    effect.Play();
                    //Debug.Log("After: "+effect + " " + effect.isPlaying);
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
