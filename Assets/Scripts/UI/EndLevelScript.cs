using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelScript : Photon.MonoBehaviour
{
   //end level script controls loading of the final cutscene
    public void load()
    {
        //re-enable mouse
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //load the cutscene
        SceneManager.LoadScene("EndLevelStory");
    }
	// Update is called once per frame
    //Loads the end level Scene when the player has past a specific point, indicating they won
	void Update ()
    {
        //perform the same for two players
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
        //load it for all players
        //for two player
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject x in players)
        {
            x.GetComponent<EndLevelScript>().load();
        }

    }
}
