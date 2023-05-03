using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimatorManager animatorManager;
    PlayerLocomotion playerLocomotion;
    PlayerAttacker playerAttacker;
    PlayerInventory playerInventory;
    PlayerManager playerManager;
    UIManager uiManager;
    CameraManager cameraManager;
    PlayerStats playerStats;

    public LayerMask leftClickHitLayerMask;
    public LayerMask rightClickHitLayerMask;

    public Vector2 movementInput;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public Vector2 cameraInput;
    public float cameraInputVertical;
    public float cameraInputHorizontal;

    public bool sprintInput;
    public bool jumpInput;
    public bool dodgeInput;
    public bool lightAttackInput;
    public bool rightClickLockOnInput;
    public bool interactInput;
    public bool inventoryInput;
    public bool equipmentInput;
    public bool lockOnInput;
    public bool leftLockOn;
    public bool rightLockOn;
    public bool spell1Input;


    public bool comboFlag;
    public bool inventoryFlag;
    public bool equipmentFlag;
    public bool lockOnFlag;
    //public bool rollFlag;

    [Header("JumpTimers")]
    bool canPressJump = true;
    [SerializeField] float maxJumpCoolDown = 1f;
    float currentJumpCoolDown = 0;

    private void Awake()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerAttacker = GetComponentInChildren<PlayerAttacker>();
        playerInventory = GetComponent<PlayerInventory>();
        playerManager = GetComponent<PlayerManager>();
        uiManager = FindObjectOfType<UIManager>();
        cameraManager = FindAnyObjectByType<CameraManager>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
            playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;

            playerControls.PlayerActions.Jump.performed += i => jumpInput = true;

            playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;

            playerControls.PlayerActions.LightAttack.performed += i => lightAttackInput = true;
            playerControls.PlayerActions.RightClickLockOn.performed += i => rightClickLockOnInput = true;

            playerControls.PlayerActions.Interact.performed += i => interactInput = true;

            playerControls.PlayerActions.Inventory.performed += i => inventoryInput = true;
            playerControls.PlayerActions.Equipment.performed += i => equipmentInput = true;

            playerControls.PlayerActions.LockOn.performed += i => lockOnInput = true;
            playerControls.PlayerMovement.LockOnTargetLeft.performed += i => leftLockOn = true;
            playerControls.PlayerMovement.LockOnTargetRight.performed += i => rightLockOn = true;

            playerControls.PlayerActions.Spell1.performed += i => spell1Input = true;

        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void FixedUpdate()
    {
        if (!canPressJump)
        {
            currentJumpCoolDown -= Time.deltaTime;
            if (currentJumpCoolDown <= 0)
                canPressJump = true;
        }
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpInput();
        HandleDodgeInput();
        HandleAttackInput();
        HandleRightClickLockOnInput();
        HandleInteractInput();
        HandleInventoryInput();
        HandleEquipmentInput();
        HandleLockOnInput();
        HandleSpellOneInput();
    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputVertical = cameraInput.y;
        cameraInputHorizontal = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleSprintingInput()
    {
        if (playerStats.currentStamina <= 0)
        {
            sprintInput = false;
            playerLocomotion.isSprinting = false;
            return;
        }

        if (sprintInput && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {
        if (jumpInput)
        {
            jumpInput = false;

            if (canPressJump)
            {
                canPressJump = false;
                currentJumpCoolDown = maxJumpCoolDown;
                playerLocomotion.HandleJump();
            }          
        }
    }

    private void HandleDodgeInput()
    {
        if (dodgeInput)
        {
            dodgeInput = false;
            //rollFlag = true;
            playerLocomotion.HandleDodge();
        }
    }

    private void HandleAttackInput()
    {
        if (lightAttackInput)
        {
            lightAttackInput = false;
            playerAttacker.HandleAttackAction();
        }

    }

    private void HandleRightClickLockOnInput()
    {
        if (rightClickLockOnInput)
        {
            rightClickLockOnInput = false;
            lockOnFlag = false;
            cameraManager.ClearLockOnTargets();

            Ray ray = cameraManager.playerCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, rightClickHitLayerMask))
            {
                if (hit.collider != null)
                {
                    print(hit.collider.gameObject.name);
                    CharacterManager hitCharacterManager = hit.collider.gameObject.GetComponent<CharacterManager>();

                    if (hitCharacterManager != null)
                    {
                        cameraManager.HandleLockOn();
                        cameraManager.currentLockOnTarget = hitCharacterManager;
                        lockOnFlag = true;
                    }
                }
            }
        }
    }


    private void HandleInteractInput()
    {
        if (interactInput)
        {
            //interactInput = false;     //moved to playermanager late update
        }
    }

    private void HandleInventoryInput()
    {
        if (inventoryInput)
        {
            inventoryInput = false;
            inventoryFlag = !inventoryFlag;

            if (inventoryFlag)
            {
                uiManager.OpenInventoryWindow();
            }
            else
            {
                uiManager.CloseInventoryWindow();
            }
        }
    }

    private void HandleEquipmentInput()
    {
        if (equipmentInput)
        {
            equipmentInput = false;
            equipmentFlag = !equipmentFlag;

            if (equipmentFlag)
            {
                uiManager.OpenEquipmentWindow();
            }
            else
            {
                uiManager.CloseEquipmentWindow();
            }
        }
    }

    private void HandleLockOnInput()
    {
        if (lockOnInput && !lockOnFlag)
        {
            lockOnInput = false;
            cameraManager.HandleLockOn();

            if(cameraManager.nearestLockOnTarget != null)
            {
                cameraManager.currentLockOnTarget = cameraManager.nearestLockOnTarget;
                lockOnFlag = true;
            }
        }
        else if (lockOnInput && lockOnFlag)
        {
            lockOnInput = false;
            lockOnFlag = false;

            //clear lock on targets
            cameraManager.ClearLockOnTargets();
        }

        if(lockOnFlag && leftLockOn)
        {
            leftLockOn = false;
            cameraManager.HandleLockOn();
            if(cameraManager.leftLockTarget != null && cameraManager.currentLockOnTarget != cameraManager.leftLockTarget)
            {
                cameraManager.currentLockOnTarget = cameraManager.leftLockTarget;
            }
        }
        
        if (lockOnFlag && rightLockOn)
        {
            rightLockOn = false;
            cameraManager.HandleLockOn();
            if (cameraManager.rightLockTarget != null)
            {
                cameraManager.currentLockOnTarget = cameraManager.rightLockTarget;
            }
        }

    }

    void HandleSpellOneInput()
    {
        if (spell1Input)
        {
            spell1Input = false;

            if(playerManager.isInteracting == false)
            {
                playerAttacker.HandleAbilityAction(0);
            }
            
        }


    }



}
