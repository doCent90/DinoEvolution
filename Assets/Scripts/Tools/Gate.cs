using System;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Tools _tools;

    protected RoadMover RoadMover;

    public event Action GateDone;
    public event Action GateReached;

    private void OnEnable()
    {
        RoadMover = _tools.RoadMover;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg))
        {
            RoadMover.OnToolsWorked();
            GateReached?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerHand playerHand))
            GateDone?.Invoke();
    }
}
