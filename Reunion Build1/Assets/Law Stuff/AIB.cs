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
    GameObject player;

    float distToPlayer;

    public enum State
    {
        Wander,
        Chase,
    }
    public State states;
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        states = State.Wander;
        //agent.autoBraking = false;
       
       // pointToReach = GameObject.Find("EndPoint");
        currtarget = Nodes[0];
        AgentMoveToNode();
        

    }

 
    // Update is called once per frame

   

	void Update ()
    {
      switch (states)
        {
            case State.Wander:
                Debug.Log("wander");
                moveBoi();
                break;

            case State.Chase:
                Debug.Log("chase");
                DistCheck();
                break;
        }
    }

    public void moveBoi()
    {
        AgentMoveToNode();

        if (agent.transform.position.x == currtarget.position.x && agent.transform.position.z == currtarget.position.z)
        {
            
            if (nodeNum < Nodes.Length - 1)
            {
                nodeNum++;
                agent.SetDestination(Nodes[nodeNum].transform.position);
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
        }
        if (distToPlayer >= 6)
        {
            states = State.Wander;
            
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
}
