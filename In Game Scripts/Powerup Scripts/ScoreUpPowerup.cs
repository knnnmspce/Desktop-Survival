using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//The purpose of this script is to control the behavior of the score double powerup.
public class ScoreUpPowerup : MonoBehaviour
{
    GameObject uiIndicator;
    GameObject player;
    float powerUpTime = 15f;
    float graceTime = 10f;
    Coroutine delRoutine;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        uiIndicator = GameObject.FindGameObjectWithTag("DoublePointsImage");
               
        delRoutine = StartCoroutine(DeletePowerup());

    }

    IEnumerator DeletePowerup()
    {
        yield return new WaitForSeconds(graceTime);

        EventBroker.CallPowerupComplete();
        Destroy(gameObject);
    }

    IEnumerator DoublePoints()
    {
        yield return new WaitForSeconds(powerUpTime);

        EventBroker.CallPowerupComplete();
        EventBroker.CallNormalizeScore(); //Alert all subscribers that the enemy value should return to the default enemy value. Necessary for already instantiated enemies.
        uiIndicator.GetComponent<Image>().enabled = false;
        Destroy(gameObject);
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            StopCoroutine(delRoutine);
            EventBroker.CallDoublePoints();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            uiIndicator.GetComponent<Image>().enabled = true;
            EventBroker.CallPowerupActive();
            StartCoroutine(DoublePoints());
        }
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0); //beacuse this object spawns in on it's side, we have to manually rotate it to be upright
    }
}
