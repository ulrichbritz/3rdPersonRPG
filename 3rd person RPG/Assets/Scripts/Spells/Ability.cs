using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public string abilityName;
    public Sprite itemIcon;

    public GameObject abilityChannelFX;
    public GameObject abilityCastFX;

    public string abilityAnimation;


    [Header("AbilityType")]
    public AbilityType spellType;
    public AbilityElement spellElement;

    [Header("Ability Description")]
    [TextArea]
    public string abilityDescription;

    [Header("Ability Requirements")]
    public bool requiresItem = false;
    public EquipmentSlotPiece requiredItemType;
    public WeaponType requiredWeaponType;

    public virtual void AttemptToCastAbility(AnimatorManager animatorManager, PlayerStats playerStats)
    {
       // print("attempt to cast");
    }

    public virtual void SuccessfullyCastAbility(AnimatorManager animatorManager, PlayerStats playerStats)
    {
       //print("ability successful");
    }

}

public enum AbilityType { Damage, Buff, Restoration}
public enum AbilityElement { Pure, Physical, Fire, Water, Frost, Lighting, Wind, Earth, Shadow }
