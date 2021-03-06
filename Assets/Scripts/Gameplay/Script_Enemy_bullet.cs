﻿using UnityEngine;
using System.Collections;

public class Script_Enemy_bullet : MonoBehaviour {
    public float bullet_LifeTime = 3F;
    private int damagePerShot;

    private ParticleSystem hitParticles;
    private Collision ShootHit;
    private int shootableMask;

    // Use this for initialization
    void Start()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        hitParticles = GetComponentInChildren<ParticleSystem>();
        Destroy(gameObject, bullet_LifeTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerShot);
            }
            Destroy(gameObject);
        }
        else if (col.gameObject.tag != "Enemies" && col.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    public void setDmg(int dmg)
    {
        damagePerShot = dmg;
    }
}
