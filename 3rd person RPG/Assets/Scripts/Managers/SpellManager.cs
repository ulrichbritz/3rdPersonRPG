using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public static SpellManager instance;

    public Ability[] currentAbilities;

    //public delegate void OnEquipmentChanged(EquipmentItem newItem, EquipmentItem oldItem);
    //public OnEquipmentChanged onEquipmentChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlotPiece)).Length;
        currentAbilities = new Ability[numSlots];
    }
}
