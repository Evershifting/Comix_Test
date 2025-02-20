using UnityEngine;

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
    public static bool IsOnForwardSide(Transform myTransform, Transform targetTransform)
    {
        Vector3 directionToTarget = targetTransform.position - myTransform.position;
        float dotProduct = Vector3.Dot(myTransform.forward, directionToTarget.normalized);
        return dotProduct > 0;
    }
}
