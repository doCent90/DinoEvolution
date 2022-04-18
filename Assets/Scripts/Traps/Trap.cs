using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private RoadMover _splineMover;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Egg egg))
        {
            _splineMover.OnTrapDone();
            egg.Destroy();
        }
    }
}
