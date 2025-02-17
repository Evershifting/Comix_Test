using System;
using UnityEngine;

internal abstract class Detector : ScriptableObject
{
    [SerializeField] DetectorType detectorType;
    internal CharacterController characterController;
    public abstract void Init(CharacterController controller);
    internal Transform GetInfo()
    {
        throw new NotImplementedException();
    }
}

public enum DetectorType
{
    SimpleRadius,
    HearingRadius,
    VisionRadius
}
