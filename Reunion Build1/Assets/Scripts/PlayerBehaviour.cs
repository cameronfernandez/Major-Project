using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour {


    public float anxiety;
    public float composure;

    public float anxietyIncrement;
    public float composureDecrement;
    public float personalSpace;

    public float composureDrainInterval;
    public float increaseAnxietyInterval;

    public bool isInToilet;
    public bool isInAuditorium;

    float _originalAnxietyInt;
    float _originalCompInt;
    float _maxAnxiety = 100;
    public GameObject enemy;
    GameObject player;


    public AudioSource heartBeat;
    public AudioClip heatBeatClip;
  
	// Use this for initialization
	void Start () {
        player = this.gameObject; 
        anxiety = 0;
        composure = 100;
        isInToilet = false;
        isInAuditorium = false;


        _originalAnxietyInt = increaseAnxietyInterval;
        _originalCompInt = composureDrainInterval;
	}
	
	// Update is called once per frame
	void Update () {

        if (anxiety > 25 && anxiety < 50)
        {
            heartBeat.pitch = 1.5f;
        }

        if (anxiety > 50 && anxiety < 75)

        {
            heartBeat.pitch = 2.0f;
        }

        if (anxiety > 75)
        {
            heartBeat.pitch = 2.5f;
        }

        if (anxiety >= _maxAnxiety)
        {
            anxiety = _maxAnxiety;
            composureDrainInterval -= Time.deltaTime;

            if (composureDrainInterval <= 0)
            {
                composure -= composureDecrement;
                composureDrainInterval = _originalCompInt;
            }
            
        }

        if (composure <= 0)
        {
            Debug.Log("game over");
            
        }

        float distToEnemy = Vector3.Distance(player.transform.position, enemy.transform.position);

        if (distToEnemy < personalSpace)
        {
            increaseAnxietyInterval -= Time.deltaTime; 
            if (increaseAnxietyInterval <= 0 )
            {
                IncreaseAnxiety();
                increaseAnxietyInterval = _originalAnxietyInt;

            }
        }

        if (isInToilet == true)
        {
            Debug.Log("in toilet");
        }
	}

    void IncreaseAnxiety()
    {
        anxiety += anxietyIncrement;
    }

    public void CalmDown()
    {
        anxiety = 0;
        composure = 0; 

    }


    
}
