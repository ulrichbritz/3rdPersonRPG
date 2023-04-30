using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;

    public Image icon;
    public Button removeButton;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.itemIcon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        PlayerInventory.instance.RemoveItem(item);
        //implement drop item on floor
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
