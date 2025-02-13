using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] SimpleMover mover;

    CharacterController controller;

    //cache
    Vector3 forward;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        mover.Init(controller);
    }

    void Update()
    {
        //Jump through the platform
        if (controller.isGrounded && Input.GetButtonDown("Jump") && Input.GetAxis("Vertical") < 0)
        {
            //set layer to ignore platform
            mover.Jump(false);
        }

        // Move forward / backward
        forward = transform.TransformDirection(Vector3.right);
        mover.Move(forward * Input.GetAxis("Horizontal")); 
    }
}
