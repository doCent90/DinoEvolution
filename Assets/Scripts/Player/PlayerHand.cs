using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(BoxCollider))]
public class PlayerHand : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private Transform _eggStackPosition;
    [Header("Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float _time = 0.5f;
    [SerializeField] private float _eggStackStep = 0.8f;
    [SerializeField] private float _addColliderSize = 0.3f;

    private Stack<Egg> _eggs = new Stack<Egg>();
    private BoxCollider _boxCollider;
    private Player _player;

    private const float DELAY = 0.03f;
    private const float MULTIPLY = 1.5f;

    public void OnEggAdded(Egg egg)
    {
        IncreaseCollider();
        Egg lastEgg;

        if (_eggs.Count > 0)
            lastEgg = _eggs.Peek();
        else
            lastEgg = null;

        _eggs.Push(egg);
        Transform positionInStack;

        if (_eggs.Count <= 1)
            positionInStack = _eggStackPosition;
        else
            positionInStack = lastEgg.transform;

        egg.OnTaked(_player, this, positionInStack, _eggStackStep, _time);       
        StartCoroutine(EggsAnimation());
        _animator.Take();
    }

    private void OnEnable()
    {
        _player = GetComponent<Player>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Egg egg) && egg.HasInStack == false)
            OnEggAdded(egg);
    }

    private IEnumerator EggsAnimation()
    {
        var waitForSecods = new WaitForSeconds(DELAY);
        yield return waitForSecods;

        foreach (var item in _eggs)
        {
            item.Animate();
            yield return waitForSecods;
        }
    }

    private void IncreaseCollider()
    {
        Vector3 target = new Vector3(0, 0, _addColliderSize);
        _boxCollider.size += target * MULTIPLY;
        _boxCollider.center += target;
    }
}
