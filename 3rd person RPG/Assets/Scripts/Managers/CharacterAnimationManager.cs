using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    public Animator anim;

    public virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void PlayTargetAnimation(string targetAnimation, bool isInteracting, bool useRootMotion = false)
    {
        anim.SetBool("isInteracting", isInteracting);
        anim.SetBool("isUsingRootMotion", useRootMotion);
        anim.CrossFade(targetAnimation, 0.2f);
    }
}
