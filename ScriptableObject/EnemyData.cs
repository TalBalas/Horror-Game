using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    [Header("AI Influence")]
    public float ArriveDis;
    public float PatrolSpeed;
    public float ChaseSpeed;
    public float KillDistance;

    [Header("View Influence")]
    public float ViewRadius;
    [Range(0, 200)]
    public float ViewAngle;
    public float MinViewDistance;
    public float SpherecastRadius;
    public float SpherecastOriginOffset;
    public float SpherecastTargetOffset;
    public float EnemyDisappearDistance;

}
