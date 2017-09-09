using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {
    public GameObject trappedText;
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            Debug.Log("Hit Player");
            trappedText.GetComponent<Text>().enabled = true;
            trappedText.GetComponent<EnsnaredTimer>().timeLeft = 5f;
            player.GetComponent<PlayerController>().ensared = true;
            player.GetComponent<PlayerHealth>().Decrease(10);

        }
        else
        {
            Debug.Log("Never hit player");
        }
    }
}
