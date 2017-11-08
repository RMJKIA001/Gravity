using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour {
    [SerializeField]
    private RoomLayoutGroup _roomLayoutGroup;
    private RoomLayoutGroup RoomLayoutGroup { get { return _roomLayoutGroup; } }

    //when player clicks on room to join
    public void OnClickJoinRoom(string room)
    {
        //enter room and load level
        Debug.Log("Lobby Canvas");
        PhotonNetwork.JoinRoom(room);
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.LoadLevel("NetworkPrototype");
    }
    //OnJoinedRoom ()
}
