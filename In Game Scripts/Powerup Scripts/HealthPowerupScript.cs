using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The purpose of this script is to control the behavior of the add health powerup.

public class HealthPowerupScript : MonoBehaviour
{
    PlayerHealth playerhealth;
    GameObject player;
    int maxHealth = 100;
    float graceTime = 10f;
    Coroutine delRoutine;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerhealth = player.GetComponent<PlayerHealth>();
        delRoutine = StartCoroutine(DeletePowerup());
    }

    //If the powerup isn't picked up after the graceTime has passed, delete the powerup.
    IEnumerator DeletePowerup()
    {
        yield return new WaitForSeconds(graceTime);

        EventBroker.CallPowerupComplete();
        Destroy(gameObject);
    }

    //When the player picks up the powerup, add 25 to the health but clamp it so it doesn't go over 100. Can only pickup if the player isn't already full health
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            if(playerhealth.currentHealth < maxHealth)
            {
                StopCoroutine(delRoutine);
                playerhealth.currentHealth += 25;
                playerhealth.currentHealth = Mathf.Clamp(playerhealth.currentHealth, 0, 100);
                EventBroker.CallPowerupComplete();
                Destroy(gameObject);
            }
            
        }
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}
