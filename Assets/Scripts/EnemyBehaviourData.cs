using UnityEngine;

internal abstract class EnemyBehaviourData : ScriptableObject
{
    //in case we need some shared data for all behaviours
}

internal abstract class EnemyBehaviour : MonoBehaviour
{
    public EnemyState State { get => state; }

    protected EnemyState state;
    protected CharacterController characterController;
    protected EnemyAttackBehaviour attackBehaviour;
    internal abstract EnemyAction GetActions(Transform transform);
    internal abstract void Init(CharacterController characterController, EnemyAttackBehaviour attackBehaviour);

    private void Start()
    {
        enabled = false; //we don't need later lifecycle calls. Especially all the checks for the Update method
    }
}

public enum EnemyBehaviourType
{
    Stationary,
    ZoneControl,
    Chase
}