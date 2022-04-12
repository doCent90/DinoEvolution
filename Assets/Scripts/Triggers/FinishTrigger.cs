using System;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public event Action Finished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerSpeedHandler player))
            Finished?.Invoke();
    }
}
