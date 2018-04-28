﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public enum GamePhases { SEARCH_CLASSROOM, SEARCH_AUDITORIUM, LEAVE_AUDITORIUM, BULLY, FINAL };
    public static GamePhases gamePhase;
    public AIB FirstAI;
    public AIB SecondAI;
    public Material mat;
    public Material flrMat;
    public Material doorMat;
    public Material deathMat;
    // Use this for initialization
    void Start() {


        gamePhase = GamePhases.SEARCH_CLASSROOM;
        FirstAI.gameObject.SetActive(true);
        SecondAI.gameObject.SetActive(false);
        Renderer[] renders = GameObject.FindObjectsOfType<Renderer>();
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
        GameObject[] vendingMachines = GameObject.FindGameObjectsWithTag("VendingMachine");
        GameObject deathScreen = GameObject.FindGameObjectWithTag("DeathScreen");
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");

        for (int i = 0; i < renders.Length; i++)
        {
            renders[i].material = mat;
        }

        for (int i = 0; i < vendingMachines.Length; i++)
        {
            vendingMachines[i].GetComponent<Renderer>().material = doorMat;
        }

        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].AddComponent<BoxCollider>();
        }
        Debug.Log("hello" + deathScreen.name);

        GameObject floor = GameObject.FindGameObjectWithTag("Floor");

        floor.GetComponent<Renderer>().material = flrMat;
        deathScreen.GetComponent<Renderer>().material = deathMat;
        
        

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
