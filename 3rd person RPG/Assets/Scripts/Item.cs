using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [Header("IsDefault")]
    public bool isDefaultItem = false;

    [Header("Item Information")]
    public Sprite itemIcon;
    public string itemName;

    public virtual void Use()
    {
        //use item

        Debug.Log($"Used {itemName}");
    }

    public void RemoveFromInventory()
    {
        PlayerInventory.instance.RemoveItem(this);
    }

}
