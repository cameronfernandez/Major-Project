using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public enum GamePhases { SEARCH_CLASSROOM, SEARCH_AUDITORIUM, LEAVE_AUDITORIUM, BULLY, FINAL };
    public GamePhases gamePhase;
    // Use this for initialization
    void Start() {


        gamePhase = GamePhases.SEARCH_CLASSROOM;

    }

    // Update is called once per frame
    void Update() {
        
    
        switch(gamePhase)
        {
            case GamePhases.SEARCH_CLASSROOM:
                Debug.Log("search the classrooms for your friend");
            break;
        }

    }

    

}
