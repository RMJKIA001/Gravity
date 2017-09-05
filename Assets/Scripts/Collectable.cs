using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public int value;
    public string type;
    public GameObject HUD;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            bool possible = HUD.GetComponent<HUD>().increase(value, type);
            if (possible)
            {
                Debug.Log("Possile");
                gameObject.SetActive(false);
                if (type == "Health" || type == "Armor")
                {
                    player.GetComponent<PlayerHealth>().increase(value,type);
                }
                else
                {
                    player.GetComponent<GunControls>().active.GetComponent<Shoot>().bullets = player.GetComponent<GunControls>().active.GetComponent<Shoot>().maxBul;
                }
            }
        }
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject + " with "+collision.gameObject);
        if(collision.gameObject.tag == "Player")
        {
            bool possible = HUD.GetComponent<HUD>().increase(value, type);
            if (possible)
            {
                Debug.Log("Possile");
                gameObject.SetActive(false);
                if(type == "Health" || type == "Armor")
                {
                    player.GetComponent<PlayerHealth>().increase(value);
                }
                else
                {
                    player.GetComponent<GunControls>().active.GetComponent<Shoot>().bullets = player.GetComponent<GunControls>().active.GetComponent<Shoot>().maxBul;
                }
            }
        }
        

    }*/
}
