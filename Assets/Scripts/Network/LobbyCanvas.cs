using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour {
    [SerializeField]
    private RoomLayoutGroup _roomLayoutGroup;
    private RoomLayoutGroup RoomLayoutGroup { get { return _roomLayoutGroup; } }

    public void OnClickJoinRoom(string room)
    {
        Debug.Log("Lobby Canvas");
        PhotonNetwork.JoinRoom(room);
        PhotonNetwork.LoadLevel("NetworkPrototype");
    }
    //OnJoinedRoom ()
}
