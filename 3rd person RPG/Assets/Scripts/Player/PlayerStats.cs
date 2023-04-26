using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    AnimatorManager animatorManager;
    PlayerManager playerManager;

    public HealthBar healthBar;
    public StaminaBar staminaBar;

    public float staminaRegenAmount = 30f;
    private float staminaRegenTimer;


    public override void Awake()
    {
        base.Awake();

        animatorManager = GetComponentInChildren<AnimatorManager>();
        playerManager = GetComponent<PlayerManager>();
    }

    public override void Start()
    {
        base.Start();

        maxHP = SetMaxHealthFromStrength();
        currentHP = maxHP;
        healthBar.SetMaxHealth(maxHP);

        maxStamina = SetMaxStaminaFromVitality();
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
    }


    public override void TakeDamage(int damage)
    {
        if (playerManager.isInvulnerable)
            return;

        if (isDead)
            return;

        base.TakeDamage(damage);

        healthBar.SetCurrentHealth(currentHP);

        animatorManager.PlayTargetAnimation("Damage_01", true, true);

        if (currentHP <= 0)
        {
            currentHP = 0;
            animatorManager.PlayTargetAnimation("Death_01", true, true);
            isDead = true;
            //handle death
        }
    }

    public void DrainStamina(int amount)
    {
        currentStamina -= amount;

        staminaBar.SetCurrentStamina(currentStamina);
    }

    public void RegenerateStamina()
    {
        print(playerManager.isInteracting);
        if (playerManager.isInteracting)
        {
            staminaRegenTimer = 0;
        }
        else
        {
            staminaRegenTimer += Time.deltaTime;

            if (currentStamina < maxStamina && staminaRegenTimer > 1f)
            {
                currentStamina += staminaRegenAmount * Time.deltaTime;
                staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
            }
        }
    }

}
