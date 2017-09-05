using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float fireRate = 20;
    private double lightofftime;
    private float lightoff;
    public Camera myCamera;
    private float nextFire;
    public AudioSource soundeffect;
    public GameObject HUD;
    public int bullets;
    public GameObject lightsource;
    public int damage;

    private void Start()
    {
        lightsource.SetActive(false);
    }
    
	void Update ()
    {
        float time = Time.time;

        if (time > lightofftime)
        {
            lightsource.SetActive(false);
        }

        if ((Input.GetMouseButtonDown(0)) && (time > nextFire) && (bullets > 0))
        {
            
            Debug.Log(bullets);
            HUD.GetComponent<HUD>().shoot();
            bullets = bullets - 1;

            lightsource.SetActive(true);

            lightofftime = Time.time + lightofftime;

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
                    myObject.Hit(damage);
                   // Debug.DrawLine(transform.position, new Vector3(0.5f, 0.5f, 0), Color.red, 10f, true);
                }

             
            }
            else
            {
                Debug.Log("No Hit");
            }

            nextFire = Time.time + fireRate;
            lightofftime = Time.time + 0.1 ;
        }



    }
}
