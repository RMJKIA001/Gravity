using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour , IAI{
    public float speed;
    public float travelled = 0;
    public int distance;
    public GameObject AI;
    public GameObject player;
    bool right = true;
    bool turn = false;
    private int step = 1;

    bool IsAttacking  = false;
    public GameObject Bullet ;
    //public Transform SpawnPoint;
    float fireRate = 7f;
    float nextFire = 0;
    public float bulletTime = 50f;
    public float disableBullet = 0;

    public void Attack()
    {
         System.Random rnd = new System.Random();
         if (Time.time > nextFire && IsAttacking)
         {
             Debug.Log("Attack");
             AI.transform.LookAt(player.transform);
             nextFire = Time.time + fireRate;
             disableBullet = Time.time + bulletTime;
             Bullet.transform.position = player.transform.position  +  new Vector3(rnd.Next(1),rnd.Next(1),rnd.Next(1));
             if (AI.transform.position.z > 50)
             {
                AI.transform.rotation = new Quaternion(0, 180, 0, 0);
             }


             if (AI.transform.position.z < 38)
             {
                 AI.transform.rotation = new Quaternion(0, 0, 0, 0);
             }

        }
    
    }

    public void Roam()
    {
        Vector3 otherPosn = AI.transform.position;

        if (right)
        {
            AI.transform.position = new Vector3(otherPosn.x, otherPosn.y, otherPosn.z + speed);
            travelled = travelled + speed;
        }
        else
        {
            AI.transform.position = new Vector3(otherPosn.x, otherPosn.y, otherPosn.z - speed);
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

    //not for prototype
    public void Spawn()
    {
        throw new NotImplementedException();
    }
    //not for prototype
    public void Flee()
    {
        throw new NotImplementedException();
    }
    
	
	// Update is called once per frame
	void Update () {
        
        Roam();
        Hunt();
        Attack();
        
    }

    void Hunt()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 AIPos = AI.transform.position;
        Vector3 distanceBetweenThem = AIPos - playerPos;//new Vector3(2, 2, 2);
        if ((distanceBetweenThem.x <5 && distanceBetweenThem.x >-5) && (distanceBetweenThem.z<5 && distanceBetweenThem.z>-5)  )
        {
            IsAttacking = true;
        }
        else
        {
            IsAttacking = false;
        }
    }
}
