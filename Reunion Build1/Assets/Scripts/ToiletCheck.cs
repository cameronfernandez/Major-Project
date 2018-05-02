using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletCheck : MonoBehaviour {


    public PlayerBehaviour playerBehaviour;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerBehaviour.isInToilet = true;
            Debug.Log("inside");
        }
    }

}
