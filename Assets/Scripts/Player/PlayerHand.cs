using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(BoxCollider))]
public class PlayerHand : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private Transform _eggStackPosition;

    private bool _isBusy = false;
    private Stack<Egg> _eggs = new Stack<Egg>();

    public EggMover LastInStack { get; private set; }
    public Transform EggStackPosition => _eggStackPosition;

    public void SetLastEgg(EggMover eggMover)
    {
        LastInStack = eggMover;
        _eggs.Push(LastInStack.Egg);
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
