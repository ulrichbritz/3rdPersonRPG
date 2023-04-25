using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    Animator animator;

    public override void Awake()
    {
        base.Awake();

        animator = GetComponentInChildren<Animator>();
    }

    public override void TakeDamage(int damage)
    {
        if (isDead)
            return;

        base.TakeDamage(damage);

        animator.Play("Damage_01");

        if(currentHP <= 0)
        {
            currentHP = 0;
            animator.Play("Death_01");
            isDead = true;
            //handle enemy death
        }
    }
}
