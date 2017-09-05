using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    private int totalhealth = 100;
    private int currenthealth;
    public  GameObject healthbar;

    private void Start()
    {
        currenthealth = 100;
        healthbar.GetComponent<TextMesh>().text = currenthealth + "/" + totalhealth;
    }

    public void Hit (int damage)
    {
        currenthealth = currenthealth - damage;

        if (currenthealth <= 0 )
        {
            gameObject.SetActive(false);
            healthbar.SetActive(false);
        }

        healthbar.GetComponent<TextMesh>().text = currenthealth + "/" + totalhealth;
    }
}
