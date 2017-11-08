using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelScript : Photon.MonoBehaviour
{
   
    public void load()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("EndLevelStory");
    }
	// Update is called once per frame
    //Loads the end level Scene when the player has past a specific point, indicating they won
	void Update ()
    {
        if (transform.position.x > 236)
        {
            if (PhotonNetwork.connected)
            {
                photonView.RPC("scene",PhotonTargets.All);
            }
            else
            {
                load();
            }
        }
	}

    [PunRPC]
    void scene()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject x in players)
        {
            x.GetComponent<EndLevelScript>().load();
        }

    }
}
