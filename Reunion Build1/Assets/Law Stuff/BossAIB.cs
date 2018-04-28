using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAIB : MonoBehaviour {

    GameObject player;
    public NavMeshAgent agent;
    float distToPlayer;
    PlayerBehaviour Damage;
    float CurrHP, maxHP, damage2HP;
    Animator animator;


	// Use this for initialization
	void Start ()
    {
        animator = this.gameObject.GetComponent<Animator>();
        maxHP = 100;
        CurrHP = maxHP;
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        Damage = player.GetComponent<PlayerBehaviour>();
    }
	  public enum State
    {
        chase,
        attack,
        idle,
        die,
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

            case State.die:
                Debug.Log("dieng");
                Die();
                break;

        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Controller")
        {
            CurrHP -= 20;
            animator.SetTrigger("stunntrigger");
        }
    }

    void chase()
    {
        animator.SetTrigger("running");
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
        if (CurrHP <= 0)
        {
            states = State.die;
        }

    }

    void attack()
    {
        animator.SetTrigger("attack");
       // Damage.IncreaseAnxiety();
        distToPlayer = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
        
        if (distToPlayer >= 2)
        {
            states = State.attack;
        }
        if (CurrHP <= 0)
        {
            states = State.die;
        }
    }

    void idle()
    {
        animator.SetTrigger("Idle");
        distToPlayer = Vector3.Distance(this.gameObject.transform.position, player.transform.position);

        if (distToPlayer <= 10)
        {
            states = State.idle;
        }

    }
    void Die()
    {
        animator.SetTrigger("die");
    }
  
}
