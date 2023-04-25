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

        HandleRotateTowardTarget(enemyManager);

        if (enemyManager.isPerformingAction)
        {
            enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        }

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

    private void HandleRotateTowardTarget(EnemyManager enemyManager)
    {
        //rotate manually
        if (enemyManager.isPerformingAction)
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = enemyManager.transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed * Time.deltaTime);
        }
        //rotate with navmesh(pathfinding)
        else
        {
            Vector3 relativeDirection = enemyManager.transform.InverseTransformDirection(enemyManager.agent.desiredVelocity);
            Vector3 targetVelocity = enemyManager.enemyrb.velocity;

            enemyManager.agent.enabled = true;
            enemyManager.agent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.enemyrb.velocity = targetVelocity;
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.agent.transform.rotation, enemyManager.rotationSpeed * Time.deltaTime);
        }
    }
}
