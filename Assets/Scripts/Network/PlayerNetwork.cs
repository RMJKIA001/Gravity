﻿using UnityEngine;

public class PlayerNetwork : MonoBehaviour {

    /*
     starts network and keeps it active
         */
    public static PlayerNetwork instance;
    public string pname { get; set; }

	// Use this for initialization
	void Awake () {
        instance = this;
        
	}


}
