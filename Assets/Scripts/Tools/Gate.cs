using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private RoadMover _roadMover;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg))
            _roadMover.OnToolsWorked();
    }
}
