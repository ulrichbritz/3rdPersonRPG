using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage = 25;

    private void OnTriggerEnter(Collider other)
    {
        CharacterStats characterStats = other.GetComponentInParent<CharacterStats>();

        if (characterStats != null)
        {
            characterStats.TakeDamage(damage);
        }
    }
}
