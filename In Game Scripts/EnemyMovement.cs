using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//The purpose of this script is to define the movement for the enemy character along the navmesh as a navmesh agent

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    NavMeshAgent nav;
    PlayerHealth playerHealth;
    EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyController = GetComponent<EnemyController>();
        playerHealth = player.GetComponent<PlayerHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player is alive and the navmesh agent isn't disabled by the enemyController, chase after the target, which is the player
        if (playerHealth.currentHealth > 0 && !enemyController.navDisabled)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
        
    }
}
