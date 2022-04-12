using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private RoadMover _roadMover;

    public void EnableMove()
    {
        _mover.enabled = true;
        _roadMover.enabled = true;
    }

    public void DisableMove()
    {
        _mover.enabled = false;
        _roadMover.enabled = false;
    }
}
