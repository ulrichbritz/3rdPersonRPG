using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : CharacterManager
{
    EnemyLocomotionManager enemyLocomotionManager;
    EnemyAnimatorManager enemyAnimatorManager;
    EnemyStats enemyStats;

    public State currentState;
    public CharacterStats currentTarget;
    public NavMeshAgent agent;
    public Rigidbody enemyrb;

    public bool isPerformingAction;
    public bool isInteracting;
    public float rotationSpeed = 25f;
    public float maxAttackRange = 1.5f;

    [Header("AI Settings")]
    public float detectionRaduis = 20f;
    //the higher and lower, these angles are, the greater the detection field of view
    public float maxDetectionAngle = 50f;
    public float minDetectionAngle = -50f;

    public float currentRecoveryTime = 0f;

    private void Awake()
    {
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        enemyStats = GetComponent<EnemyStats>();

        agent = GetComponentInChildren<NavMeshAgent>();
        enemyrb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        enemyrb.isKinematic = false;
    }

    private void Update()
    {
        HandleRecoveryTimer();

        isInteracting = enemyAnimatorManager.anim.GetBool("isInteracting");
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
    }

    private void HandleStateMachine()
    {
        if (currentState != null)
        {
            State nextState = currentState.Tick(this, enemyStats, enemyAnimatorManager);

            if(nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(State state)
    {
        currentState = state;
    }

    private void HandleRecoveryTimer()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if (isPerformingAction)
        {
            if (currentRecoveryTime <= 0)
            {
                isPerformingAction = false;
            }
        }
    }
}
