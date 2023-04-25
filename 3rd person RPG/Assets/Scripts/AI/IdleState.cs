using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public PursueTargetState pursueTargetState;

    public LayerMask detectionLayer;


    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        #region Handle Enemy Target Detection
        //look for potential target
        Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, enemyManager.detectionRaduis, detectionLayer);

        //switch to pursue target state if target found
        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

            if (characterStats != null)
            {
                //check for team id

                Vector3 targetDirection = characterStats.transform.position - enemyManager.transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

                if (viewableAngle > enemyManager.minDetectionAngle && viewableAngle < enemyManager.maxDetectionAngle)
                {
                    enemyManager.currentTarget = characterStats;  
                }
            }
        }

        #endregion

        #region Handle Switching State
        if (enemyManager.currentTarget != null)
        {
            return pursueTargetState;
        }
        //if not target found return this state
        else
        {
            return this;
        }
        #endregion
    }
}
