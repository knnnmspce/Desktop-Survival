using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//The purpose of this script is to control the behavior of the players health
public class PlayerHealth : MonoBehaviour
{
    public int initialHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public AudioSource hurtAudio;
    public AudioSource playerDeath;

    Animator anim;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    GameObject ring;
    bool isDead;
    bool damaged;
    bool powerupActive;
    MeshRenderer ringMesh;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        ring = GameObject.FindGameObjectWithTag("Ring");
        currentHealth = initialHealth;

        ringMesh = ring.GetComponent<MeshRenderer>();

        EventBroker.PowerupActive += HandlePowerupActive;
        EventBroker.PowerupComplete += HandlePowerupComplete;
    }

    private void OnDisable()
    {
        EventBroker.PowerupActive -= HandlePowerupActive;
        EventBroker.PowerupComplete -= HandlePowerupComplete;
    }

    private void HandlePowerupComplete()
    {
        powerupActive = false;
    }

    private void HandlePowerupActive()
    {
        powerupActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        } else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        if(powerupActive && !isDead)
        {
            ringMesh.enabled = true;
        }
        else
        {
            ringMesh.enabled = false;
        }
        healthSlider.value = currentHealth;
        damaged = false;
    }

    public void TakeDamage(int damage)
    {
        if(damage > 0 && !ShieldScript.shieldIsActive)
        {
            damaged = true;
            currentHealth -= damage;
            healthSlider.value = currentHealth;
            hurtAudio.Play();


            if (currentHealth <= 0 && !isDead)
            {
                Die();
            }
        }    
    }

    void Die()
    {
        playerDeath.Play();
        isDead = true;
        anim.SetTrigger("Dead");
        EventBroker.CallPlayerDied();
        playerMovement.enabled = false;
    }
}
