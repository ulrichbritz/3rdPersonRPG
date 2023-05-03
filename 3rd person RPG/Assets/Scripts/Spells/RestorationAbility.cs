using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Restoration Ability")]
public class RestorationAbility : Ability
{
    public int healAmount;


    public override void AttemptToCastAbility(AnimatorManager animatorManager, PlayerStats playerStats)
    {
        base.AttemptToCastAbility(animatorManager, playerStats);

        GameObject instantiatedChannelAbilityFX = Instantiate(abilityChannelFX, animatorManager.transform);
        animatorManager.PlayTargetAnimation(abilityAnimation, true);
        Debug.Log("Attempt to cast ability");
    }

    public override void SuccessfullyCastAbility(AnimatorManager animatorManager, PlayerStats playerStats)
    {
        base.SuccessfullyCastAbility(animatorManager, playerStats);

        GameObject instantiatedAbilitylFX = Instantiate(abilityCastFX, animatorManager.transform);
        playerStats.Heal(healAmount);
        Debug.Log("ability cast successfull");
    }
}