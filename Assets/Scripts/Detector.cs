using System;
using UnityEngine;

internal abstract class Detector : ScriptableObject
{
    internal CharacterController characterController;
    public abstract void Init(CharacterController controller);
    internal abstract Transform GetInfo();
}
