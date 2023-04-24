using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    PlayerInventory playerInventory;

    InventorySlot[] slots;

    private void Start()
    {
        playerInventory = FindAnyObjectByType<PlayerInventory>();
        playerInventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    private void Update()
    {
        
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < playerInventory.inventoryItems.Count)
            {
                slots[i].AddItem(playerInventory.inventoryItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
