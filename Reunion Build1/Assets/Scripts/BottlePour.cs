using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePour : MonoBehaviour
{
    public ParticleSystem liquid;
    public SteamVR_Controller.Device controller;
    Valve.VR.EVRButtonId touchPad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    public PlayerBehaviour playerBehaviour;
    float bottleAngle;


    // Use this for initialization
    void Start()
    {

        Debug.Log(transform.eulerAngles);
    }

    // Update is called once per frame
    void Update()
    {

     
            Debug.Log("pouring");
            bottleAngle = Vector3.Angle(Vector3.up, transform.up);
        if (bottleAngle < 95)
        {
            transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
            liquid.Play();
        }


    }

}
