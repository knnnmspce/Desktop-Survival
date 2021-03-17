using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//The purpose of this script is to control the behaviour of the shield poweup.

public class ShieldScript : MonoBehaviour
{
    public static bool shieldIsActive;

    GameObject player;
    GameObject uiIndicator;
    float shieldDuration = 15f;
    float graceTime = 10f;
    Coroutine delRoutine;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        uiIndicator = GameObject.FindGameObjectWithTag("ShieldImage");
        delRoutine = StartCoroutine(DeletePowerup());
    }


    //Delete the poweup if not picked up
    IEnumerator DeletePowerup()
    {
        yield return new WaitForSeconds(graceTime);

        EventBroker.CallPowerupComplete();
        Destroy(gameObject);
    }

    IEnumerator ShieldPowerUp()
    {
        yield return new WaitForSeconds(shieldDuration);

        uiIndicator.GetComponent<Image>().enabled = false;
        shieldIsActive = false;
        EventBroker.CallPowerupComplete();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            StopCoroutine(delRoutine);
            shieldIsActive = true;
            EventBroker.CallPowerupActive(); //alert subscribers a powerup is activated
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            uiIndicator.GetComponent<Image>().enabled = true;

            StartCoroutine(ShieldPowerUp());
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}
