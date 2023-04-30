using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    PlayerInventory playerInventory;

    public EquipmentItem[] currentEquipment;

    public delegate void OnEquipmentChanged(EquipmentItem newItem, EquipmentItem oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerInventory = PlayerInventory.instance;
    }

    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlotPiece)).Length;
        currentEquipment = new EquipmentItem[numSlots];
    }

    public void Equip(EquipmentItem newItem)
    {
        int slotIndex = (int)newItem.equipSlotPiece;

        EquipmentItem oldItem = null;

        //check to see if weapon
        if (newItem.equipSlotPiece == EquipmentSlotPiece.Weapon)
        {
            WeaponItem newWeaponItem = newItem as WeaponItem;
            //check for two handed
            if (newWeaponItem.weaponType == WeaponType.TwoHand || newWeaponItem.weaponType == WeaponType.DuelWield || newWeaponItem.weaponType == WeaponType.Bow || newWeaponItem.weaponType == WeaponType.Guns || newWeaponItem.weaponType == WeaponType.Staff)
            {
                if (currentEquipment[slotIndex] != null)
                {
                    Unequip(slotIndex);
                }

                if (currentEquipment[slotIndex + 1] != null)
                {
                    Unequip(slotIndex + 1);
                }              
            }
            //normal one handed
            else
            {
                Unequip(slotIndex);
            }
        }
        //is offhand
        else if (newItem.equipSlotPiece == EquipmentSlotPiece.OffHand)
        {
            WeaponItem newOffhandItem = newItem as WeaponItem;
            if (currentEquipment[6] != null)
            {
                WeaponItem currentWeapon = currentEquipment[6] as WeaponItem;
                if(currentWeapon.weaponType == WeaponType.TwoHand || currentWeapon.weaponType == WeaponType.DuelWield || currentWeapon.weaponType == WeaponType.Bow || currentWeapon.weaponType == WeaponType.Guns || currentWeapon.weaponType == WeaponType.Staff)
                {
                    Unequip(slotIndex - 1);
                }
            }

            if (currentEquipment[slotIndex] != null)
            {
                Unequip(slotIndex);
            }
        }
        //if not wepaon or offhand
        else
        {
            if (currentEquipment[slotIndex] != null)
            {
                Unequip(slotIndex);
            }
        }

        currentEquipment[slotIndex] = newItem;  //equip item

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
    }

    public void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            Item oldItem = currentEquipment[slotIndex];
            playerInventory.AddItem(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem as EquipmentItem);
            }

        }
    }

}
