using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "MainCamera")
        {
            Debug.Log("hit");
        }
    }
}
