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
    [SerializeField] private float _power = 0.5f;
    [SerializeField] private float _eggStackStep = 0.8f;
    [SerializeField] private float _addColliderSize = 0.3f;

    private Stack<Egg> _eggsStack = new Stack<Egg>();
    private List<Egg> _eggList = new List<Egg>();
    private BoxCollider _boxCollider;
    private Vector3 _origCollider;
    private Player _player;

    private const float DELAY = 0.03f;
    private const float MULTIPLY = 1.5f;

    public void DeleteNotCompleteEgg(Egg currentEgg)
    {
        foreach (var egg in _eggList)
        {
            if(egg == currentEgg)
            {
                _eggList.Remove(egg);
                egg.ResetEgg();
            }
        }
    }

    public void OnEggAdded(Egg egg)
    {
        IncreaseCollider();
        bool firstEgg;
        Egg lastEgg;
        Transform positionInStack;

        if (_eggsStack.Count > 0)
            lastEgg = _eggsStack.Peek();
        else
            lastEgg = null;

        _eggList.Add(egg);
        _eggsStack.Push(egg);

        if (_eggsStack.Count <= 1)
        {
            firstEgg = true;
            positionInStack = _eggStackPosition;
        }
        else
        {
            firstEgg = false;
            positionInStack = lastEgg.transform;
        }

        egg.OnTaked(_player, this, positionInStack, firstEgg, _eggStackStep, _power);       
        StartCoroutine(EggsAnimation());
        _animator.Take();
    }

    private void OnEnable()
    {
        _player = GetComponent<Player>();
        _boxCollider = GetComponent<BoxCollider>();

        _origCollider = _boxCollider.size;
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

        foreach (var item in _eggsStack)
        {
            if(item != null)
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
