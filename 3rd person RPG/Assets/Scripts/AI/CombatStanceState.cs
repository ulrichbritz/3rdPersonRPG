using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStanceState : State
{
    public AttackState attackState;
    public PursueTargetState pursueTargetState;

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        //maybe circle around player

        //if in attack range switch to attack state
        if (enemyManager.currentRecoveryTime <= 0 && distanceFromTarget <= enemyManager.maxAttackRange)
        {
            return attackState;
        }
        //if player runs out of range, return pursue target state
        else if (distanceFromTarget > enemyManager.maxAttackRange)
        {
            return pursueTargetState;
        }
        else
        {
            return this;
        }
 
    }
}
