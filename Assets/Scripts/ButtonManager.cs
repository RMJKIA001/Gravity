using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {
    public GameObject play,howto,exit,sinlge,two,start,join,create,back,canv,howToC,Ename, scrollView, createRM,rmname;
   
    [SerializeField]
    private Text _roomName;
    private Text RoomName
    {
        get { return _roomName; }
    }
    public void Start()
    {
         
    }
  
    public void RoomNameEdited()
    {
        createRM.GetComponent<Button>().interactable = true;
    }
    void OnCreatedRoom()
    {
        //Debug.Log("I have been created");
        PhotonNetwork.LoadLevel("NetworkPrototype");
    }
    public void CreateRoom()
    {
        RoomOptions rm = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };    
        if(PhotonNetwork.CreateRoom(RoomName.text,rm,TypedLobby.Default))
        {
            Debug.Log("success");
            createRM.SetActive(false);
            rmname.SetActive(false);
            
        }
    }
    public void Playbtn()
    {
         play.SetActive(false);
         howto.SetActive(false);
         exit.SetActive(false);
         sinlge.SetActive(true);
         two.SetActive(true);
         back.SetActive(true);
    }
 
    public void HowTobtn()
    {
        if(canv.activeSelf)
        {
            canv.SetActive(false);
            howToC.SetActive(true);
        }

    }
 

    public void Exitbtn()
    {
        Application.Quit();
    }

    public void SinglePlayerbtn ()
    {
        sinlge.SetActive(false);
        two.SetActive(false);
        start.SetActive(true);
    }

    public void Newbtn()
    {
        SceneManager.LoadScene("BeginLevelStory");
    }
    //Not for prototype
    public void TwoPlayerbtn()
    {
        sinlge.SetActive(false);
        two.SetActive(false);
        //join.SetActive(true);
        create.SetActive(true);
        //join.GetComponent<Button>().interactable = false;
        create.GetComponent<Button>().interactable = false;
        Ename.SetActive(true);
    }

    public void Createbtn()
    {
        //SceneManager.LoadScene("NetworkPrototype"); //No Lobby yet
        //join.SetActive(false);
        create.SetActive(false);
        Ename.SetActive(false);
        createRM.SetActive(true);
        rmname.SetActive(true);
        createRM.GetComponent<Button>().interactable = false;
        scrollView.SetActive(true);
    }
    //Not for prototype
    public void Joinbtn()
    {
        
        join.SetActive(false);
        create.SetActive(false);
        Ename.SetActive(false);
        createRM.SetActive(false);
        rmname.SetActive(false);
        scrollView.SetActive(true);
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        Debug.Log("Join : " + rooms);
    }

    public void Backbtn()
    {
        if(sinlge.activeSelf)
        {
            play.SetActive(true);
            howto.SetActive(true);
            exit.SetActive(true);
            sinlge.SetActive(false);
            two.SetActive(false);
            back.SetActive(false);
        }
        if(create.activeSelf || start.activeSelf)
        {
            sinlge.SetActive(true);
            two.SetActive(true);
            join.SetActive(false);
            Ename.SetActive(false);
            create.SetActive(false);
            start.SetActive(false);
            if (PhotonNetwork.connected) { PhotonNetwork.Disconnect(); }
            
        }
        if(howToC.activeSelf)
        {
            howToC.SetActive(false);
            canv.SetActive(true);
        }

        if(createRM.activeSelf)
        {
            createRM.SetActive(false);
            rmname.SetActive(false);
            scrollView.SetActive(false);
            sinlge.SetActive(false);
            two.SetActive(false);
            create.SetActive(true);
            Ename.SetActive(true);
        }

    }

}
