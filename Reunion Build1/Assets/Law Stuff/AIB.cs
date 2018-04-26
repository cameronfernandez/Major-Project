using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIB : MonoBehaviour {


    public Transform[] Nodes;
    private Transform currtarget;
    public NavMeshAgent agent;
    int nodeNum = 0;
    int s = 5;
    GameObject pointToReach;
    public GameObject player;
    public GameManager gameManager;

    float distToPlayer;
    float _distractedTime = 4;

    bool playAnimation = false;

    [SerializeField]
    float _timeTillAbandon = 4;
    public enum State
    {
        Wander,
        Chase,
        Distracted,
        Idle,

    }
    public static State states;
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        if (gameObject.tag == "FirstAI")
        {
            GetComponent<Animator>().SetTrigger("WalkTrigger");
            states = State.Wander;

        }

        currtarget = Nodes[0];

        //agent.autoBraking = false;

        // pointToReach = GameObject.Find("EndPoint");


    }


    private void OnEnable()
    {
        if (gameObject.tag == "SecondAI")
        {
            Debug.Log("entered");
          GetComponent<Animator>().SetTrigger("WalkTrigger");

            states = State.Wander;
        }
    }

    // Update is called once per frame



    void Update ()
    {

      switch (states)
        {

            case State.Idle:
                Debug.Log("doing nothing");
                DistCheck();
                break;

            case State.Wander:
                Debug.Log("wander");
                moveBoi();
                break;

            case State.Chase:
                Debug.Log("chase");
                DistCheck();
                break;

            case State.Distracted:
                IsDistracted();
                Debug.Log("distracted");
                break;

        }
    }

    public void moveBoi()
    {
        AgentMoveToNode();

        if (agent.transform.position.x == currtarget.position.x && agent.transform.position.z == currtarget.position.z)
        {
            Debug.Log("same position");
            if (nodeNum < Nodes.Length - 1)
            {
                nodeNum++;
                Debug.Log("doing something");
                
                if (nodeNum == Nodes.Length - 1 )
                {
                    Debug.Log("yeet");
                    nodeNum = 0;
                    currtarget = Nodes[nodeNum];
                    AgentMoveToNode();
                    return;
                    
                }
                currtarget = Nodes[nodeNum];
                agent.SetDestination(Nodes[nodeNum].transform.position);

            }

        }
        distToPlayer = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
        if (distToPlayer <= 6)
        {
            states = State.Chase;
        }
    }


    public void DistCheck()
    {
        distToPlayer = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
        if (distToPlayer <= 6)
        {
            agent.SetDestination(player.transform.position);
            if (playAnimation == false)
            {
                GetComponent<Animator>().SetTrigger("ChaseTrigger");
                GetComponent<NavMeshAgent>().speed = 4;
                playAnimation = true;
            }
        }
        if (distToPlayer >= 10)
        {
            playAnimation = false;
            if (states == State.Idle) return;

            else
            {
                states = State.Wander;
            }
            
        }

        if (player.GetComponent<PlayerBehaviour>().isInToilet == true)
        {

            GetComponent<Animator>().SetTrigger("IdleTrigger");


            _timeTillAbandon -= Time.deltaTime;
            if (_timeTillAbandon <= 0)
            {

                if (gameObject.tag == "FirstAI")
                {
                    GameManager.gamePhase = GameManager.GamePhases.SEARCH_AUDITORIUM;
                    gameObject.SetActive(false);
                }

                if (gameObject.tag == "SecondAI")
                {
                    GameManager.gamePhase = GameManager.GamePhases.BULLY;
                }
            }
        }
    }

    private void AgentMoveToNode()
    {
        //  if (agent.transform.position.x == currtarget.position.x && agent.transform.position.z == currtarget.position.z )
        if (agent.transform.position != currtarget.position)
        {
            agent.SetDestination(currtarget.position);
        }
    }

    public void NewPos(int nodePos)
    {
        currtarget = Nodes[nodePos];
    }

    public void IsDistracted()
    {
        _distractedTime -= Time.deltaTime;

        if(_distractedTime <= 0)
        {
            GetComponent<Animator>().SetTrigger("DistractedTrigger");
            states = State.Chase;
        }
    }
}
