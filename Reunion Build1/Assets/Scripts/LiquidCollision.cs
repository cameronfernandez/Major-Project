using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidCollision : MonoBehaviour {
    PlayerBehaviour player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<PlayerBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("hit");
        player.CalmDown();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Debug.Log("hitting wall");
        }
    }
}
