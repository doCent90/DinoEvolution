using System;
using UnityEngine;

public class BossAreaTrigger : MonoBehaviour
{
    public event Action NestsReached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HandTrigger handTrigger))
            NestsReached?.Invoke();
    }
}
