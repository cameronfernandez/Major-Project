using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour {


    public GameObject liquidCourage;
    public GameObject vendingMachineSpawn;
	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("button"))
        {
            GameObject bottle = Instantiate(liquidCourage, vendingMachineSpawn.transform.position, Quaternion.identity);
        }
    }
}
