using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//The purpose of this script is to control the behavior for the players shooting
public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 25;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public AudioSource gunAudio;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    
    Light gunLight;
    float effectsDisplayTime = 0.2f;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
    }

    private void OnEnable()
    {
        EventBroker.PlayerDied += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        EventBroker.PlayerDied -= HandlePlayerDeath;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position); //start the line at the position of the gun barrell where the script is attached

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyController enemyHealthStat = shootHit.collider.GetComponentInParent<EnemyController>();
            EnemyController enemyHealthType2 = shootHit.collider.GetComponent<EnemyController>();

            if (enemyHealthStat != null) //because whatever we hit may or may not have an EnemyController script so we only want to take action if it does
            {
                enemyHealthStat.TakeDamage(damagePerShot); //ray will allow us to get effect based on exactly where the bullets hit
            } else if (enemyHealthType2 != null)
            {
                enemyHealthType2.TakeDamage(damagePerShot); //some scripts were attached in different locations on certain prefabs
            }

            gunLine.SetPosition(1, shootHit.point); //where the line ends or where did we hit
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range); //draw a line between ends
        }
    }

    void HandlePlayerDeath()
    {
        PlayerShooting ps = gameObject.GetComponent<PlayerShooting>();
        if (ps != null)
        {
            ps.enabled = false;
        }
        else
        {
            Debug.LogError("PlayerShooting: could not find Playershooting ps");
        }
    }
}
