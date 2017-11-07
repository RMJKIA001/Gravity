using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : Photon.MonoBehaviour
{
    public int totalhealth =100;
    public int currenthealth;
    public  GameObject healthbar;
    public GameObject[] x;

    private void Start()
    {
        x = GameObject.FindGameObjectsWithTag("Enemy");
        healthbar.GetComponent<TextMesh>().text = currenthealth + "/" + totalhealth;
    }

    //decrease health by damage
    public void Hit (int damage)
    {
        currenthealth = currenthealth - damage;
        if (PhotonNetwork.connected) { photonView.RPC("updateHealth", PhotonTargets.All, photonView.viewID, currenthealth); }
        else
        {
            if (currenthealth <= 0 )
            {
                gameObject.SetActive(false);
                healthbar.SetActive(false);  
             }
            else
            {
               healthbar.GetComponent<TextMesh>().text = currenthealth + "/" + totalhealth;
            }
        }
    }
    
    //Synchronises the damage done to the enemy acrosss the network.
    [PunRPC]
    void updateHealth(int a, int health)
    {
        Debug.Log("RPC");
        

        foreach (GameObject y in x)
        {
            if (y.GetPhotonView().viewID == a)
            {
                Debug.Log("Mine");
                y.GetComponent<Shootable>().currenthealth  = health;
                if (currenthealth <= 0)
                {

                    gameObject.SetActive(false);
                    healthbar.SetActive(false);

                }
                y.GetComponent<Shootable>().healthbar.GetComponent<TextMesh>().text = currenthealth + "/" + totalhealth;
                break;
            }
        }

    }
}
