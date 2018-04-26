using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAIB : MonoBehaviour {

    GameObject player;
    public NavMeshAgent agent;
    float distToPlayer;
    PlayerBehaviour Damage;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        Damage = player.GetComponent<PlayerBehaviour>();
    }
	  public enum State
    {
        chase,
        attack,
        idle,
    }
    public static State states;
	// Update is called once per frame
	void Update ()
    {
        switch (states)
        {
            case State.idle:
                Debug.Log("idle");
                idle();
                break;

            case State.chase:
                Debug.Log("chasing");
                chase();
                break;

            case State.attack:
                Debug.Log("attacking");
                attack();
                break;
        }
		
	}

    void chase()
    {
        distToPlayer = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
        if (distToPlayer <= 10)
        {
            agent.SetDestination(player.transform.position);

        }
        if (distToPlayer <= 2)
        {
            states = State.attack;
        }
        if (distToPlayer >= 10)
        {
            states = State.idle;
        }

    }

    void attack()
    {
       // Damage.IncreaseAnxiety();
        distToPlayer = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
        
        if (distToPlayer >= 2)
        {
            states = State.attack;
        }

    }

    void idle()
    {
        distToPlayer = Vector3.Distance(this.gameObject.transform.position, player.transform.position);

        if (distToPlayer <= 10)
        {
            states = State.idle;
        }

    }
  
}
