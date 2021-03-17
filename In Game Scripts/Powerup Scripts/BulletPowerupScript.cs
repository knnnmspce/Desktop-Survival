using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//The purpose of this script is to control the behavior for the bullet powerup pickup.

public class BulletPowerupScript : MonoBehaviour
{
    GameObject uiIndicator;
    GameObject player;
    PlayerShooting playershooting;
    float powerupDuration = 10f;
    int defaultDamage = 25;
    Vector3 scaleChange;
    float graceTime = 10f;
    Coroutine delRoutine;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        uiIndicator = GameObject.FindGameObjectWithTag("DamageBuffImage");
        playershooting = player.GetComponentInChildren<PlayerShooting>();
        scaleChange = new Vector3(-2f, -2f, -2f);
        delRoutine = StartCoroutine(DeletePowerup());
    }

    //Delete the powerup after gracetime if not picked up by the player
    IEnumerator DeletePowerup()
    {
        yield return new WaitForSeconds(graceTime);

        EventBroker.CallPowerupComplete();
        Destroy(gameObject);
    }

    IEnumerator DamageBuff()
    {
        yield return new WaitForSeconds(powerupDuration);

        playershooting.damagePerShot = defaultDamage;
        uiIndicator.GetComponent<Image>().enabled = false;
        EventBroker.CallPowerupComplete();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            StopCoroutine(delRoutine);
            playershooting.damagePerShot = defaultDamage * 10;
            gameObject.transform.localScale += scaleChange; //hide the prefab
            uiIndicator.GetComponent<Image>().enabled = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false; //prevent prefab from being recollected
            EventBroker.CallPowerupActive();
            StartCoroutine(DamageBuff());
        }
    }
}
