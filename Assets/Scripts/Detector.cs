using System;
using UnityEngine;

internal abstract class Detector : ScriptableObject
{
    [SerializeField] DetectorType detectorType;
    internal CharacterController characterController;
    public abstract void Init(CharacterController controller);
    internal abstract Transform GetInfo();
}

public enum DetectorType
{
    SimpleRadius,
    HearingRadius,
    VisionRadius
}
public static class Utils
{
    /// <summary>
    /// Check if the distance between two transforms is less or equal than the given distance
    /// </summary>
    /// <param name="transform_A"></param>
    /// <param name="transform_B"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static bool CheckDistanceUsingX(Transform transform_A, Transform transform_B, float distance)
    {
        Vector3 posA = transform_A.position;
        Vector3 posB = transform_B.position;
        return Mathf.Abs(posB.x - posA.x) <= distance;
    }
}
