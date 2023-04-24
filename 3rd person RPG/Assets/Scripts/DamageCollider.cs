using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    Collider damageCollider;

    public int currentWeaponDamage = 25;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hittable" || other.tag == "Player" || other.tag == "Enemy")
        {
            CharacterStats characterStats = other.GetComponent<CharacterStats>();

            if(characterStats != null)
            {
                print("got enemy stats");
                characterStats.TakeDamage(currentWeaponDamage);
            }
        }
    }
}
