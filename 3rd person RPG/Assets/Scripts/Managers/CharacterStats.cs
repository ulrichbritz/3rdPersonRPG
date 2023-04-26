using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public bool isPlayer = false;

    public int Strength = 10;
    public int Vitality = 10;


    public int maxHP;
    public int currentHP;

    public float maxStamina;
    public float currentStamina;

    public bool isDead;

    public virtual void Awake()
    {
    }

    public virtual void Start()
    {
          
    }

    public virtual void TakeDamage(int damage)
    {
        currentHP -= damage;
    }

    public virtual int SetMaxHealthFromStrength()
    {
        maxHP = Strength * 10;
        return maxHP;
    }

    public float SetMaxStaminaFromVitality()
    {
        maxStamina = Vitality * 10;
        return maxStamina;
    }

}
