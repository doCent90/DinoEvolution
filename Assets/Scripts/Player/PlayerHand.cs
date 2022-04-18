using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(BoxCollider))]
public class PlayerHand : MonoBehaviour
{
    [SerializeField] private BossArea _bossArea;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private Transform _eggStackParent;
    [SerializeField] private Transform _eggStackPosition;

    private bool _isBusy = false;

    public EggMover LastInStack { get; private set; }
    public BossArea BossArea => _bossArea;
    public Transform EggStackParent => _eggStackParent;
    public Transform EggStackPosition => _eggStackPosition;

    public void SetLastEgg(EggMover eggMover)
    {
        LastInStack = eggMover;
    }

    public void OnEggAdded()
    {        
        _animator.Take();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg) && _isBusy == false)
        {
            LastInStack = egg.EggMover;
            egg.transform.parent = transform;
            egg.transform.position = _eggStackPosition.position;
            egg.OnHandTaked(this);
            OnEggAdded();
            _isBusy = true;
        }
    }
}
