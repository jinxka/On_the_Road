﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.Scripts.Others;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 100f;                 
    public float currentHealth;
    public float startingArmor = 10f;
    public float armor;
    
    public Slider healthSlider;
    public Image FillImage;                           
    public Color FullHealthColor = Color.green;      
    public Color ZeroHealthColor = Color.red;       

    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    IList<Quest> quests;
    bool isDead;
    bool damaged;
    bool quest_active;
    int nb_quests;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
        nb_quests = 0;
        SetHealthUI();      //new
    }


    void Update ()
    {
        for(int i = 0; i < nb_quests; i++)
        {

        }
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        SetHealthUI(); //new

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }

    private void SetHealthUI() //new
    {
        
        healthSlider.value = currentHealth;

        
        FillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, currentHealth / startingHealth);
    }
}
