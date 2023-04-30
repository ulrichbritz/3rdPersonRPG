using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment Item")]
public class EquipmentItem : Item
{
    public EquipmentSlotPiece equipSlotPiece;

    public int armorModifier;

    public override void Use()
    {
        base.Use();

        //equip item
        EquipmentManager.instance.Equip(this);
        //remove from inventory
        RemoveFromInventory();
    }
}

public enum EquipmentSlotPiece { Head, Neck, Chest, Arms, Finger, Finger2, Weapon, OffHand, Legs, Feet }
