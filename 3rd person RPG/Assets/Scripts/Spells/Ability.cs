using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
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

    public virtual void AttemptToCastAbility()
    {
        print("attempt to cast");
    }

    public virtual void SuccessfullyCastAbility(Ability ability)
    {
        print("ability successful");
    }
}

public enum AbilityType { Damage, Buff, Restoration}
public enum AbilityElement { Pure, Physical, Fire, Water, Frost, Lighting, Wind, Earth, Shadow }
