using UnityEngine;
using DG.Tweening;
using System;

public class SortGate : MonoBehaviour
{
    [SerializeField] private Tools _tools;
    [SerializeField] private Transform _point;
    [Header("View")]
    [SerializeField] private MeshRenderer _positive;
    [SerializeField] private MeshRenderer _negative;

    private RoadMover _mover;

    private bool _isPlayerMoverDisable = false;
    private bool _hasActivated = false;

    private const float Duration = 3f;

    public event Action SortGateReached;

    private void OnEnable()
    {
        _mover = _tools.RoadMover;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Egg egg))
        {
            if(_hasActivated == false)
                _mover.OnSortGateReached();

            Sort(egg);
        }

        if (other.TryGetComponent(out PlayerHand playerHand) && playerHand.IsBusy == false)
            _tools.GameOver.StopGame();        
    }

    private void Sort(Egg egg)
    {
        DisablePlayerMover(egg);
        DisableView(_negative);
        EnableView(_positive);

        _hasActivated = true;
        SortGateReached?.Invoke();

        if(egg.WasUVLightsHeated == false || egg.WasWashed == false || egg.HaveNest == false)
        {
            DisableView(_positive);
            EnableView(_negative);
            egg.Sort(transform);
            egg.transform.DOMove(_point.position, Duration);
        }
    }

    private void DisablePlayerMover(Egg egg)
    {
        if (_isPlayerMoverDisable == false)
        {
            _isPlayerMoverDisable = true;
            egg.PlayerHand.PlayerMover.SetDefaultPosition();
        }
    }

    private void EnableView(MeshRenderer mesh)
    {
        mesh.enabled = true;
    }

    private void DisableView(MeshRenderer mesh)
    {
        mesh.enabled = false;
    }
}
