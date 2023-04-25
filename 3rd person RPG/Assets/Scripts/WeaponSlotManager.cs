using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    CharacterStats characterStats;

    WeaponHolderSlot leftHandSlot;
    WeaponHolderSlot rightHandSlot;

    DamageCollider leftHandDamageCollider;
    DamageCollider rightHandDamageCollider;

    public WeaponItem usedWeapon;

    Animator animator;

    private void Awake()
    {
        characterStats = GetComponentInParent<CharacterStats>();
        animator = GetComponent<Animator>();

        
        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach(WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if (weaponSlot.isLeftHandSlot)
            {
                leftHandSlot = weaponSlot;
            }
            else if (weaponSlot.isRightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
        }
        
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft, bool isTwoHanded = false)
    {
        if (isLeft)
        {
            leftHandSlot.LoadWeaponModel(weaponItem);
            LoadLeftWeaponDamageCollider();

            #region Handle Left Weapon Idle Anims
            if (weaponItem != null)
            {
                animator.CrossFade(weaponItem.left_Hand_Idle, 0.2f);
            }
            else
            {
                animator.CrossFade("Left Arm Empty", 0.2f);
            }
            #endregion
        }
        else
        {
            if (isTwoHanded)
            {
                //unload other slots
                animator.CrossFade(weaponItem.two_Hand_Idle, 0.2f);
            }
            else
            {
                animator.CrossFade("Both Arms Empty", 0.2f);

                #region Handle Right Weapon Idle Anims
                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.right_Hand_Idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Right Arm Empty", 0.2f);
                }
                #endregion
            }

            rightHandSlot.LoadWeaponModel(weaponItem);
            LoadRightWeaponDamageCollider();

        }
    }

    #region Handle weapons damage collider

    private void LoadLeftWeaponDamageCollider()
    {
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    private void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    public void OpenDamageCollider()
    {
        leftHandDamageCollider.EnableDamageCollider();
    }

    public void CloseDamageCollider()
    {
        leftHandDamageCollider.DisableDamageCollider();
    }

    #endregion

    //remove stamina drain if i decide not to want
    #region Handle Weapon Stamina Drain 
    public void DrainStaminaOnLightAttack()
    {
        characterStats.DrainStamina(Mathf.RoundToInt(usedWeapon.baseStaminaCost * usedWeapon.lightAttackStaminaMultiplier));
    }

    public void DrainStaminaOnHeavyAttack()
    {
        characterStats.DrainStamina(Mathf.RoundToInt(usedWeapon.baseStaminaCost * usedWeapon.lightAttackStaminaMultiplier));
    }
    #endregion
}
