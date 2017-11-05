using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CanvasButton : MonoBehaviour {

	public void exit()
    {
        Application.Quit();
    }
    public void back()
    {
        if (PhotonNetwork.connected) { PhotonNetwork.Disconnect(); }
        SceneManager.LoadScene("Menu");
    }
}
