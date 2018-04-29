using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class BystanderBehaviour : MonoBehaviour {


    NavMeshAgent agent;
    public static bool alarmTriggered = false;
    GameObject player;
    // Use this for initialization
    void Start() {

        player = GameObject.FindGameObjectWithTag("Player");
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;

    }

    // Update is called once per frame
    void Update() {
        if (alarmTriggered == true)
        transform.LookAt(player.transform);
    }

    public void WalkTowards(Transform destination)
   {
        this.gameObject.GetComponent<NavMeshAgent>().enabled = true;

        
        agent.destination = destination.position;
       
   }
    
}
