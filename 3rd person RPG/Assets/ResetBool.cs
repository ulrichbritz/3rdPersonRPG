using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBool : StateMachineBehaviour
{
    public string isInteractingBool;
    public bool isInteractingStatus;

    public string isUsingRootMotionBool;
    public bool isUsingRootMotionStatus;

    public string canDoComboBool;
    public bool canDoComboStatus;

    public string isUsingRightHandBool;
    public bool isUsingRightHandStatus;

    public string isUsingLeftHandBool;
    public bool isUsingLeftHandStatus;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(isInteractingBool, isInteractingStatus);
        animator.SetBool(isUsingRootMotionBool, isUsingRootMotionStatus);
        animator.SetBool(canDoComboBool, canDoComboStatus);
        animator.SetBool(isUsingRightHandBool, isUsingRightHandStatus);
        animator.SetBool(isUsingLeftHandBool, isUsingLeftHandStatus);
    }

    
}
