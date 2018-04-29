using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FireEscapeEvent : MonoBehaviour
{

    public GameObject player;
    public AudioSource AKTUMALARMEH;
    public AudioSource music, crowd;
    public Text runTEXT, exitTEXT, overHereTEXT;
    public Material redMat; 
 
    GameObject[] bystanders;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bystanders = GameObject.FindGameObjectsWithTag("bystander");

       
    }

    // Update is called once per frame
  

    public void TriggerAlarm()
    {

        AKTUMALARMEH.Play();
        Debug.Log("collided");
        GameObject.FindGameObjectWithTag("enterDoor").GetComponent<Doorcontrol>().ChangeDoorState();
        GameObject.FindGameObjectWithTag("exitDoor").GetComponent<Doorcontrol>().ChangeDoorState();
        exitTEXT.gameObject.SetActive(false);
        runTEXT.gameObject.SetActive(true);
        overHereTEXT.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
        crowd.Stop();
        music.Stop();
        BystanderBehaviour.alarmTriggered = true;
        foreach (GameObject bystander in bystanders)
        {
            bystander.transform.LookAt(player.transform);
            bystander.GetComponent<Animator>().SetTrigger("StareTrigger");
            Invoke("TriggerMovement", 5f);
            // TriggerMovement();

            // bystander.GetComponentInChildren<Renderer>().material = redMat;
        }

       
        
    }


    public void TriggerMovement()
    {
     
    }

}
