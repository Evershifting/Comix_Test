using UnityEngine;

[CreateAssetMenu(fileName = "SimpleMover", menuName = "ActorsComponents/Movers/SimpleMover")]
internal class SimpleMover : ScriptableObject, IMover
{
    [SerializeField] float speed = 13.0F;

    //bool isInitialized = false;
    CharacterController controller;

    //public bool IsInitialized { get => isInitialized; }

    public void Init(CharacterController character)
    {
        controller = character;
    }
    public void Move(Vector2 direction)
    {
        controller.SimpleMove(direction * speed); //I don't want to bother with gravity and create a proper Character Controller with acceleration etc.
    }

    public void Jump(bool up)
    {
        Debug.Log($"Jumping {(up ? "up" : "down")} remove this string later");
    }
}

internal interface IMover
{
    void Move(Vector2 direction);
    void Jump(bool down);
}