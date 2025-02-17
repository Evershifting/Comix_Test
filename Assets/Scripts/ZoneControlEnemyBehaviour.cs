using System;
using System.Collections.Generic;
using UnityEngine;

internal class ZoneControlEnemyBehaviour : EnemyBehaviour
{
    [SerializeField] ZoneControlBehaviourData data;
    [SerializeField] List<Transform> patrolPoints;

    [Header("Debug values")]
    [SerializeField] Transform myTarget;
    [SerializeField] int currentPatrolPointIndex = 0;

    internal override void Init(CharacterController characterController, EnemyAttackBehaviour attackBehaviour)
    {
        this.attackBehaviour = attackBehaviour;
        this.characterController = characterController;
        if (patrolPoints.Count == 0)
        {
            patrolPoints = new List<Transform>();
            GameObject patrolPoint = new GameObject("Patrol Point");
            patrolPoint.transform.position = base.characterController.transform.position;
            patrolPoints.Add(patrolPoint.transform);
        }
        state = EnemyState.Patrolling;
    }
    internal override EnemyAction GetActions(Transform target)
    {
        myTarget = target;
        if (myTarget != null)
        {
            //target in attack range and enemy has attack options
            if (attackBehaviour != null && Utils.CheckDistanceUsingX(characterController.transform, myTarget, attackBehaviour.attackRange))
            {
                SetState(EnemyState.Attacking);
                return new EnemyAction { actionType = EnemyActionType.Attack, MovePosition = myTarget.position };
            }
            //target in chase range
            if (Utils.CheckDistanceUsingX(characterController.transform, myTarget, data.ChaseRange))
            {
                SetState(EnemyState.Chasing);
                return new EnemyAction { actionType = EnemyActionType.Move, MovePosition = myTarget.position };
            }
            //Enemy is ignoring patrol "tether" if he's in full aggro mode
            if (!Utils.CheckDistanceUsingX(characterController.transform, patrolPoints[currentPatrolPointIndex], data.ReturnRange))
            {
                SetState(EnemyState.Returning);
                return new EnemyAction { actionType = EnemyActionType.Move, MovePosition = patrolPoints[currentPatrolPointIndex].position };
            }
            //target in investigation range and enemy not returning
            if (Utils.CheckDistanceUsingX(characterController.transform, myTarget, data.InvestigationRange) && state != EnemyState.Returning)
            {
                SetState(EnemyState.Investigating);
                return new EnemyAction { actionType = EnemyActionType.Move, MovePosition = myTarget.position };
            }
        }
        //no target (hiding) and agent too far from patrol route
        if (!Utils.CheckDistanceUsingX(characterController.transform, patrolPoints[currentPatrolPointIndex], data.ReturnRange))
        {
            SetState(EnemyState.Returning);
            return new EnemyAction { actionType = EnemyActionType.Move, MovePosition = patrolPoints[currentPatrolPointIndex].position };
        }
        //no target nearby so back to patrol
        if (Utils.CheckDistanceUsingX(characterController.transform, patrolPoints[currentPatrolPointIndex], data.PatrolPointDetectionRange))
        {
            if (patrolPoints.Count == 1)
            {
                return new EnemyAction { actionType = EnemyActionType.Idle, MovePosition = Vector2.zero };
            }
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Count;
            SetState(EnemyState.Patrolling);
        }
        return new EnemyAction { actionType = EnemyActionType.Move, MovePosition = patrolPoints[currentPatrolPointIndex].position };
    }

    private int GetClosestPatrolPointUsingX()
    {
        Transform closestPoint = null;
        float closestDistance = float.MaxValue;
        int index = 0;
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            float distance = Mathf.Abs(characterController.transform.position.x - patrolPoints[i].position.x);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                index = i;
            }
        }

        return index;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(characterController.transform.position, data.InvestigationRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(characterController.transform.position, data.ChaseRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(characterController.transform.position, data.ReturnRange);
        Gizmos.color = Color.yellow;
        foreach (Transform point in patrolPoints)
        {
            Gizmos.DrawSphere(point.position, 0.5f);
        }
    }

    //for potential use of full state machine and external state changes' subscribers
    private void SetState(EnemyState newState)
    {
        if (newState == state)
            return;
        if (newState == EnemyState.Returning)
            currentPatrolPointIndex = GetClosestPatrolPointUsingX();

        state = newState;
    }

}
public enum EnemyState
{
    Patrolling,
    Investigating,
    Chasing,
    Attacking,
    Returning //future proof state
}