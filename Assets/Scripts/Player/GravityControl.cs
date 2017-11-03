using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    public int height1;
    public int height2;
    public float step;
    public bool up;
    bool gravity = false;
    
     
    public void setHeight(int a,int b)
    {
        height1 = a;
        height2 = b;
    }

    // moves platforms between height1 and height2 when the user manipulates gravity.
    void Update()
    {
        if (up)
        {
            if (gravity == true)
            {
                if (transform.position.y >= height2)
                {
                    transform.position = new Vector3(transform.position.x, height2, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + step, transform.position.z);
                }
            }
            else
            {
                if (transform.position.y <= height1)
                {
                    transform.position = new Vector3(transform.position.x, height1, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - step, transform.position.z);
                }
            }
        }
        else
        {
            if (gravity == true)
            {

                if (transform.position.y <= height2)
                {
                    transform.position = new Vector3(transform.position.x, height2, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - step, transform.position.z);
                }
            }
            else
            {

                if (transform.position.y >= height1)
                {
                    transform.position = new Vector3(transform.position.x, height1, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + step, transform.position.z);
                }
            }
        }
    }

    public void setGravity(bool settings)
    {
        gravity = settings;
    }

  

}
