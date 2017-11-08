using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour {
    //const string VERSION = "v1.0";

    private string playerPrefab = "Player";

    public Transform spawn; //players spawn pos

    void OnMasterClientSwitch()
    {
        PhotonNetwork.LeaveRoom();
    }
    void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Menu");
    }
    void OnJoinedRoom()
    {
        //these objects are gernerated when the player joins a rooom
        PhotonNetwork.Instantiate(playerPrefab, spawn.position, spawn.rotation, 0);
    
    }
	
}
