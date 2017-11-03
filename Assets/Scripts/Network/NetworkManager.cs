using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour {
    //const string VERSION = "v1.0";

    private string playerPrefab = "Player";
    private string platformPrefab = "GravityPlatform";

    public Transform spawn, platformSpawn, platformSpawn1, platformSpawn2, platformSpawn3, platformSpawn4, platformSpawn5;

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

        if(PhotonNetwork.isMasterClient) //first person to join the room
        {
          
        }
    
    }
	
}

/*
 
    private string spiderPrefab = "SPIDER";
    private string shotGunPrefab = "ShotGun";
    private string lazerGunPrefab = "LazerGun";
    private string healthPrefab = "Health";
    private string ammoPrefab = "Ammo";
    private string amorPrefab = "Armor";
    private string grunkPrefab = "Grunk";
    private string hunterPrefab = "Hunter";
    
    shotGunsSpawn, shotGunsSpawn1,healthSpawn, healthSpawn1, healthSpawn2, grunkSpawn,grunkSpawn1, grunkSpawn2, lazerGunsSpawn, lazerGunsSpawn1,
        ammoSpawn, ammoSpawn1, ammoSpawn2, ammoSpawn3, armorSpawn, armorSpawn1, spiderSpawn,spiderSpawn1, spiderSpawn2, spiderSpawn3, spiderSpawn4, spiderSpawn5, spiderSpawn6, spiderSpawn7, hunterSpawn;
 void Start () {
    //PhotonNetwork.ConnectUsingSettings(VERSION); //connect to server
}

void OnJoinedLobby()
{
    //create and join a room
    //RoomOptions rm = new RoomOptions() { IsVisible = false, MaxPlayers = 2 };
    //PhotonNetwork.JoinOrCreateRoom("Test", rm, TypedLobby.Default);
}
void OnCreatedRoom()
{
    // thes game objects are only instantiated once


   // 
}*/
// PhotonNetwork.Instantiate(platformPrefab, platformSpawn.position, platformSpawn.rotation, 0);
// PhotonNetwork.Instantiate(platformPrefab, platformSpawn1.position, platformSpawn1.rotation, 0);
// PhotonNetwork.Instantiate(platformPrefab, platformSpawn2.position, platformSpawn2.rotation, 0);
// PhotonNetwork.Instantiate(platformPrefab, platformSpawn3.position, platformSpawn3.rotation, 0);
// PhotonNetwork.Instantiate(platformPrefab, platformSpawn4.position, platformSpawn4.rotation, 0);
// PhotonNetwork.Instantiate(platformPrefab, platformSpawn5.position, platformSpawn5.rotation, 0);
// GameObject[] x = GameObject.FindGameObjectsWithTag("GravityPlatform");
// for (int i = 0; i < x.Length; i++)
// {
//     x[i].GetComponent<GravityControl>().setHeight(0, 5 - i);
// }
//PhotonNetwork.Instantiate(healthPrefab, healthSpawn.position, healthSpawn.rotation, 0);
//PhotonNetwork.Instantiate(healthPrefab, healthSpawn1.position, healthSpawn1.rotation, 0);
//PhotonNetwork.Instantiate(healthPrefab, healthSpawn2.position, healthSpawn2.rotation, 0);

// PhotonNetwork.Instantiate(ammoPrefab, ammoSpawn.position, ammoSpawn.rotation, 0);
// PhotonNetwork.Instantiate(ammoPrefab, ammoSpawn1.position, ammoSpawn1.rotation, 0);
// PhotonNetwork.Instantiate(ammoPrefab, ammoSpawn2.position, ammoSpawn2.rotation, 0);
// PhotonNetwork.Instantiate(ammoPrefab, ammoSpawn3.position, ammoSpawn3.rotation, 0);

//PhotonNetwork.Instantiate(amorPrefab, armorSpawn.position, armorSpawn.rotation, 0);
//PhotonNetwork.Instantiate(amorPrefab, armorSpawn1.position, armorSpawn1.rotation, 0);

//PhotonNetwork.Instantiate(grunkPrefab, grunkSpawn.position, grunkSpawn.rotation, 0);
//PhotonNetwork.Instantiate(grunkPrefab, grunkSpawn1.position, grunkSpawn1.rotation, 0);
//PhotonNetwork.Instantiate(grunkPrefab, grunkSpawn2.position, grunkSpawn2.rotation, 0);

//PhotonNetwork.Instantiate(spiderPrefab, spiderSpawn.position, spiderSpawn.rotation, 0);
//PhotonNetwork.Instantiate(spiderPrefab, spiderSpawn1.position, spiderSpawn1.rotation, 0);
//PhotonNetwork.Instantiate(spiderPrefab, spiderSpawn2.position, spiderSpawn2.rotation, 0);
//PhotonNetwork.Instantiate(spiderPrefab, spiderSpawn3.position, spiderSpawn3.rotation, 0);
//PhotonNetwork.Instantiate(spiderPrefab, spiderSpawn4.position, spiderSpawn4.rotation, 0);
//PhotonNetwork.Instantiate(spiderPrefab, spiderSpawn5.position, spiderSpawn5.rotation, 0);
//PhotonNetwork.Instantiate(spiderPrefab, spiderSpawn6.position, spiderSpawn6.rotation, 0);
//PhotonNetwork.Instantiate(spiderPrefab, spiderSpawn7.position, spiderSpawn7.rotation, 0);
//PhotonNetwork.Instantiate(shotGunPrefab, shotGunsSpawn.position, shotGunsSpawn.rotation, 0);
//PhotonNetwork.Instantiate(shotGunPrefab, shotGunsSpawn1.position, shotGunsSpawn1.rotation, 0);

//PhotonNetwork.Instantiate(lazerGunPrefab, lazerGunsSpawn.position, lazerGunsSpawn.rotation, 0);
//PhotonNetwork.Instantiate(lazerGunPrefab, lazerGunsSpawn1.position, lazerGunsSpawn1.rotation, 0);
//PhotonNetwork.Instantiate(hunterPrefab, hunterSpawn.position, hunterSpawn.rotation, 0);