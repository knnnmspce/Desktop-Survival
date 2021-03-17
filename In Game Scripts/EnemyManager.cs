using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The purpose of this script is to spawn in enemies while keeping of the number of enemies spawned. The maximum number of enemies allowed is 20.
//It will spawn enemies randomly at one of the two spawn points for that particular enemy, respective to its spawn time.

public class EnemyManager : Singleton <EnemyManager>
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    public static int maxEnemies = 20;
    public static int currentEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        currentEnemies = 0;
    }

    void Spawn()
    {
        if(playerHealth.currentHealth <= 0)
        {
            return;
        }

        //Randomly obtains one of the spawn points and spawns an enemy at that given spawn point with its rotation and adds to the enemy tracker
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        if(currentEnemies < maxEnemies)
        {
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            currentEnemies += 1;
        }       
    }
}
