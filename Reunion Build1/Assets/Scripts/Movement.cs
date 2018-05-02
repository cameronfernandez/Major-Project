﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public Transform rig;

    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    private SteamVR_TrackedObject trackedObj;

    private Vector2 axis = new Vector2(0, 3.07f);

    public Transform head;

    Rigidbody rb;

    float speed;
	// Use this for initialization
	void Start () {
        speed = 1;

        trackedObj = GetComponent<SteamVR_TrackedObject>();
        rb = rig.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		

        if(controller == null)
        {
            Debug.Log("Controller not initialized");
            return; 
        }

        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (controller.GetTouch(touchpad))
        {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);

            if(rig!=null)
            {

                Vector3 move = axis.x * head.right + axis.y * head.forward;

                Vector3.ClampMagnitude(move, 4f);
                rb.velocity = move * speed;
                //move.Normalize();
                //rb.AddForce(move * 4f);

                

               
                //rig.position += (head.right * axis.x + head.forward * axis.y) * Time.deltaTime * speed;
                //rig.position = new Vector3(rig.position.x, 0, rig.position.z);

            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        if (controller.GetPress(touchpad)) speed = 3;
        else
        {
            speed = 1.5f;
        }
	}
}
