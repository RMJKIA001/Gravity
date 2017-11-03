using UnityEngine;
using UnityEngine.UI;
public class RoomListing : MonoBehaviour {

    [SerializeField]
    private Text _roomNameText;
    public Text RoomNameText
    {
        get { return _roomNameText; }
    }
    public string RoomName { get; private set; }
    public bool updated { get; set; }
	// Use this for initialization
	void Start () {
        GameObject lobbyCanvasObj = MainCanvas.Inst.LobbyCanvas.gameObject;
        if(lobbyCanvasObj == null)
        {
            return;
        }
        LobbyCanvas lobbyCanvas = lobbyCanvasObj.GetComponent<LobbyCanvas>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(()=>lobbyCanvas.OnClickJoinRoom(RoomNameText.text));
	}

    public void SetRoomName(string tx)
    {
        RoomName = tx;
        RoomNameText.text = RoomName;
    }

    private void OnDestroy()
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }

    /*public void OnClickJoinRoom(string room)
    {
        Debug.Log("Room Listing");
        PhotonNetwork.JoinRoom(room);
    }*/
}
