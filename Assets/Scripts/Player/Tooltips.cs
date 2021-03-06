﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tooltips : MonoBehaviour
{
    public GameObject player;
    // Update is called once per frame
    //checks the position of the player, if in range of specific items then display the tip
    void Update ()
    {
        //if two players are playing
        if (!PhotonNetwork.connected)
        {
            //set tool tips based on player position
            Transform mytransform = player.GetComponent<Transform>();

            //weapons tooltip
            if (mytransform.position.z > 10 && mytransform.position.z < 20 && mytransform.position.x > 3 && mytransform.position.x < 10)
            {
                GetComponent<Text>().text = "You can pick up weapons by walking over them.";
            }

            //powerups tooltip
            else if (mytransform.position.z > 30 && mytransform.position.z < 40 && mytransform.position.x > -2 && mytransform.position.x < 5)
            {
                GetComponent<Text>().text = "You can pick up power-ups like health by walking over it.";
            }

            //gravity 2 tooltip
            else if (mytransform.position.z > 30 && mytransform.position.z < 45 && mytransform.position.x > 6 && mytransform.position.x < 10)
            {
                GetComponent<Text>().text = "Press 2 to manipulate gravity and jump higher.";
            }

            //gravity 3 tooltop
            else if (mytransform.position.z > 44 && mytransform.position.z < 52 && mytransform.position.x > 4 && mytransform.position.x < 7)
            {
                GetComponent<Text>().text = "Press 3 to manipulate the gravity of platforms.";
            }

            //shooting tool tip
            else if (mytransform.position.z > 45 && mytransform.position.z < 55 && mytransform.position.x > 24 && mytransform.position.x < 30)
            {
                GetComponent<Text>().text = "Left click to shoot enemies.";
            }

            //enemies tool tip
            else if (mytransform.position.x > 30 && mytransform.position.x < 35)
            {
                GetComponent<Text>().text = "Enemies will hurt you. Left click to shoot them.";
            }
            else
            {
                GetComponent<Text>().text = "";
            }
        }
	}
}
