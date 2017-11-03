using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : Photon.MonoBehaviour
{
    public int totalhealth =100;
    public int currenthealth;
    public  GameObject healthbar;

    private void Start()
    {
        healthbar.GetComponent<TextMesh>().text = currenthealth + "/" + totalhealth;
    }

    //decrease health by damage
    public void Hit (int damage)
    {
        currenthealth = currenthealth - damage;
        if (currenthealth <= 0 )
        {
            gameObject.SetActive(false);
            healthbar.SetActive(false);  
        }
        if (PhotonNetwork.connected) { photonView.RPC("updateHealth", PhotonTargets.All, photonView.viewID, currenthealth); }
        healthbar.GetComponent<TextMesh>().text = currenthealth + "/" + totalhealth;
        
    }

    //Synchronises the damage done to the enemy acrosss the network.
    [PunRPC]
    void updateHealth(int a, int health)
    {
        GameObject[] x = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject y in x)
        {
            if (y.GetPhotonView().viewID == a)
            {
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
