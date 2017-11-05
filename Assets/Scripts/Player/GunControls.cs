using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControls : MonoBehaviour {

    public GameObject gun1;
    public GameObject gun2;
    public GameObject HUD;
    public GameObject active;
    public GameObject gunText;
    public bool shotGun;
    public bool lazerGun;
    public bool moreThanOne;
    // Use this for initialization
    void Start ()
    {
        gun1.SetActive(false);
        gun2.SetActive(false);
    }
	public void enableGun()
    {
        gunText.SetActive(true);
    }
	// Update is called once per frame
	void Update ()
    {
        if(moreThanOne)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                if (gun1.activeInHierarchy == true)
                {
                    gun1.SetActive(false);
                    gun2.SetActive(true);
                    active = gun2;
                }
                else
                {
                    gun1.SetActive(true);
                    gun2.SetActive(false);
                    active = gun1;
                }

            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                if (gun1.activeInHierarchy == true)
                {
                    gun1.SetActive(false);
                    gun2.SetActive(true);
                    active = gun2;
                }
                else
                {
                    gun1.SetActive(true);
                    gun2.SetActive(false);
                    active = gun1;
                }
            }
        }
        else
        {
            if(shotGun)
            {
                gun1.SetActive(true);
                gun2.SetActive(false);
                //GetComponent<NetworkPlayer>().shot.SetActive(true);
                active = gun1;
            }
            else if(lazerGun)
            {
                gun1.SetActive(false);
                gun2.SetActive(true);
                active = gun2;
            }
        }
        if(shotGun || lazerGun)
        {
            Shoot curr = active.GetComponent<Shoot>();
            HUD.GetComponent<HUD>().create(curr.type, curr.bullets, curr.maxBul);
        }
        else
        {
            HUD.SetActive(false);
        }

    }
}
