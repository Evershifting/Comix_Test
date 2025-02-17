using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRadiusDetector", menuName = "Components/Enemy/Detectors/SimpleRadiusDetector")]
internal class SimpleRadiusDetector : Detector
{
    [SerializeField] float radius = 10f;
    Transform player;
    public override void Init(CharacterController controller)
    {
        characterController = controller;
    }

    internal override Transform GetInfo()
    {
        if (player == null)
        {
            player = PlayerController.Instance.transform;
        }
        if (Utils.CheckDistanceUsingX(player, characterController.transform, radius))
            return player;
        else
            return null;
    }
}
