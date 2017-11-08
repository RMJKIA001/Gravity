using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView : MonoBehaviour {
    [SerializeField]
    private GameObject roomListPref;
    private GameObject RoomListingPrefab
    {
        get { return roomListPref; }
    }

    //list of rooms to join
    private List<RoomListing> roomListButt = new List<RoomListing>();
    private List<RoomListing> RoomListButt { get { return roomListButt; } }

    //set panel active
    void OnEnable()
    {
        Debug.Log("Enabled");
        //get list of possible rooms
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        Debug.Log(rooms);
        foreach (RoomInfo r in rooms)
        {
            RoomReceived(r);
        }
        //destroy old rooms
        RemoveOldRooms();
    }

    //get room
    private void RoomReceived(RoomInfo room)
    {

        int index = RoomListButt.FindIndex(x => x.RoomNameText.text == room.Name);
        if (index == -1)
        {
            //make room appear
            if (room.IsVisible && room.PlayerCount < room.MaxPlayers)
            {
                //create object for room
                GameObject roomlistobj = Instantiate(RoomListingPrefab);

                //set parent
                roomlistobj.transform.SetParent(transform, false);

                RoomListing roomlisting = roomlistobj.GetComponent<RoomListing>();

                //add to list of rooms
                RoomListButt.Add(roomlisting);

                index = (RoomListButt.Count - 1);
            }
        }
        //updates
        if (index != -1)
        {
            RoomListing roomlisting = RoomListButt[index];
            roomlisting.SetRoomName(room.Name);
            roomlisting.updated = true;
        }
    }

    //remove old rooms if necessary
    private void RemoveOldRooms()
    {
        List<RoomListing> removeRooms = new List<RoomListing>();
        foreach (RoomListing rl in RoomListButt)
        {
            if (!rl.updated)
            {
                removeRooms.Add(rl);
            }
            else
            {
                rl.updated = false;
            }
        }
        //get rid of old lobbys if necessary
        foreach (RoomListing rl in removeRooms)
        {
            GameObject roomlistobj = rl.gameObject;
            RoomListButt.Remove(rl);
            Destroy(roomlistobj);

        }
    }
}
