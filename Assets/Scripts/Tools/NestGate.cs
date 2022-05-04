using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(NestGateAnimator))]
public class NestGate : Gate
{
    private NestGateAnimator _nestGateAnimator;
    private BoxCollider _boxCollider;

    private int _count = 12;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _nestGateAnimator = GetComponent<NestGateAnimator>();
    }

    public void GiveNest()
    {
        _count--;

        if(_count <= 0)
            _boxCollider.enabled = false;

        _nestGateAnimator.DeleteCurrentNest();
    }
}
