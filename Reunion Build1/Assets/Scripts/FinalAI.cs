using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FinalAI : MonoBehaviour {


    PlayerBehaviour player;
    public float attackingDistance;
    public float anxietyDistance;
    NavMeshAgent agent;
    Doorcontrol[] doors;
    public int enemyHitpoints = 3;
    bool doorCheck = false;
    bool isHit = false;
    public float anxietyDrainInterval;
    public float maxInterval;
    public int enemiesKilled;

    GameManager gameManager;

    // Use this for initialization
    void Start () {

        agent = this.gameObject.GetComponent<NavMeshAgent>();
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        doors = GameObject.FindObjectsOfType<Doorcontrol>();
        player = GameObject.FindObjectOfType<PlayerBehaviour>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {

       

        if (Vector3.Distance(this.transform.position, player.transform.position) <= attackingDistance)
        {
            if (this.gameObject.GetComponent<NavMeshAgent>().enabled == false)
            {
                this.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            }

            if (Vector3.Distance(this.transform.position, player.transform.position) <= anxietyDistance && isHit == false)
            {
                anxietyDrainInterval -= Time.deltaTime;

                if (anxietyDrainInterval <= 0)
                {
                    player.GetComponent<PlayerBehaviour>().IncreaseAnxiety();
                    anxietyDrainInterval = maxInterval;
                }
            }

            this.gameObject.GetComponent<Animator>().SetTrigger("WalkTrigger");

            agent.destination = player.transform.position;

            if (doorCheck == false)
            { 
                foreach (Doorcontrol door in doors)
                {
                    door.Open = false;
                    

                }
                doorCheck = true;
            }

           
            if(enemiesKilled >= 2)
            {
                gameManager.bossBeaten = true;
            }


        }

        else
        {
            transform.LookAt(player.transform);
        }

        if(isHit == false && agent.isActiveAndEnabled == true)
        {
            if (enemyHitpoints <= 0)
            {
                agent.isStopped = true;
                isHit = true;
            }

            agent.isStopped = false;
        } 

      
        
	}



    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "controller" || collider.gameObject.tag == "canPickUp")
        {
            isHit = true;
            Debug.Log("hit");
            agent.isStopped = true;
            isHit = false;

            gameObject.GetComponent<Animator>().SetTrigger("GetHitTrigger");
            enemyHitpoints--; 


            if (enemyHitpoints <= 0)
            {
                gameObject.GetComponent<Animator>().SetTrigger("DieTrigger");

                isHit = true;
                gameManager.enemiesKilled++;
            }

            

        }

        
    }
}
