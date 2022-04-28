using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Trap : MonoBehaviour
{
    [SerializeField] private Tools _tools;

    private RoadMover _splineMover;
    private BoxCollider _boxCollider;

    private const float Delay = 0.5f;

    private void OnEnable()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _splineMover = _tools.RoadMover;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Egg egg))
        {
            EnableCoolDown();
            _splineMover.OnTrapWorked();
            egg.Destroy();
        }
    }

    private void EnableCoolDown()
    {
        DisableCollider();
        Invoke(nameof(EnableCollider), Delay);
    }

    private void DisableCollider()
    {
        _boxCollider.enabled = false;
    }

    private void EnableCollider()
    {
        _boxCollider.enabled = true;
    }
}
