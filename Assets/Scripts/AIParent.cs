using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        }


        if (AI.transform.position.z < 38)
        {
            right = true;
            AI.transform.rotation = new Quaternion(0, 0, 0, 0);
        }




    }

    public void Attack()
    {

    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        Roam();
	}
}
