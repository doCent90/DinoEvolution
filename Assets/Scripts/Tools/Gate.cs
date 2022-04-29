using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Tools _tools;

    protected RoadMover RoadMover;

    private void OnEnable()
    {
        RoadMover = _tools.RoadMover;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg))
            RoadMover.OnToolsWorked();
    }
}
