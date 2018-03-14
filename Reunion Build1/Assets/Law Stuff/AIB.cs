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

    float distToPlayer;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        //agent.autoBraking = false;
       
       // pointToReach = GameObject.Find("EndPoint");
        currtarget = Nodes[0];
        AgentMoveToNode();


    }
	
	// Update is called once per frame
	void Update ()
    {
          Invoke("moveBoi", 2);
         // Invoke("DistCheck", 3);

        //Debug.Log("agent" + agent.transform.position);
       // Debug.Log("current:" + currtarget.position);
        
        //agent.SetDestination(Nodes[nodeNum].transform.position);
       // agent.SetDestination(currtarget.transform.position);
    }

    public void moveBoi()
    {
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
    }
    public void DistCheck()
    {
        distToPlayer = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
        if (distToPlayer <= 3)
        {
            Debug.Log("ATTAACKKK");
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
