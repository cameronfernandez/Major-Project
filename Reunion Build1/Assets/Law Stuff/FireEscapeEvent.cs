using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FireEscapeEvent : MonoBehaviour {

    public GameObject player;
    public AudioSource AKTUMALARMEH;
    public Text runTEXT, exitTEXT, overHereTEXT;
    

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void TriggerAlarm()
    {

        AKTUMALARMEH.Play();
        Debug.Log("collided");
        GameObject.FindGameObjectWithTag("enterDoor").GetComponent<Doorcontrol>().ChangeDoorState();
        GameObject.FindGameObjectWithTag("exitDoor").GetComponent<Doorcontrol>().ChangeDoorState();
        exitTEXT.gameObject.SetActive(false);
        runTEXT.gameObject.SetActive(true);
        overHereTEXT.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
