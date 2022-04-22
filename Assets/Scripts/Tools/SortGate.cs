using UnityEngine;
using DG.Tweening;
using System;

public class SortGate : MonoBehaviour
{
    [SerializeField] private Transform _point;

    private int _count;

    private const float Duration = 1f;

    public event Action EggStackEmpty;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Egg egg))
            Sort(egg);

        if (other.TryGetComponent(out Player player) && _count <= 0)
            EggStackEmpty?.Invoke();        
    }

    private void Sort(Egg egg)
    {
        _count++;
        if(egg.WasUVLightsHeated == false || egg.WasWashed == false || egg.HaveNest == false)
        {
            _count--;
            egg.Sort(transform);
            egg.transform.DOMove(_point.position, Duration);
        }
    }
}
