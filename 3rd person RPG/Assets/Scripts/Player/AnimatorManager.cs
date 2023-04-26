using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : CharacterAnimationManager
{
    PlayerManager playerManager;
    PlayerLocomotion playerLocomotion;
    InputManager inputManager;

    int horizontal;
    int vertical;

    public override void Awake()
    {
        base.Awake();

        playerManager = GetComponentInParent<PlayerManager>();
        playerLocomotion = GetComponentInParent<PlayerLocomotion>();
        inputManager = GetComponentInParent<InputManager>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }


    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
    {
        //Animation Snapping
        float snappedHorizontal;
        float snappedVertical;
        #region Horizontal Snapping
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }          
        else if (horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1f;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1f;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion
        #region Vertical Snapping
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            snappedVertical = 1f;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            snappedVertical = -1f;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion


        if (inputManager.lockOnFlag && !isSprinting)
        {
            if(inputManager.horizontalInput == 0)
            {
                anim.SetFloat(horizontal, inputManager.horizontalInput, 0.1f, Time.deltaTime);
                anim.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
            }
            else
            {
                anim.SetFloat(horizontal, inputManager.horizontalInput, 0.1f, Time.deltaTime);
                anim.SetFloat(vertical, 0, 0.1f, Time.deltaTime);
            }
            
        }
        else if (isSprinting)
        {
            snappedHorizontal = horizontalMovement;
            snappedVertical = 2f;

            anim.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
            anim.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
            anim.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
        }
  
    }

    public void EnableCombo()
    {
        anim.SetBool("canDoCombo", true);
    }

    public void DisableCombo()
    {
        anim.SetBool("canDoCombo", false);
    }

    public void EnableIsInvulnerable()
    {
        anim.SetBool("isInvulnerable", true);
    }

    public void DisableIsInvulnerable()
    {
        anim.SetBool("isInvulnerable", false);
    }

    private void OnAnimatorMove()
    {
        if (playerManager.isUsingRootMotion)
        {
            playerLocomotion.rb.drag = 0;
            Vector3 deltaPos = anim.deltaPosition;
            deltaPos.y = 0;
            Vector3 velocity = deltaPos / Time.deltaTime;
            playerLocomotion.rb.velocity = velocity;
        }
    }
}
