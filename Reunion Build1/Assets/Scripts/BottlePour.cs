using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePour : MonoBehaviour {
    public ParticleSystem liquid;
	// Use this for initialization
	void Start () {

        Debug.Log(transform.eulerAngles);
	}
	
	// Update is called once per frame
	void Update () {

       if (transform.localEulerAngles.z > 98 || transform.localEulerAngles.z < -98|| transform.localEulerAngles.x > 98 || transform.localEulerAngles.x < -98)
        {
            Debug.Log("pouring");
            transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
            liquid.Play();
                
        }

       
        
	}
}
