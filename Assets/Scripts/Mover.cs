using UnityEngine;

internal abstract class Mover : ScriptableObject
{
    [SerializeField] protected float speed = 1f;
    public abstract void Init(CharacterController character, Animator animator);
    public abstract void Move(Vector2 positionToMoveTowards);
    public abstract void Jump(bool down);
}
