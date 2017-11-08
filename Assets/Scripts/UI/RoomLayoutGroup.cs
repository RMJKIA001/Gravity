using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGroup : MonoBehaviour {
    [SerializeField]
    private GameObject roomListPref;

    //getter for prefab room listing
    private GameObject RoomListingPrefab
    {
        get { return roomListPref; }
    }

    //list of rooms
    private List<RoomListing> _roomListButt = new List<RoomListing>();
    private List<RoomListing> RoomListButt { get { return _roomListButt; } }

    //receive update
    private void OnReceivedRoomListUpdate()
    {
        Debug.Log("received room");
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        Debug.Log(rooms);
        //update the rooms
        foreach (RoomInfo r in rooms)
        {
            RoomReceived(r);
        }
        //get rid of old rooms
        RemoveOldRooms();
    }

    private void RoomReceived(RoomInfo room)
    {
        //get index
        int index = RoomListButt.FindIndex(x => x.RoomName == room.Name);
        if(index ==-1)
        {
            //check if room is visible and has less than max players
            if(room.IsVisible && room.PlayerCount < room.MaxPlayers)
            {
                //create room listing
                GameObject roomlistobj = Instantiate(RoomListingPrefab);
                roomlistobj.transform.SetParent(transform, false);

                RoomListing roomlisting = roomlistobj.GetComponent<RoomListing>();

                //add to list of rooms
                RoomListButt.Add(roomlisting);

                index = (RoomListButt.Count - 1);
            }
        }
         if (index !=-1)
        {
            //update the room listing
            RoomListing roomlisting = RoomListButt[index];
            roomlisting.SetRoomName(room.Name);
            roomlisting.updated = true;
        }
    }

    //remove old rooms
    private void RemoveOldRooms()
    {
        List<RoomListing> removeRooms = new List<RoomListing>();

        //iterate through each
        foreach(RoomListing rl in RoomListButt)
        {
            //check if it needs to be update, remove if necessary
            if(!rl.updated)
            {
                removeRooms.Add(rl);
            }
            else
            {
                rl.updated = false;
            }
        }
        //perform destruction of rooms
        foreach (RoomListing rl in removeRooms)
        {
            GameObject roomlistobj = rl.gameObject;
            RoomListButt.Remove(rl);
            Destroy(roomlistobj);

        }
    }
}
