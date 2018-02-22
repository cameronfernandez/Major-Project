using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {


    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    private SteamVR_TrackedObject trackedObj;

    public GameObject obj;
    public FixedJoint fJoint;


    private Rigidbody rigidbody;
    bool isThrowing;
	// Use this for initialization
	void Start () {


        trackedObj = GetComponent<SteamVR_TrackedObject>();
        fJoint = GetComponent<FixedJoint>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if (controller == null)
        {
            Debug.Log("Controller not connected");
            return;
                
        }

        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (controller.GetPress(triggerButton))
        {
            print("pressed trigger");
            PickUpObj();
        }

        if (controller.GetPressUp(triggerButton))
        {
            //Drop
            DropObj();
        }
	}


     void FixedUpdate()
    {
        
       if (isThrowing)

        {
            Transform origin;
            if(trackedObj.origin != null)
            {
                origin = trackedObj.origin;
               
            }

            else
            {
                origin = trackedObj.transform.parent;
            }

            if (origin != null)
            {
                rigidbody.velocity = origin.TransformVector(controller.velocity);
                rigidbody.angularVelocity = origin.TransformVector(controller.angularVelocity * 0.25f);

            }

            else
            {
                rigidbody.velocity = controller.velocity;
                rigidbody.angularVelocity = controller.angularVelocity * 0.25f;
            }

            rigidbody.maxAngularVelocity = GetComponent<Rigidbody>().angularVelocity.magnitude;

            isThrowing = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "canPickUp")
        {
            Debug.Log("trigger");
            obj = other.gameObject;
        }
    }


    void PickUpObj()
    {
        if (obj != null)
        {
            fJoint.connectedBody = obj.GetComponent<Rigidbody>();
            isThrowing = false;

            rigidbody = null;
        }

        else
        {
            fJoint.connectedBody = null;
        }
    }

    void DropObj()
    {
        if (fJoint.connectedBody != null)
        {

            rigidbody = fJoint.connectedBody;
            fJoint.connectedBody = null;
            isThrowing = true;
        }
    }
    

}
