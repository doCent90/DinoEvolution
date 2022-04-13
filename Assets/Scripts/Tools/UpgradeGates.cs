using System;
using UnityEngine;

public class UpgradeGates : MonoBehaviour
{
    [Header("Negative or Positive modificator")]
    [Range(-10f, 10f)]
    [SerializeField] private float _value;
    [SerializeField] private bool _health = true;
    [SerializeField] private bool _damage = false;

    private void OnEnable()
    {
        if (_health && _damage)
            throw new ArgumentOutOfRangeException("Only one modificator");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Egg egg))
        {
            if(_health)
                egg.ModifyHealth(_value);
            else if (_damage)
                egg.ModifyDamage(_value);
        }
    }
}
