﻿using System.Collections;
using UnityEngine;

public class Melee : MonoBehaviour {
    public float cooldown = 10.0F;
    public GameObject meleeAttack;

    IEnumerator MeleeAttack()
    {
		AudioManager.instance.Play("MeleeAttack");
        Instantiate(meleeAttack, transform.position, transform.rotation);
        yield return null;
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyDown("b"))
            UseSpell();
    }
    public void UseSpell()
    {
        MeleeAttack();
    }

    public float GetCooldown()
    {
        return cooldown;
    }
}
