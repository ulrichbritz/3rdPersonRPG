using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    EnemyLocomotionManager enemyLocomotionManager;

    public bool isPerformingAction;

    [Header("AI Settings")]
    public float detectionRaduis = 20f;
    //the higher and lower, these angles are, the greater the detection field of view
    public float maxDetectionAngle = 50f;
    public float minDetectionAngle = -50f;

    private void Awake()
    {
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
    }

    private void Update()
    {
        HandleCurrentAction();
    }

    private void HandleCurrentAction()
    {
        if(enemyLocomotionManager.currentTarget == null)
        {
            enemyLocomotionManager.HandleDetection();
        }
    }
}
