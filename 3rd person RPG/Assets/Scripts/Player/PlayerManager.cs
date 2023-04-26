using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    Animator anim;
    InputManager inputManager;
    CameraManager cameraManager;
    PlayerStats playerStats;
    PlayerLocomotion playerLocomotion;

    InteractableUI interactableUI;
    public GameObject interactableUIGameObject;
    public GameObject itemInteractableUIGameObject;

    public bool isInteracting;
    public bool isUsingRootMotion;
    public bool canDoCombo;
    public bool isUsingRightHand;
    public bool isUsingLeftHand;
    public bool isInvulnerable;
    



    private void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        anim = GetComponentInChildren<Animator>();
        inputManager = GetComponent<InputManager>();
        playerStats = GetComponent<PlayerStats>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        interactableUI = FindAnyObjectByType<InteractableUI>();
    }

    private void Update()
    {
        isUsingRightHand = anim.GetBool("isUsingRightHand");
        isUsingLeftHand = anim.GetBool("isUsingLeftHand");

        inputManager.HandleAllInputs();
        CheckForInteractable();
        playerStats.RegenerateStamina();
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();

        //maybe put these things in update
        isInteracting = anim.GetBool("isInteracting");
        canDoCombo = anim.GetBool("canDoCombo");
        isUsingRootMotion = anim.GetBool("isUsingRootMotion");
        isInvulnerable = anim.GetBool("isInvulnerable");
        playerLocomotion.isJumping = anim.GetBool("isJumping");
        anim.SetBool("isGrounded", playerLocomotion.isGrounded);

        inputManager.interactInput = false;
        //until here
    }

    public void CheckForInteractable()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f))   //maybe add layers to ignore 17 for through walls etc
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                if (interactableObject != null)
                {
                    string interactableText = interactableObject.interactableText;  //set ui text
                    interactableUI.interactableText.text = interactableText;
                    interactableUIGameObject.SetActive(true);

                    if (inputManager.interactInput)
                    {
                        hit.collider.GetComponent<Interactable>().Interact(this);
                    }
                }
            }

        }
        else
        {
            if(interactableUIGameObject != null)
            {
                interactableUIGameObject.SetActive(false);
            }

            if (itemInteractableUIGameObject != null && inputManager.interactInput) 
            {
                itemInteractableUIGameObject.SetActive(false);
            }
        }
    }
}
