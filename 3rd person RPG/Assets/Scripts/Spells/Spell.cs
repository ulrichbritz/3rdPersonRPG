using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public GameObject spellChannelFX;
    public GameObject spellCastFX;

    public string spellAnimation;

    [Header("SpellType")]
    public SpellType spellType;
    public SpellElement spellElement;

    [Header("Spell Description")]
    [TextArea]
    public string spellDescription;

    public virtual void AttemptToCastSpell()
    {
        print("attempt to cast");
    }

    public virtual void SuccessfullyCastSpell()
    {
        print("Spell successful");
    }
}

public enum SpellType { Damage, Buff, Restoration}
public enum SpellElement { Pure, Physical, Fire, Water, Frost, Lighting, Wind, Earth, Shadow }
