using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Tools _tools;

    private RoadMover _roadMover;

    private void OnEnable()
    {
        _roadMover = _tools.RoadMover;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg))
            _roadMover.OnToolsWorked();
    }
}
