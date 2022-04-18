using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private RoadMover _splineMover;
    [SerializeField] private ParticleSystem _trail;
    [SerializeField] private PlayerAnimator _animator;

    public void EnableMove()
    {
        _trail.Play();
        _animator.Move();
        _mover.enabled = true;
        _splineMover.enabled = true;
    }

    public void DisableMove()
    {
        _trail.Stop();
        _mover.enabled = false;
        _splineMover.enabled = false;
    }
}
