using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Mover mover;

    CharacterController controller;

    //in the future access to the player will be through proper game manager
    public static PlayerController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);

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

        // Move left/right
        mover.Move(transform.position + transform.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal"));
    }
}
