using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment Item/Weapon Item")]
public class WeaponItem : EquipmentItem
{
    public WeaponType weaponType;

    public GameObject modelPrefab;

    [Header("Idle Anims")]
    public string right_Hand_Idle;
    public string left_Hand_Idle;
    public string two_Hand_Idle;


    [Header("Attack Animations")]
    public string OH_Light_Attack_01;
    public string OH_Light_Attack_02;
    public string OH_Heavy_Attack_01;

    [Header("Stamina Costs")]
    public int baseStaminaCost;
    public float lightAttackStaminaMultiplier;
    public float heavyAttackStaminaMultiplier;

}

public enum WeaponType { OneHand, DuelWield, TwoHand, Bow, Guns, Staff}
