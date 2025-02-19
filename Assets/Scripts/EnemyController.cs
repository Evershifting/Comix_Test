using System.Reflection.Emit;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] Mover mover;
    [SerializeField] Detector detector;

    [Header("Debug values")]
    [SerializeField] EnemyState enemyState;
    
    //refs
    CharacterController controller;
    EnemyBehaviour enemyBehaviour;
    HealthSystem healthSystem;
    EnemyAttackBehaviour attackBehaviour;
    Animator animator;

    //cache
    Transform target;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        healthSystem = GetComponent<HealthSystem>();
        attackBehaviour = GetComponent<EnemyAttackBehaviour>();

        animator = GetComponent<Animator>();

        mover.Init(controller, animator);
        detector.Init(controller);
        enemyBehaviour.Init(controller, attackBehaviour);
    }
    private void Update()
    {
        target = detector.GetInfo();
        EnemyAction enemyAction = enemyBehaviour.GetActions(target); //attack, move
        switch (enemyAction.actionType)
        {
            case EnemyActionType.Move:
                mover.Move(enemyAction.MovePosition);
                break;
            case EnemyActionType.Attack:
                break;
            case EnemyActionType.Idle:
                break;
            case EnemyActionType.None:
                break;
            default:
                break;
        }

        //debug
        enemyState = enemyBehaviour.State;
    }
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
    Idle,
    None
}