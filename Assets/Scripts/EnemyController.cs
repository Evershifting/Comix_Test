using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] Mover mover;
    [SerializeField] Detector detector;
    [SerializeField] EnemyBehaviour enemyBehaviour;
    [SerializeField] EnemyAttackBehaviour attackBehaviour;
    [SerializeField] HealthSystem healthSystem; 

    [Header("Debug values")]
    [SerializeField] EnemyState enemyState;
    CharacterController controller;
    Vector2 direction;
    Transform target;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        mover.Init(controller);
        detector.Init(controller);
        enemyBehaviour.Init(controller, attackBehaviour);
    }
    private void Update()
    {
        target = detector.GetInfo();
        if (target == null)
            Debug.Log("No Target");
        else
            Debug.Log("Player is in range");
        EnemyAction enemyAction = enemyBehaviour.GetActions(target); //attack, move
        if (enemyAction.actionType == EnemyActionType.Move)
        {
            mover.Move(enemyAction.MovePosition);
        }

        //debug
        enemyState = enemyBehaviour.State;
    }
}
public class EnemyAttackBehaviour : ScriptableObject
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