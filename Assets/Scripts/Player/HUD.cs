using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    private int used;
    private int total;
    public string iname;

    public void create(string nme, int x, int tot)
    {
        used = x;
        total = tot;
        iname = nme;
    }
    public void decrease(int i)
    {
        used = used - i;
    }

    public bool increase(int i, string from)
    {
        int temp = used + i;
        if (used >= total)
        {
            if (from == "Armor")
            {
                if (used == 130)
                {
                    return false;
                }
                if (temp > 130)
                {
                    used = 130;
                }
                else
                {
                    used = temp;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        if (temp > total)
        {
            used = total;
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
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