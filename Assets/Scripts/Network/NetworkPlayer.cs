﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour {
    public GameObject myCamera;
    public GameObject canvas;
   // public GameObject shot,laz;

	void Start () {
        
        //so that you only control yourself and not the other people in the network
        if(GetComponent<PhotonView>().isMine)
        {
            myCamera.GetComponent<Camera>().enabled = true;
            myCamera.GetComponent<AudioListener>().enabled = true;
            //myCamera.SetActive(true);
            canvas.SetActive(true);
            GetComponent<PlayerController>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<GunControls>().enabled = true;
            GetComponent<PlayerHealth>().enabled = true;
            //GetComponent<Animator>().enabled = true;
        }
        //because prefab starts of with these features disabled
        if((!PhotonNetwork.connected))
        {
            myCamera.GetComponent<Camera>().enabled = true;
            myCamera.GetComponent<AudioListener>().enabled = true;
            //myCamera.SetActive(true);
            canvas.SetActive(true);
            GetComponent<PlayerController>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<GunControls>().enabled = true;
            GetComponent<PlayerHealth>().enabled = true;
            //GetComponent<Animator>().enabled = true;
        }
        
    }


	
}
