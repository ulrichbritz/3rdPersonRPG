using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{

    [SerializeField] EquipmentSlot[] slots;

    private void Awake()
    {

    }

    private void Start()
    {
       EquipmentManager.instance.onEquipmentChanged += UpdateUI;
    }

    private void Update()
    {

    }

    public void UpdateUI(EquipmentItem newItem, EquipmentItem oldItem)
    {
        EquipmentManager equipmentManager = EquipmentManager.instance;

        for (int i = 0; i < slots.Length; i++)
        {
            if (equipmentManager.currentEquipment[i] != null)
            {
                slots[i].AddItem(equipmentManager.currentEquipment[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
