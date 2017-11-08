using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shootable : Photon.MonoBehaviour
{

    /*
     allows agent to be shot
         */
    public int totalhealth =100;
    public int currenthealth;
    public  GameObject healthbar;
    public GameObject[] x;

    private void Start()
    {
        x = GameObject.FindGameObjectsWithTag("Enemy"); // finds all the enemies in the scene this is for the networking part 
        healthbar.GetComponent<TextMeshProUGUI>().SetText(currenthealth + "/" + totalhealth);
    }

    //decrease health by damage
    public void Hit (int damage)
    {
        currenthealth = currenthealth - damage;
        if (PhotonNetwork.connected) { photonView.RPC("updateHealth", PhotonTargets.All, photonView.viewID, currenthealth); } //ensure everyone knows that the agent has been hit
        else
        {
            if (currenthealth <= 0 )
            {
                gameObject.SetActive(false);
               // healthbar.SetActive(false);
             }
            else
            {
                healthbar.GetComponent<TextMeshProUGUI>().SetText(currenthealth + "/" + totalhealth);
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
            if (y.GetPhotonView().viewID == a) //only update the agent who was hit
            {
                Debug.Log("Mine");
                y.GetComponent<Shootable>().currenthealth  = health;
                if (currenthealth <= 0)
                {

                    gameObject.SetActive(false);
                   
                }
                y.GetComponent<Shootable>().healthbar.GetComponent<TextMeshProUGUI>().SetText(currenthealth + "/" + totalhealth);
                break;
            }
        }

    }
}
