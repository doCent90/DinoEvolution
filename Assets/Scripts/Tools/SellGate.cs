using DG.Tweening;
using UnityEngine;

public class SellGate : Gate
{
    [SerializeField] private Transform _point;

    private const float Duration = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg))
        {
            egg.Sell(transform);
            egg.transform.DOMove(_point.position, Duration);
        }
    }
}
