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

    private List<RoomListing> roomListButt = new List<RoomListing>();
    private List<RoomListing> RoomListButt { get { return roomListButt; } }


    void OnEnable()
    {
        Debug.Log("Enabled");
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        Debug.Log(rooms);
        foreach (RoomInfo r in rooms)
        {
            RoomReceived(r);
        }
        RemoveOldRooms();
    }
    private void RoomReceived(RoomInfo room)
    {
        int index = RoomListButt.FindIndex(x => x.RoomNameText.text == room.Name);
        if (index == -1)
        {
            if (room.IsVisible && room.PlayerCount < room.MaxPlayers)
            {
                GameObject roomlistobj = Instantiate(RoomListingPrefab);
                roomlistobj.transform.SetParent(transform, false);

                RoomListing roomlisting = roomlistobj.GetComponent<RoomListing>();
                RoomListButt.Add(roomlisting);

                index = (RoomListButt.Count - 1);
            }
        }
        if (index != -1)
        {
            RoomListing roomlisting = RoomListButt[index];
            roomlisting.SetRoomName(room.Name);
            roomlisting.updated = true;
        }
    }

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
        foreach (RoomListing rl in removeRooms)
        {
            GameObject roomlistobj = rl.gameObject;
            RoomListButt.Remove(rl);
            Destroy(roomlistobj);

        }
    }
}
