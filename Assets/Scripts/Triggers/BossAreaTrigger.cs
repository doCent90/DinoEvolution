using System;
using UnityEngine;

public class BossAreaTrigger : MonoBehaviour
{
    public event Action BossAreaReached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            BossAreaReached?.Invoke();
    }
}
