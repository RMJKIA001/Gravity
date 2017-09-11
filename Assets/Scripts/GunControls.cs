using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControls : MonoBehaviour {

    public GameObject gun1;
    public GameObject gun2;
    public GameObject HUD;
    public GameObject active;

    // Use this for initialization
    void Start ()
    {
        HUD.GetComponent<HUD>().create("Shotgun", 10, 10);
        gun2.SetActive(true);
        active = gun1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if (gun1.activeInHierarchy == true)
            {
                gun1.SetActive(false);
                gun2.SetActive(true);
                active = gun2;

                //HUD.GetComponent<HUD>().create("Laser Gun", gun2.GetComponent<Shoot>().bullets, 20);
            }
            else
            {
                gun1.SetActive(true);
                gun2.SetActive(false);
                active = gun1;
                //HUD.GetComponent<HUD>().updateGun("Shotgun", gun1.GetComponent<Shoot>().bullets, 10);
            }
            
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (gun1.activeInHierarchy == true)
            {
                gun1.SetActive(false);
                gun2.SetActive(true);
                active = gun2;
                //HUD.GetComponent<HUD>().updateGun("Laser Gun", gun2.GetComponent<Shoot>().bullets, 20);

            }
            else
            {
                gun1.SetActive(true);
                gun2.SetActive(false);
                active = gun1;
                //HUD.GetComponent<HUD>().updateGun("Shotgun", gun1.GetComponent<Shoot>().bullets, 10);
            }
        }
        Shoot curr = active.GetComponent<Shoot>();
        HUD.GetComponent<HUD>().create(curr.type, curr.bullets, curr.maxBul);

    }
}
