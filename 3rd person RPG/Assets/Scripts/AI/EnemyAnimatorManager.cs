using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : CharacterAnimationManager
{
    EnemyLocomotionManager enemyLocomotionManager;

    public override void Awake()
    {
        base.Awake();

        enemyLocomotionManager = GetComponentInParent<EnemyLocomotionManager>();
    }

    private void OnAnimatorMove()
    {
        float delta = Time.deltaTime;
        enemyLocomotionManager.enemyrb.drag = 0;
        Vector3 deltaPos = anim.deltaPosition;
        deltaPos.y = 0;
        Vector3 velocity = deltaPos / delta;
        enemyLocomotionManager.enemyrb.velocity = velocity;
    }
}
