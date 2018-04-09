using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuditoriumCheck : MonoBehaviour {
    public PlayerBehaviour playerBehaviour;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerBehaviour.isInAuditorium = true;
        }
    }
}
