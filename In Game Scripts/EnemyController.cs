using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//The purpose of this script is to control the how the enemy behaves while it is active. 
//It holds the health, score value and attack damage information, can attack the player, go into defense if available, and take damage and die.

public class EnemyController : MonoBehaviour
{
    #region StatDeclarations

    public int enemyType;
    
    public float sinkSpeed = 2.5f;
    public bool isDead;
    public Color flashColor = new Color(0f, 0f, 0f, 0.1f);
    public bool hasDefense;
    public bool isDefending;
    public float defenseTime = 4f;

    public int defaultHealth = 0;
    public int currentHealth = 0;

    public int attackDamage = 0;
    public float timeBetweenAttacks = 0f;
    public float timeBetweenLongAttacks = 0f;

    public int defaultValue = 0;
    public int currentValue = 0;
    public int poValue = 0;

    #endregion

    #region Declarations

    public GameObject[] powerups;
    public Material[] enemyMaterial;
    public bool navDisabled;

    Animator anim;
    AudioSource deathAudio;
    EnemyMovement enemyMovement;
    ParticleSystem particles;
    CapsuleCollider cap;
    BoxCollider box;

    GameObject player;
    PlayerHealth playerHealth;

    public static bool blockPowerup;
    float timer;
    float timer2;
    bool playerInRange;
    bool inLongAttackRange;
    bool didSecAttack;
    public static bool scoreDoubled;

    #endregion

    #region Start and Disable

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        particles = GetComponentInChildren<ParticleSystem>();
        cap = GetComponentInChildren<CapsuleCollider>();
        box = GetComponent<BoxCollider>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        EventBroker.PowerupComplete += UnBlockPowerup;
        EventBroker.NormalizeScore += NormalizeScorePoints;
        EventBroker.DoublePoints += HandleDoublePoints;
    }

    private void OnDisable()
    {
        EventBroker.PowerupComplete -= UnBlockPowerup;
        EventBroker.NormalizeScore -= NormalizeScorePoints;
        EventBroker.DoublePoints -= HandleDoublePoints;
    }

    #endregion

    #region Update

    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            HandlePlayerDeath();
            return;
        }
        //This update method takes a bool from the collision methods to determine when to attack and what kind of attack it can perform
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        inLongAttackRange = false;

        //Because of the lag of the contact between the enemy's collider and the player, we have to check after the animation has passed to see if contact was made or if it missed
        if (didSecAttack && playerInRange)
        {
            playerHealth.TakeDamage(attackDamage);
            didSecAttack = false;
        }
        //The enemy needs to be in a certain range to execute a long-ranged attack and it can't do it before the time limit passes between each attacck
        if (distanceToPlayer <= 4.5 && distanceToPlayer >= 3.5)
        {
            inLongAttackRange = true;
        }
        //If in range and the time has passed the you can attack if you're still alive
        if (inLongAttackRange && timer2 >= timeBetweenLongAttacks && currentHealth > 0)
        {
            Attack2();

        } //But if you're too close for a ranged attack then do the normal attack
        else if (timer >= timeBetweenAttacks && playerInRange && currentHealth > 0)
        {
            Attack();
        }

        if (scoreDoubled) //If this enemy exist before a player has picked up a score powerup but exist still after the player picks one up, its value needs to be doubled manually
        {
            if(currentValue == defaultValue)
            {
                currentValue = poValue;
            }
        }
        else
        {
            currentValue = defaultValue;
        }
    }

    #endregion

    #region Collision Handlers
    //Keep track of if the player is in range. This will be used to determine if the enemy can attack or not
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    #endregion

    #region Health Handlers
    //Handles taking damage, entering defenses for enemies with a defense, and death
    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            return;
        }
        else
        {
            currentHealth -= amount;
            //If the enemy has a defense and the health reaches threshold, then enter defense routine
            if (hasDefense && currentHealth <= 100)
            {
                InitiateDefense();
            }

            if (currentHealth <= 0)
            {
                Die();
            };
            //Flash the enemy to a white color to indicate damage every time hit
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = enemyMaterial[1];
            StartCoroutine(FixMeshColor());
        }
    }

    //All the things needed for an enemy to enter a defensive form and when to go back to chasing the player
    public void InitiateDefense()
    {
        isDefending = true;
        anim.SetTrigger("Hit");
        GetComponent<NavMeshAgent>().enabled = false;
        navDisabled = true;
        StartCoroutine(DefenseCountdownRoutine());
    }

    public void Die()
    {
        isDead = true;
        anim.SetTrigger("Dead");
        box.isTrigger = true;
        cap.enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        navDisabled = true;
        HandleDeathAudio();

        ScoreManager.Instance.UpdateScore(currentValue);
        ChanceSpawnPowerup(); //Some enemies will drop powerups on death

        EnemyManager.currentEnemies -= 1;
        Destroy(gameObject, 2f);
    }

    public void HandleDeathAudio()
    {
        switch (enemyType)
        {
            case 1: //slime
                deathAudio = GameAudioManager.Instance.fxAudio[2];
                if (deathAudio == null)
                {
                    Debug.Log("audio is null");
                }
                else
                {
                    deathAudio.Play();
                }
                break;
            case 2: //beholder
                deathAudio = GameAudioManager.Instance.fxAudio[1];
                if (deathAudio == null)
                {
                    Debug.Log("audio is null");
                }
                else
                {
                    deathAudio.Play();
                }
                break;
            case 3: //turtle
                deathAudio = GameAudioManager.Instance.fxAudio[3];
                if (deathAudio == null)
                {
                    Debug.Log("audio is null");
                }
                else
                {
                    deathAudio.Play();
                }
                break;
            default:
                Debug.LogError("Unable to find find associated enemy");
                break;
        }
    }

    #endregion

    #region Attack Handlers
    //Both types of attacks are defined here. Attack() is the standard attack while Attack2() is the long-range attack
    void Attack()
    {
        //Reset the timers so cooldown occurs 
        timer = 0f;
        timer2 = 0f;

        anim.SetTrigger("InRange");

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    void Attack2()
    {
        timer2 = 0f;
        timer = 0f;

        anim.SetTrigger("LongRange");
        didSecAttack = true;
    }

    #endregion

    #region IEnumerators
    //IEnumerators defined here. The first for what should be done once defense starts. The second changes the enemy back to its correct material color
    IEnumerator DefenseCountdownRoutine()
    {
        yield return new WaitForSeconds(defenseTime);

        GetComponent<NavMeshAgent>().enabled = true;
        navDisabled = false;
        anim.SetTrigger("TimePassed");
        isDefending = false;
        currentHealth = defaultHealth;

    }

    IEnumerator FixMeshColor()
    {
        yield return new WaitForSeconds(0.1f);

        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = enemyMaterial[0];
    }

    #endregion

    #region Powerup Handlers
    //Because only one powerup can be used and spawned at a time, we need to prevent the enemies for chancing a powerup if there already is one active. 
    //These events may subscribe so the controller knows when this is to happen.
    private void BlockPowerup()
    {
        blockPowerup = true;
    }

    private void UnBlockPowerup()
    {
        blockPowerup = false;
    }

    void NormalizeScorePoints()
    {
        currentValue = defaultValue;
        scoreDoubled = false;
    }

    void ChanceSpawnPowerup()
    {
        //creates a random number from 0-1. If that number is less than the threshold, it will spawn a random powerup.
        if (blockPowerup)
        {
            return;
        }
        else 
        {
            int n;
            GameObject powerup;
            Vector3 heightOffset = new Vector3(0, 1, 0);
            float threshHold = 0.1f;
            float chance;
            n = Random.Range(0, 4);

            chance = Random.Range(0, 100) / 100f;
            if (chance <= threshHold)
            {
                powerup = Instantiate(powerups[n], gameObject.transform.position + heightOffset, Quaternion.identity);
                BlockPowerup();
            }
        }
    }

    void HandleDoublePoints()
    {
        currentValue = poValue;
        scoreDoubled = true;
    }

    #endregion

    void HandlePlayerDeath()
    {
        //playerIsDead = true;
        anim.SetTrigger("PlayerDead");
        GetComponent<NavMeshAgent>().enabled = false;
        navDisabled = true;
    }
}
