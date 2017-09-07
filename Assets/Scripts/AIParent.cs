using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIParent : MonoBehaviour
{
    public float speed;
    public float travelled = 0; 
    public int distance;
    public GameObject AI;
    bool right = true;
    bool turn = false;
    private int step = 1;
    // Use this for initialization
    void Start ()
    {

    }
	
    void Roam()
    {
        Vector3 otherPosn = AI.transform.position;

        if (right)
        {
            AI.transform.position = new Vector3(otherPosn.x, otherPosn.y, otherPosn.z + speed);
            travelled = travelled + speed;
        }
        else
        {
            AI.transform.position = new Vector3(otherPosn.x, otherPosn.y, otherPosn.z -speed);
            travelled = travelled - speed;
        }

        
        if (AI.transform.position.z > 50)
        {
            right = false;
            AI.transform.rotation = new Quaternion(0, 180, 0, 0);
            Step();
        }


        if (AI.transform.position.z < 38)
        {
            right = true;
            AI.transform.rotation = new Quaternion(0, 0, 0, 0);
            Step();
        }




    }
    void Step()
    {
        Vector3 otherPosn = AI.transform.position;
        if (turn)
        {
            AI.transform.position = new Vector3(otherPosn.x + step, otherPosn.y, otherPosn.z);
        }
        else
        {
            AI.transform.position = new Vector3(otherPosn.x - step, otherPosn.y, otherPosn.z);
        }

        if (AI.transform.position.x > 70)
        {
            turn = false;
        }
        if (AI.transform.position.x < 50)
        {
            turn = true;
        }
    }


	// Update is called once per frame
	void FixedUpdate ()
    {
        Roam();
	}
}
