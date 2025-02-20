using UnityEngine;

/// <summary>
/// It's an example of a detector that checks if the player is inside the vision radius of the enemy. It also checks if the player is in front of the enemy.
/// But it won't work until we implement vision system (Layers, targets etc) in the game and that's out of the scope
/// </summary>
[CreateAssetMenu(fileName = "VisionRadiusDetector", menuName = "Components/Enemy/Detectors/VisionRadiusDetector")]
internal class VisionRadiusDetector : Detector
{
    [SerializeField] float radius = 10f;
    [SerializeField] bool isUsingDirection = false;
    Transform player;
    Transform eyesTransform;
    public override void Init(CharacterController controller)
    {
        characterController = controller;
        Eyes eyes = characterController.GetComponentInChildren<Eyes>();
        if (eyes != null)
        {
            eyesTransform = eyes.transform;
        }
    }
    internal override Transform GetInfo()
    {
        if (player == null)
        {
            player = PlayerController.Instance.transform;
        }
        if (Utils.CheckDistanceUsingX(player, characterController.transform, radius))
        {
            if (isUsingDirection)
            {
                if (!Utils.IsOnForwardSide(eyesTransform, player))
                {
                    return null;
                }
            }
            if (Physics.Raycast(eyesTransform.position, player.position, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Player")) 
                    return player;
            }
        }
        return null;
    }
}
