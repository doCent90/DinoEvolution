using UnityEngine;

public class Tools : MonoBehaviour
{
    [SerializeField] private RoadMover _roadMover;

    public RoadMover RoadMover => _roadMover;
}
