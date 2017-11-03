using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour {
    public GameObject myCamera;
    public GameObject canvas;
	
	void Start () {
        //so that you only control yourself and not the other people in the network
        if(photonView.isMine)
        {
            myCamera.SetActive(true);
            canvas.SetActive(true);
            GetComponent<PlayerController>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<GunControls>().enabled = true;
            GetComponent<PlayerHealth>().enabled = true;
        }
        //because prefab starts of with these features disabled
        if((!PhotonNetwork.connected))
        {
            myCamera.SetActive(true);
            canvas.SetActive(true);
            GetComponent<PlayerController>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<GunControls>().enabled = true;
            GetComponent<PlayerHealth>().enabled = true;
        }
        
    }


	
}
