using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SortGateAnimator : Gate
{
    [SerializeField] private Animator _animator;

    private BoxCollider _collider;
    private bool _isPlayerMoverDisable = false;

    private const string OpenDoors = "OpenDoors";

    private void OnEnable()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg))
        {
            DisablePlayerMover(egg);

            _collider.enabled = false;
            _animator.SetTrigger(OpenDoors);
        }
    }

    private void DisablePlayerMover(Egg egg)
    {
        if (_isPlayerMoverDisable == false)
        {
            _isPlayerMoverDisable = true;
            egg.PlayerHand.PlayerMover.SetDefaultPosition();
        }
    }
}
