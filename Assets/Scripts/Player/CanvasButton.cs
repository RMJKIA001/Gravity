using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CanvasButton : MonoBehaviour {

    //displays whether or not player died
    public float timeLeft = 0;
    public bool end;
    public GameObject dead;


    public void exit()
    {
        //load menu scene
        if (PhotonNetwork.connected) { PhotonNetwork.Disconnect(); }
        SceneManager.LoadScene("Menu");
    }

    public void back()
    {
        //respawn
        SceneManager.LoadScene("CapstoneSinglePlayer");
    }

    public void died()
    {
        //5 seconds of being dead
        timeLeft = 5f;

    }
        void FixedUpdate()
    {
        //update timer
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            if (!end)
            {
                //respawn
                dead.SetActive(false);
                timeLeft = 0;
            }
        }
       
    }
}
