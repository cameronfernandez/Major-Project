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
    public PlayerBehaviour player;
    public GameManager gameManager;

    public GameObject guide1;
    public GameObject guide2;

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
        guide1.SetActive(false);
        guide2.SetActive(false);
        player = GameObject.FindObjectOfType<PlayerBehaviour>();
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



    void LateUpdate ()
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
        if (player.GetComponent<PlayerBehaviour>().isInToilet == true)
        {

            GetComponent<Animator>().SetTrigger("IdleTrigger");

            Debug.Log("counting");
            _timeTillAbandon -= Time.deltaTime;
            if (_timeTillAbandon <= 0)
            {

                if (gameObject.tag == "FirstAI")
                {
                    GameManager.gamePhase = GameManager.GamePhases.SEARCH_AUDITORIUM;
                    gameObject.SetActive(false);
                    guide1.SetActive(false);
                    guide2.SetActive(true);
                }

                if (gameObject.tag == "SecondAI")
                {
                    GameManager.gamePhase = GameManager.GamePhases.BULLY;
                }
            }
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
        distToPlayer = Vector3.Distance(this.gameObject.transform.position, player.gameObject.transform.position);
        if (distToPlayer <= 6)
        {
            states = State.Chase;
			agent.SetDestination(player.transform.position);
            if (gameObject.tag == "FirstAI")
            {
                guide1.SetActive(true);
            }
        }
    }


    public void DistCheck()
    {
        //Debug.Log("DISTCHECK");


        //if (distToPlayer <= 6)
        //{
        //    Debug.Log("enter");
            agent.SetDestination(player.transform.position);
            //if (playAnimation == false)
           // {
                Debug.Log("should chase");
        agent.GetComponent<NavMeshAgent>().speed = 3.5f;

        GetComponent<Animator>().SetTrigger("ChaseTrigger");
              //  playAnimation = true;
           // }
        //}
		distToPlayer = Vector3.Distance(this.gameObject.transform.position, player.gameObject.transform.position);

        if (distToPlayer >= 10)
        {
            playAnimation = false;
            guide1.SetActive(false);
            if (states == State.Idle) return;

            //else
           // {
                states = State.Wander;
            //}
            
        }

        //if (player.GetComponent<PlayerBehaviour>().isInToilet == true)
        //{

        //    GetComponent<Animator>().SetTrigger("IdleTrigger");

        //    Debug.Log("counting");
        //    _timeTillAbandon -= Time.deltaTime;
        //    if (_timeTillAbandon <= 0)
        //    {

        //        if (gameObject.tag == "FirstAI")
        //        {
        //            GameManager.gamePhase = GameManager.GamePhases.SEARCH_AUDITORIUM;
        //            gameObject.SetActive(false);
        //            guide1.SetActive(false);
        //            guide2.SetActive(true);
        //        }

        //        if (gameObject.tag == "SecondAI")
        //        {
        //            GameManager.gamePhase = GameManager.GamePhases.BULLY;
        //        }
        //    }
        //}
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
