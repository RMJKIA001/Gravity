using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 class not used in this build
     */
public class Bullet : MonoBehaviour {
    public GameObject trappedText;
    public GameObject player;

    //dynamically assign because of player prefabs
    public void setPlayer(GameObject player)
    {
        this.player = player;
        trappedText = player.GetComponentInChildren<Canvas>().GetComponent<CanComp>().Trapped;
    }  
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            Debug.Log("Hit Player");
            if (PhotonNetwork.connected)
            {
                if (player.GetPhotonView().isMine) //only display if i am being trapped
                {
                    trappedText.GetComponent<Text>().enabled = true;
                    trappedText.GetComponent<EnsnaredTimer>().timeLeft = 5f;
                }
                player.GetComponent<PlayerController>().ensared = true;
                player.GetComponent<PlayerHealth>().Decrease(10);
            }
            else
            {
                //not on network
                trappedText.GetComponent<Text>().enabled = true;
                trappedText.GetComponent<EnsnaredTimer>().timeLeft = 5f;
                player.GetComponent<PlayerController>().ensared = true;
                player.GetComponent<PlayerHealth>().Decrease(10);
            }
        }
        else
        {
           // Debug.Log("Never hit player");
        }
    }
}
