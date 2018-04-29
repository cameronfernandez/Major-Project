using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPickupable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.tag == "controller")
    //    {
    //        other.gameObject.GetComponent<PickUp>().notInteracting = false;
    //        Debug.Log("collision");
    //        other.gameObject.GetComponent<Collider>().isTrigger = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{

    //    if (other.CompareTag("controller"))
    //    {
    //        other.gameObject.GetComponent<PickUp>().notInteracting = true;
    //    }

    //}
}
