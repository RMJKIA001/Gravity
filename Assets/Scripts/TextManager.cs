using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    public bool end;
    // Update is called once per frame

    //updates the postion of the story text and loads next scene when it is off screen
    void Update ()
    {
        float y = (float)(transform.position.y + 0.5);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);

        if (transform.position.y > 500)
        {
            if(!end)
            SceneManager.LoadScene("CapstoneSinglePlayer");
            else
            {
                if (PhotonNetwork.connected) { PhotonNetwork.Disconnect(); }
                SceneManager.LoadScene("Menu");
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            if(!end)
            SceneManager.LoadScene("CapstoneSinglePlayer");
            else {
                if (PhotonNetwork.connected) { PhotonNetwork.Disconnect(); }
                SceneManager.LoadScene("Menu");
            }
        }
	}
}
