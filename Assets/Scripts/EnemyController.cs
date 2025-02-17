using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Mover mover;
    [SerializeField] Detector detector;
    [SerializeField] EnemyBehaviour enemyBehaviour;
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] CharacterController controller;

    Vector2 direction;
    Transform target;
    private void Awake()
    {
        mover.Init(controller);
    }
    private void Update()
    {
        target = detector.GetInfo();
        EnemyAction enemyAction = enemyBehaviour.GetActions(target); //attack, move
        if (enemyAction.actionType == EnemyActionType.Move)
        {
            mover.Move(enemyAction.MovePosition);
        }
        mover.Move(direction);
    }
}
public enum DetectorType
{
    SimpleRadius,
    HearingRadius,
    VisionRadius
}

public class AttackBehaviour : ScriptableObject
{
    [SerializeField] internal float attackRange = 1f;
    internal void Attack()
    {
        throw new NotImplementedException();
    }
}

public enum AttackType
{
    Melee,
    Ranged,
    Suicide
}

public struct EnemyAction
{
    public EnemyActionType actionType;
    public Vector2 MovePosition;
}
public enum EnemyActionType
{
    Move,
    Attack,
    Idle
}