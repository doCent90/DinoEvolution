using UnityEngine;
using DG.Tweening;
using System;

public class SortGate : MonoBehaviour
{
    [SerializeField] private Transform _point;

    private bool _isPlayerMoverDisable = false;

    private const float Duration = 1f;

    public event Action EggStackEmpty;
    public event Action SortGateReached;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Egg egg))
            Sort(egg);

        if (other.TryGetComponent(out PlayerHand playerHand) && playerHand.IsBusy == false)
            EggStackEmpty?.Invoke();        
    }

    private void Sort(Egg egg)
    {
        DisablePlayerMover(egg);

        SortGateReached?.Invoke();

        if(egg.WasUVLightsHeated == false || egg.WasWashed == false || egg.HaveNest == false)
        {
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
}
