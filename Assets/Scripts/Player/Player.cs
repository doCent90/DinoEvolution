using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private RoadMover _roadMover;
    [SerializeField] private ParticleSystem _trail;

    public void EnableMove()
    {
        _trail.Play();
        _mover.enabled = true;
        _roadMover.enabled = true;
    }

    public void DisableMove()
    {
        _trail.Stop();
        _mover.enabled = false;
        _roadMover.enabled = false;
    }
}
