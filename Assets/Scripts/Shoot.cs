using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float fireRate = 20;
    private float lightofftime = (float) 0.2;
    public Camera myCamera;
    private float nextFire;
    private float lightoff;
    public AudioSource soundeffect;
    public GameObject HUD;
    public int bullets;

    private void Start()
    {
      
    }
    
	void Update ()
    {
        float time = Time.time;

        if ((Input.GetMouseButtonDown(0)) && (time > nextFire) && (bullets > 0))
        {
            Debug.Log(bullets);
            HUD.GetComponent<HUD>().shoot();
            bullets = bullets - 1;

            lightoff = Time.time + lightofftime;

            Vector3 crosshair = myCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));



            soundeffect.Play();
            RaycastHit hit;

            if (Physics.Raycast(crosshair, myCamera.transform.forward, out hit, 500))
            {
                Debug.Log("Hit");
                nextFire = Time.time + fireRate;
            
                if (hit.collider.GetComponent<Shootable>() != null)
                {
                    Shootable myObject = hit.collider.GetComponent<Shootable>();
                    myObject.Hit();
                    Debug.DrawLine(transform.position, new Vector3(0.5f, 0.5f, 0), Color.red, 10f, true);
                }

             
            }
            else
            {
                Debug.Log("No Hit");
            }
            nextFire = Time.time + fireRate;
        }



    }
}
