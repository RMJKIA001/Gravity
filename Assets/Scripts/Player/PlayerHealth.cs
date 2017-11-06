using System.Collections;
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
            if (temp > 130)
            {
                currhealth = 130;
            }
            else
            {
                currhealth = temp;
            }
        }
        else
        {
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
        if (temp <= 0)
        {
            //dead
            Dead();
        }
        currhealth = temp;
    }

    // Update is called once per frame
    void Update()
    {

        HUD.GetComponent<HUD>().create("Health", currhealth, 100);

        if (transform.position.y < -50)
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
