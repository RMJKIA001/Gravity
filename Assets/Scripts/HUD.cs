using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    private int used;
    private int total;
    private string iname;

    void Start()
    {
    }

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
        Debug.Log("Increase");
        int temp = used + i;
        if (used >= total)
        {
            if (from == "Armor")
            {
                if (used == 150)
                {
                    return false;
                }


                if (temp > 150)
                {
                    used = 150;
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
        GetComponent<Text>().text = iname + ": " + used + "/" + total;
    }
}