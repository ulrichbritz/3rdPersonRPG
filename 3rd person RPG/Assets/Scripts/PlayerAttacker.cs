using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;
    WeaponSlotManager weaponSlotManager;

    public string lastAttack;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponentInChildren<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
    }

    public void HandleWeaponCombo(WeaponItem weapon)
    {
        if (inputManager.comboFlag)
        {
            animatorManager.anim.SetBool("canDoCombo", false);
            if (lastAttack == weapon.OH_Light_Attack_01)
            {
                animatorManager.PlayTargetAnimation(weapon.OH_Light_Attack_02, true, true);
            }
        }

        
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
        if (playerManager.isInteracting)
        {
            return;
        }

        weaponSlotManager.usedWeapon = weapon;  //remove for stamina drain stuff

        animatorManager.PlayTargetAnimation(weapon.OH_Light_Attack_01, true, true);

        lastAttack = weapon.OH_Light_Attack_01;
    }

    public void HandleHeavyAttack(WeaponItem weapon)
    {
        if (playerManager.isInteracting)
        {
            return;
        }

        weaponSlotManager.usedWeapon = weapon;  //remove for stamina drain stuff

        animatorManager.PlayTargetAnimation(weapon.OH_Heavy_Attack_01, true, true);

        lastAttack = weapon.OH_Heavy_Attack_01;
    }
}
