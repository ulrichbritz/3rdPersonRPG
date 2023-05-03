using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Spell")]
public class Ability : ScriptableObject
{
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

}

public enum AbilityType { Damage, Buff, Restoration}
public enum AbilityElement { Pure, Physical, Fire, Water, Frost, Lighting, Wind, Earth, Shadow }
