using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SortGateAnimator : Gate
{
    [SerializeField] private Animator _animator;
    [Header("View")]
    [SerializeField] private MeshRenderer _positive;
    [SerializeField] private MeshRenderer _negative;

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

    public void EnablePositiveSort()
    {
        DisableView(_negative);
        EnableView(_positive);
    }

    public void EnableNegativeSort()
    {
        DisableView(_positive);
        EnableView(_negative);
    }

    private void DisablePlayerMover(Egg egg)
    {
        if (_isPlayerMoverDisable == false)
        {
            _isPlayerMoverDisable = true;
            egg.PlayerHand.PlayerMover.SetDefaultPosition();
        }
    }

    private void EnableView(MeshRenderer mesh)
    {
        mesh.enabled = true;
    }

    private void DisableView(MeshRenderer mesh)
    {
        mesh.enabled = false;
    }
}
