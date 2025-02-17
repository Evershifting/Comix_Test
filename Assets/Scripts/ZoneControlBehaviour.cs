using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBehaviour", menuName = "ActorsComponents/EnemyBehaviours/ZoneControlBehaviour")]
internal class ZoneControlBehaviour : EnemyBehaviour
{
    [SerializeField] List<Transform> patrolPoints;
    [SerializeField] float investigationRange = 6f;
    [SerializeField] float chaseRange = 4f;
    [SerializeField] float returnRange = 10f;       //distance from latest patrol point
    [SerializeField, Range(0f, .5f)] float patrolPointDetectionRange = .1f;

    Transform target;
    State state = State.Patrolling;
    CharacterController characterController;
    int currentPatrolPointIndex = 0;
    internal override void Init(CharacterController characterController)
    {
        this.characterController = characterController;
        if (patrolPoints.Count == 0)
        {
            patrolPoints = new List<Transform>();
            GameObject patrolPoint = new GameObject("Patrol Point");
            patrolPoint.transform.position = characterController.transform.position;
            patrolPoints.Add(patrolPoint.transform);
        }
    }
    internal override EnemyAction GetActions(Transform target)
    {
        //no target (hiding) or agent too far from patrol route (state!=State.Attacking in case enemy has attacks with reposition: backstaby teleport, charging player, etc.)
        if ((target == null && state != State.Patrolling && state != State.Returning)
            || (Vector3.SqrMagnitude(characterController.transform.position - patrolPoints[currentPatrolPointIndex].position) >= returnRange * returnRange && state != State.Attacking))
        {
            SetState(State.Returning);
            return new EnemyAction { actionType = EnemyActionType.Move, MovePosition = patrolPoints[currentPatrolPointIndex].position };
        }
        //target in attack range
        if (attackBehaviour != null && Vector3.SqrMagnitude(characterController.transform.position - patrolPoints[currentPatrolPointIndex].position) <= attackBehaviour.attackRange * attackBehaviour.attackRange)
        {
            SetState(State.Attacking);
            return new EnemyAction { actionType = EnemyActionType.Attack, MovePosition = target.position };
        }
        //target in chase range
        if (Vector3.SqrMagnitude(characterController.transform.position - patrolPoints[currentPatrolPointIndex].position) <= chaseRange * chaseRange)
        {
            SetState(State.Chasing);
            return new EnemyAction { actionType = EnemyActionType.Move, MovePosition = target.position };
        }
        //target in investigation range
        if (Vector3.SqrMagnitude(characterController.transform.position - patrolPoints[currentPatrolPointIndex].position) <= investigationRange * investigationRange)
        {
            SetState(State.Investigating);
            return new EnemyAction { actionType = EnemyActionType.Move, MovePosition = target.position };
        }
        //no target nearby so back to patrol
        SetState(State.Patrolling);
        if (Vector3.SqrMagnitude(characterController.transform.position - patrolPoints[currentPatrolPointIndex].position) <= patrolPointDetectionRange * patrolPointDetectionRange)
        {
            if (patrolPoints.Count == 1)
            {
                return new EnemyAction { actionType = EnemyActionType.Idle, MovePosition = Vector2.zero };
            }
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Count;
        }
        return new EnemyAction { actionType = EnemyActionType.Move, MovePosition = patrolPoints[currentPatrolPointIndex].position };
    }

    //futureproofing for potential use of full state machine and external state changes' subscribers
    private void SetState(State newState)
    {
        if (newState != state)
            state = newState;
    }

    private enum State
    {
        Patrolling,
        Investigating,
        Chasing,
        Attacking,
        Returning //future proof state
    }
}
