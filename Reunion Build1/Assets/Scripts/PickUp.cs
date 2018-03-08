using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {


    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    private SteamVR_TrackedObject trackedObj;

    public GameObject obj;
    public FixedJoint fJoint;
    public bool notInteracting;

    private Rigidbody rigidbody;
    bool isThrowing;
	// Use this for initialization
	void Start () {

        notInteracting = true;
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

        if(controller.GetPressDown(triggerButton))
        {
            if (obj != null)
            {
                obj.transform.rotation = Quaternion.Euler(0, 0, 0);

            }

           
        }

        if (controller.GetPressUp(triggerButton))
        {
            //Drop
            DropObj();
            GetComponent<Collider>().isTrigger = false;
        }
	}


     void FixedUpdate()
    {
        if (fJoint.connectedBody == null && notInteracting == true)
        {
            GetComponent<Collider>().isTrigger = false;
        }
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
            GetComponent<Collider>().isTrigger = false;
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

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "canPickUp")
        {
            obj = null;
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
