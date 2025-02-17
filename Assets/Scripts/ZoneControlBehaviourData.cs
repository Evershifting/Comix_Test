using System;
using System.Reflection.Emit;
using UnityEngine;

[CreateAssetMenu(fileName = "ZoneControlBehaviourData", menuName = "Components/Enemy/EnemyBehaviours/ZoneControlBehaviourData")]
internal class ZoneControlBehaviourData : EnemyBehaviourData
{
    [SerializeField] private float investigationRange = 6f;
    [SerializeField] private float chaseRange = 4f;
    [SerializeField] private float returnRange = 10f;       //distance from latest patrol point
    [SerializeField, Range(.05f, .5f)] private float patrolPointDetectionRange = .1f;

    public float InvestigationRange { get => investigationRange; }
    public float ChaseRange { get => chaseRange; }
    public float ReturnRange { get => returnRange; }
    public float PatrolPointDetectionRange { get => patrolPointDetectionRange; }
}
