using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    AnimatorManager animatorManager;

   

    public override void Awake()
    {
        base.Awake();

        animatorManager = GetComponentInChildren<AnimatorManager>();
    }


    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        animatorManager.PlayTargetAnimation("Damage_01", true, true);

        if (currentHP <= 0)
        {
            currentHP = 0;
            animatorManager.PlayTargetAnimation("Death_01", true, true);
            //handle death
        }
    }
    
}
