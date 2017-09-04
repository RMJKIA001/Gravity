using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
	public void Hit ()
    {
        gameObject.SetActive(false);
    }
}
