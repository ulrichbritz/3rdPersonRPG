using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;
    WeaponSlotManager weaponSlotManager;
    CameraManager cameraManager;
    PlayerInventory playerInventory;
    EquipmentManager equipmentManager;
    SpellManager abilityManager;
    PlayerStats playerStats;

    public string lastAttack;

    private Ability abilityInUse;

    private void Awake()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponentInParent<InputManager>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
        cameraManager = FindAnyObjectByType<CameraManager>();
        playerInventory = GetComponentInParent<PlayerInventory>();
        equipmentManager = GetComponentInParent<EquipmentManager>();
        abilityManager = GetComponentInParent<SpellManager>();
        playerStats = GetComponentInParent<PlayerStats>();
    }

    #region Input Actions
    public void HandleAttackAction()
    {
        if (cameraManager.currentLockOnTarget == null)
        {
            Vector3 direction;
            Vector3 mouseWorldPos;
            Ray ray = cameraManager.playerCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, inputManager.leftClickHitLayerMask))
            {
                mouseWorldPos = hit.point;
                direction = mouseWorldPos - inputManager.transform.position;
                direction.Normalize();
                direction.y = 0;

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                inputManager.transform.rotation = targetRotation;
                print("rotate!");
            }
        }

        WeaponItem currentWeapon = equipmentManager.currentEquipment[6] as WeaponItem;
        if (currentWeapon != null)
        {
            if (currentWeapon.weaponType == WeaponType.OneHand || currentWeapon.weaponType == WeaponType.TwoHand || currentWeapon.weaponType == WeaponType.DuelWield)
            {
                //handle melee action
                PerformMeleeAttack();
            }

            if (currentWeapon.weaponType == WeaponType.Bow || currentWeapon.weaponType == WeaponType.Guns || currentWeapon.weaponType == WeaponType.Staff)
            {
                //handle ranged action
            }
        }
        //until unarmed set to default equipment
        else
        {
            PerformMeleeAttack();
        }
    }

    public void HandleAbilityAction(int slotIndex)
    {
        Ability ability = abilityManager.currentAbilities[slotIndex];
        PerformAbilityAction(ability);
    }

    #endregion

    #region Perform Attack Actions
    private void PerformMeleeAttack()
    {
        if (playerManager.canDoCombo)
        {
            inputManager.comboFlag = true;
            HandleWeaponCombo(playerInventory.rightWeapon);
            inputManager.comboFlag = false;
        }
        else
        {
            if (playerManager.isInteracting)
                return;

            if (playerManager.canDoCombo)
                return;

            HandletAttack(playerInventory.rightWeapon);
        }
    }

    #endregion

    #region Perfom Ability Action

    private void PerformAbilityAction(Ability ability)
    {
        bool canCast = false;
        if (ability != null)
        {
            abilityInUse = ability;

            if (playerStats.currentMana < ability.manaCost)
            {
                canCast = false;
                print($"Not enough mana to cast");
                return;
            }

            if (ability.requiresItem == false)
            {
                canCast = true;
            }
            else if (ability.requiredItemType == EquipmentSlotPiece.Weapon)
            {
                WeaponItem currentWeapon;
                if (equipmentManager.currentEquipment[6] != null)
                {
                    currentWeapon = equipmentManager.currentEquipment[6] as WeaponItem;

                    if (currentWeapon.weaponType == ability.requiredWeaponType)
                    {
                        canCast = true;
                    }
                    else
                    {
                        print($"Cant cast ability using {currentWeapon.weaponType} weapon");
                    }
                }
                else
                {
                    print($"Ability requires a {ability.requiredWeaponType} weapon");
                }
            }
            else if (ability.requiredItemType == EquipmentSlotPiece.OffHand)
            {
                //check if offhand is shield
            }
        }

        if (canCast)
        {
            ability.AttemptToCastAbility(animatorManager, playerStats);
        }
    }

    private void SuccessFullyCastSpell()
    {
        playerStats.DrainMana(abilityInUse.manaCost);
        abilityInUse.SuccessfullyCastAbility(animatorManager, playerStats);
    }

    #endregion

    #region Handling Actions
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

    public void HandletAttack(WeaponItem weapon)
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

    #endregion
}
