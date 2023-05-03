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
    public ManaBar manaBar;

    public float staminaRegenAmount => Vitality;
    public float staminaSprintDrainAmount = 20f;
    private float staminaRegenTimer;

    private float manaRegenAmount => Intelligence;
    public float manaRegenTimer;


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

        maxMana = SetMaxManaFromIntelligence();
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }

    public override void Heal(int healAmount)
    {
        base.Heal(healAmount);
        healthBar.SetCurrentHealth(currentHP);
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

            currentStamina -= staminaSprintDrainAmount * Time.deltaTime;
            staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
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

    public void DrainMana(float amount)
    {
        currentMana -= amount;

        manaBar.SetCurrentMana(currentMana);
    }

    public void RegenerateMana()
    {
        if (playerManager.isInteracting)
        {
            manaRegenTimer = 0;
        }
        else
        {
            manaRegenTimer += Time.deltaTime;

            if (currentMana < maxMana &&manaRegenTimer > 1f)
            {
                currentMana += manaRegenAmount * Time.deltaTime;
                manaBar.SetCurrentMana(Mathf.RoundToInt(currentStamina));
            }
        }
    }

}
