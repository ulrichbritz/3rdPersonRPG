using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public bool isPlayer = false;

    public HealthBar healthBar;
    public StaminaBar staminaBar;


    public int Strength = 10;
    public int Vitality = 10;


    public int maxHP;
    public int currentHP;

    public int maxStamina;
    public int currentStamina;

    public bool isDead;

    public virtual void Awake()
    {
    }

    public virtual void Start()
    {
        maxHP = SetMaxHealthFromStrength();
        currentHP = maxHP;
        healthBar.SetMaxHealth(maxHP);

        if(isPlayer)
        {
            maxStamina = SetMaxStaminaFromVitality();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
        }
        
    }

    public virtual int SetMaxHealthFromStrength()
    {
        maxHP = Strength * 10;
        return maxHP;
    }

    private int SetMaxStaminaFromVitality()
    {
        maxStamina = Vitality * 10;
        return maxStamina;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHP -= damage;

        healthBar.SetCurrentHealth(currentHP);

    }

    public void DrainStamina(int amount)
    {
        currentStamina -= amount;

        staminaBar.SetCurrentStamina(currentStamina);
    }
}
