using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    private int bullets;
    private int tBullets;
    private string gunName;

	void Start ()
    {
        bullets = 20;	
	}

    public void updateGun(string name, int x, int totalBullets)
    {
        bullets = x;
        tBullets = totalBullets;
        gunName = name;

    }

    public void shoot()
    {
        bullets = bullets - 1;
    }
    // Update is called once per frame
    void Update ()
    {
        GetComponent<Text>().text = gunName + ": "  + bullets + "/" + tBullets ;
    }
}
