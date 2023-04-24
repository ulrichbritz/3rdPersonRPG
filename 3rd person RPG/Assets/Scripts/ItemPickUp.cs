using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPickUp : Interactable
{
    public Item item;

    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);

        PickUpItem(playerManager);
    }

    private void PickUpItem(PlayerManager playerManager)
    {
        PlayerInventory playerInventory;
        PlayerLocomotion playerLocomotion;
        AnimatorManager animatorManager;

        playerInventory = playerManager.GetComponent<PlayerInventory>();
        playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
        animatorManager = playerManager.GetComponentInChildren<AnimatorManager>();

        playerLocomotion.rb.velocity = Vector3.zero;    //to stop player from moving when interacting
        animatorManager.PlayTargetAnimation("Pick_Up_Item", true);  //looting item anim

        //check for can pickup etc
        bool wasPickedUp = playerInventory.AddItem(item);
        
        if (wasPickedUp)
        {
            playerManager.itemInteractableUIGameObject.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName;
            playerManager.itemInteractableUIGameObject.GetComponentInChildren<RawImage>().texture = item.itemIcon.texture;
            playerManager.itemInteractableUIGameObject.SetActive(true);
            Destroy(gameObject);
        }
        
    }
}
