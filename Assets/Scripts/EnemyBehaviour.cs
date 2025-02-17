using UnityEngine;

internal abstract class EnemyBehaviour : ScriptableObject
{
    [SerializeField] internal AttackBehaviour attackBehaviour;
    internal abstract EnemyAction GetActions(Transform transform);
    internal abstract void Init(CharacterController characterController);
}

public enum EnemyBehaviourType
{
    Stationary,
    ZoneControl,
    Chase
}