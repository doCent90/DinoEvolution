using System;
using UnityEngine;

public class BossAreaTrigger : MonoBehaviour
{
    [SerializeField] private Boss _boss;

    public Boss Boss => _boss;

    public event Action BossAreaReached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            BossAreaReached?.Invoke();
    }
}
