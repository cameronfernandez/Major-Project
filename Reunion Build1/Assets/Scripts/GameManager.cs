using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public enum GamePhases { SEARCH_CLASSROOM, SEARCH_AUDITORIUM, LEAVE_AUDITORIUM, BULLY, FINAL };
    public static GamePhases gamePhase;
    public AIB FirstAI;
    public AIB SecondAI;
    public Material mat;

    // Use this for initialization
    void Start() {


        gamePhase = GamePhases.SEARCH_CLASSROOM;
        FirstAI.gameObject.SetActive(true);
        SecondAI.gameObject.SetActive(false);
        Renderer[] renders = GameObject.FindObjectsOfType<Renderer>();

        for (int i = 0; i < renders.Length; i++)
        {
            renders[i].material = mat;
        }

    }

    // Update is called once per frame
    void Update() {
        
    
        switch(gamePhase)
        {
            case GamePhases.SEARCH_CLASSROOM:
                Debug.Log("search the classrooms for your friend");
            break;

            case GamePhases.SEARCH_AUDITORIUM:
                SecondAI.gameObject.SetActive(true);
                Debug.Log("make your way to the auditorium");
            break;
        }

    }

    

}
