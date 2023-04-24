using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Enemy Actions/Attack Action")]
public class EnemyAttackAction : EnemyActions
{
    public int attackScore = 3;
    public float recoveryTime = 2f;

    public float maxAttackAngle = 35f;
    public float minAttackAngle = -35f;

    public float minDistanceNeededToAttack = 0;
    public float maxDistanceNeededToAttack = 3f;
}
