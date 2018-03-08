using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {


    public float anxiety;
    public float composure; 


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	                
        
        if (anxiety >= 100)
        {
            //reduce composure
        }

        if (composure <= 0)
        {
            // gameover

        }

        

	}

    void IncreaseAnxiety()
    {
        //increase anxiety according to proximity to enemy

    }

}
