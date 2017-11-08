using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnsnaredTimer : MonoBehaviour {
    public float timeLeft=0;
    public GameObject player;
   
	// count down using current time being trapped
	void FixedUpdate ()
    {
        //get time
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            //allow player to move again
            GetComponent<Text>().enabled = false;
            player.GetComponent<PlayerController>().ensared = false;
            timeLeft = 0;
        }
        else
        {
            //update HUD if still trapped
            GetComponent<Text>().text = "ENSARED\n"+timeLeft.ToString("F2") + " seconds remaining";
        }
    }
}



