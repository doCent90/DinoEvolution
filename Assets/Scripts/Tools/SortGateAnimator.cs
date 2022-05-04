using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SortGateAnimator : Gate
{
    [SerializeField] private Animator _animator;

    private BoxCollider _collider;

    private const string OpenDoors = "OpenDoors";

    private void OnEnable()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg))
        {
            _collider.enabled = false;
            _animator.SetTrigger(OpenDoors);
        }
    }
}
