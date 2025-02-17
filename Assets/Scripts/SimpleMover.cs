using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleMover", menuName = "Components/Movers/SimpleMover")]
//moves character with SimpleMove method left or right
internal class SimpleMover : Mover
{
    CharacterController controller;

    public override void Init(CharacterController character)
    {
        controller = character;
    }
    public override void Move(Vector2 positionToMoveTowards)
    {
        positionToMoveTowards.y = 0;
        positionToMoveTowards.x = positionToMoveTowards.x - controller.transform.position.x;
        controller.SimpleMove(positionToMoveTowards.normalized * speed); //I don't want to bother with gravity and create a proper Character Controller with acceleration etc.
    }

    public override void Jump(bool up)
    {
        Debug.Log($"Jumping {(up ? "up" : "down")} remove this string later");
    }
}
