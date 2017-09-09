using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public int currhealth = 50;
    public GameObject HUD;
    public GameObject dead;
    // Use this for initialization
    void Start () {
		
	}

    public void increase(int i , string from)
    {
        int temp = currhealth + i;
        if (from == "Armor")
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

    public void Decrease(int i)
    {
        int temp = currhealth - i;
        if(temp<=0)
        {
            //dead
            dead.SetActive(true);
            GetComponent<PlayerController>().enabled = true;
        }
        currhealth = temp;
    }
	
	// Update is called once per frame
	void Update () {

        HUD.GetComponent<HUD>().create("Health", currhealth, 100);
    }
}
