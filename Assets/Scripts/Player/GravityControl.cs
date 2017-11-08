using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    //variables
    public int height1;
    public int height2;
    public float step;
    public bool up;
    bool gravity = false;
    
     //set the height changes for platforms
    public void setHeight(int a,int b)
    {
        height1 = a;
        height2 = b;
    }

    // moves platforms between height1 and height2 when the user manipulates gravity.
    //either platform must move up or down
    //uses bool to do this
    void Update()
    {
        //platform moves up
        if (up)
        {
            if (gravity == true)
            {
                //slow move between start and end height
                //update accordingly
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
        //platform moves down
        else
        {
            //slow move between start and end height
            //update accordingly
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

    //set gravity
    //this determines if gravity activated or not
    //so platform can move back and forth.
    public void setGravity(bool settings)
    {
        gravity = settings;
    }

  

}
