using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CanvasButton : MonoBehaviour {
    public float timeLeft = 0;
    public bool end;
    public GameObject dead;
    public void exit()
    {
        if (PhotonNetwork.connected) { PhotonNetwork.Disconnect(); }
        SceneManager.LoadScene("Menu");
    }
    public void back()
    {
        SceneManager.LoadScene("CapstoneSinglePlayer");
    }
    public void died() {
        timeLeft = 5f;

    }
        void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            if (!end)
            {
                dead.SetActive(false);
                timeLeft = 0;
            }
        }
       
    }
}
