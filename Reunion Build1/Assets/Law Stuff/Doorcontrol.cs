using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorcontrol : MonoBehaviour {

    //creating bool for door state change, setting it false so doors are closed on start
    public bool Open = false;
    public float DoorOpen = 90f;
    public float DoorClosed = 0f;
    public float OpenCloseSpeed = 1.0f;

    // Use this for initialization
    void Start()
    {

    }

    public void ChangeDoorState()
    {
        //stating that open is the opposite of not open, inverting the booling when the function is called
        Open = !Open;
    }

    // Update is called once per frame
    void Update()
    {
        //check if open == tue
        if (Open)
        {
            Quaternion DoorRotation = Quaternion.Euler(0, 0, DoorOpen);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, DoorRotation, OpenCloseSpeed);
        }
        else
        {
            Quaternion DoorRotation2 = Quaternion.Euler(0, 0, DoorClosed);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, DoorRotation2, OpenCloseSpeed);
        }
    }
}