
using UnityEngine;

public class DDOL : MonoBehaviour {
    //stops network from vbeing destroyed when changing fro menu to scene
	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this);		
	}
	
}
