﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int currhealth = 50;
    public GameObject HUD;
    public GameObject dead;
    public GameObject ensnaredText;
    public GameObject gunText;
    public GameObject deadtext;
    public GameObject exit;
    public GameObject back;

    // Use this for initialization
    void Start()
    {
        deadtext.SetActive(false);
    }

    //increases health by int i, sting indicates if its from aromor or healthpack 
    public void increase(int i, string from)
    {
        int temp = currhealth + i;
        if (from == "Armor") //allows exta
        {
            //set health at 130
            if (temp > 130)
            {
                currhealth = 130;
            }
            else
            {
                //set new health
                currhealth = temp;
            }
        }
        else
        {
            //health cannot go over 100, unless armour used
            if (temp > 100)
            {
                currhealth = 100;
            }
            else
            {
                currhealth = temp;
            }
        }
    }
    //decrease health by i
    public void Decrease(int i)
    {
        int temp = currhealth - i;

        //check if player needs to die
        if (temp <= 0)
        {
            //dead
            Dead();
        }
        else
        {
            //set new health after damage
            currhealth = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {

        HUD.GetComponent<HUD>().create("Health", currhealth, 100);

        if (transform.position.y < -300)
        {
            Dead();
            
        }

    }
    void Dead()
    {
        //dead
        if (PhotonNetwork.connected)
        {
            if (GetComponentInParent<PhotonView>().isMine)
            {
                //Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.None;
               // back.SetActive(true);
                //exit.SetActive(true);
                dead.SetActive(true);
                transform.position = new Vector3(0, 1, 0);
                currhealth = 100;
                //HUD.SetActive(false);
                //gunText.SetActive(false);
                //ensnaredText.SetActive(false);
                //deadtext.SetActive(true);
                //transform.position = new Vector3(-1000, -1000, -1000);
                //GetComponent<PlayerController>().enabled = false;
            }
        }
        else
        {
            //set things disabled if player dies
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            back.SetActive(true);
            exit.SetActive(true);
            dead.SetActive(true);
            HUD.SetActive(false);
            gunText.SetActive(false);
            ensnaredText.SetActive(false);
            deadtext.SetActive(true);
            transform.position = new Vector3(-1000, -1000, -1000);
            GetComponent<PlayerController>().enabled = false;
        }
    }
}
