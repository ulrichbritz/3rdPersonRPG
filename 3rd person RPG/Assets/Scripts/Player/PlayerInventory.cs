using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    WeaponSlotManager weaponSlotManager;

    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;

    public WeaponItem unarmedWeapon;

    public WeaponItem[] weaponsInRightHandSlots;
    public WeaponItem[] weaponsInLeftHandSlots;

    public List<Item> inventoryItems;
    public int inventorySpace = 20;

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
        

        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
    }

    private void Start()
    {
        rightWeapon = weaponsInRightHandSlots[0];
        leftWeapon = weaponsInLeftHandSlots[0];
        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);

        EquipmentManager.instance.onEquipmentChanged +=  ChangeWeapon;

    }

    public void ChangeWeapon(EquipmentItem newItem, EquipmentItem oldItem)
    {
        EquipmentManager equipmentManager = EquipmentManager.instance;

        if (equipmentManager.currentEquipment[6] != null)
        {
            WeaponItem newWeaponItem = equipmentManager.currentEquipment[6] as WeaponItem;

            if (newWeaponItem.isBow)
            {
                rightWeapon = newWeaponItem;
                weaponsInRightHandSlots[0] = rightWeapon;
                weaponSlotManager.LoadWeaponOnSlot(rightWeapon, true, true);
                leftWeapon = unarmedWeapon;
                weaponsInLeftHandSlots[0] = leftWeapon;
                weaponSlotManager.LoadWeaponOnSlot(leftWeapon, false);
            }
            else if (newWeaponItem.isTwoHanded)
            {
                rightWeapon = newWeaponItem;
                weaponsInRightHandSlots[0] = rightWeapon;

                weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false, true);
                weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
            }
            else if (newWeaponItem.isDuelWield)
            {
                rightWeapon = newWeaponItem;
                weaponsInRightHandSlots[0] = rightWeapon;

                weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
                weaponSlotManager.LoadWeaponOnSlot(rightWeapon, true);
            }
            else
            {
                rightWeapon = newWeaponItem;
                weaponsInRightHandSlots[0] = rightWeapon;

                weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
                weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
            }
        }
        else
        {
            rightWeapon = unarmedWeapon;
        }

        if (equipmentManager.currentEquipment[7] != null)
        {
            WeaponItem newOffhandItem = equipmentManager.currentEquipment[7] as WeaponItem;

            leftWeapon = newOffhandItem;
            weaponsInLeftHandSlots[0] = leftWeapon;

            weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
            weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
        }

    }

 

    public bool AddItem(Item item)
    {
        if (!item.isDefaultItem)
        {
            if(inventoryItems.Count >= inventorySpace)
            {
                Debug.Log("Not enough room in inventory");
                return false;
            }

            inventoryItems.Add(item);

            if(onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            
        }

        return true;
        
    }

    public void RemoveItem(Item item)
    {
        inventoryItems.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
