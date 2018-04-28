using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {


    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    Valve.VR.EVRButtonId touchPad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

    Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;

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
            Debug.Log("pressed trigger");
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

        if (obj != null && obj.name.Contains("liquid courage"))
        {
            
                transform.parent.GetComponent<PlayerBehaviour>().CalmDown();
                Debug.Log("calm down");
            
        }

      
	}


     void FixedUpdate()
    {
        if (fJoint.connectedBody == null && notInteracting == true)
        {
           // GetComponent<Collider>().isTrigger = false;
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

        if (other.gameObject.tag == "door")
        {
            if (controller.GetPressDown(triggerButton))
            {
                other.transform.parent.GetComponent<Doorcontrol>().Open = true;
            }
        }

        
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Door")
        {

            Debug.Log("collided");
            if (controller.GetPressDown(triggerButton))
            {
                if (other.transform.parent.GetComponent<Doorcontrol>().Open == false) other.transform.parent.GetComponent<Doorcontrol>().Open = true;
                else
                    other.transform.parent.GetComponent<Doorcontrol>().Open = false;

            }
        }

        if (other.gameObject.tag == "FireExit")
        {

            Debug.Log("exit");
            if (controller.GetPressDown(triggerButton))
            {
                other.gameObject.GetComponent<FireEscapeEvent>().TriggerAlarm();
            }
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
