using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    //variables
    private int used;
    private int total;
    public string iname;

    //initialise HUD
    public void create(string nme, int x, int tot)
    {
        used = x;
        total = tot;
        iname = nme;
    }

    //decrease player health
    public void decrease(int i)
    {
        used = used - i;
    }

    //increase player health
    public bool increase(int i, string from)
    {
        //update values for health and armour depending
        int temp = used + i;
        if (temp > total)
        {
            if (from == "Armor")
            {
                //player health cannot be above 130
                if (used == 130)
                {
                    return false;
                }
                if (temp > 130)
                {
                    used = 130;
                    //return false;
                }
                else
                {
                    used = temp;
                }

                return true;
            }
            else
            {
                if(from=="Health")
                {
                    if (temp > 100)
                    {
                        used = 100;
                        return true;
                    }
                }
                return false;
            }
        }
        //if (temp > total)
        else
        {
            used = total;
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        //update HUD
        //set yellow if above 100 (armour used)
        if(iname=="Health" && used>100)
        {
            GetComponent<Text>().color = Color.yellow;
            //Debug.Log("yellow");
        }
        else
        {
            GetComponent<Text>().color = Color.white;
        }
        GetComponent<Text>().text = iname + ": " + used + "/" + total;
    }
}