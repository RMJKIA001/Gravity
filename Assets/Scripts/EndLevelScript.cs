using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelScript : MonoBehaviour
{
    void load()
    {
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
                PhotonNetwork.LoadLevel("EndLevelStory");
                
            }
            else
            {
                load();
            }
        }
	}
}
