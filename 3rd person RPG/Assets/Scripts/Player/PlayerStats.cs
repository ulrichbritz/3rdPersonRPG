using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    AnimatorManager animatorManager;
    PlayerManager playerManager;
    PlayerLocomotion playerLocomotion;

    public HealthBar healthBar;
    public StaminaBar staminaBar;

    public float staminaRegenAmount = 30f;
    public float staminaSprintDrainAmount = 20f;
    private float staminaRegenTimer;


    public override void Awake()
    {
        base.Awake();

        animatorManager = GetComponentInChildren<AnimatorManager>();
        playerManager = GetComponent<PlayerManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
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

    public void DrainStaminaFromSprinting()
    {
        if (playerLocomotion.isSprinting)
        {
            staminaRegenTimer = 0f;

            if (currentStamina <= maxStamina)
            {
                currentStamina -= staminaSprintDrainAmount * Time.deltaTime;
                staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
            }
        }
    }

    public void DrainStaminaFromAction(int amount)
    {
        currentStamina -= amount;

        staminaBar.SetCurrentStamina(currentStamina);
    }

    public void RegenerateStamina()
    {
        if (playerManager.isInteracting || playerLocomotion.isSprinting)
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
