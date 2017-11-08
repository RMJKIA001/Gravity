﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour {
    const string VERSION = "v1.0";
    // Use this for initialization
    public void clicked () {
       // PhotonNetwork.ConnectUsingSettings(VERSION);
    }

    //check connection to host of server
    private void OnConnectedToMaster()
    {
        Debug.Log("master");
        //?PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.playerName = PlayerNetwork.instance.pname;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    
    //add log of who joined a lobby
    void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby "+ PhotonNetwork.countOfPlayers);
        PhotonNetwork.playerName = PlayerNetwork.instance.pname;
        
    }
}
