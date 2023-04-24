using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryWindow;
    public GameObject equipmentWindow;

    public void OpenInventoryWindow()
    {
        inventoryWindow.SetActive(true);
    }

    public void CloseInventoryWindow()
    {
        inventoryWindow.SetActive(false);
    }

    public void OpenEquipmentWindow()
    {
        equipmentWindow.SetActive(true);
    }

    public void CloseEquipmentWindow()
    {
        equipmentWindow.SetActive(false);
    }

}
