using System;
using UnityEngine;

public class RoadOverTrigger : MonoBehaviour
{
    public event Action RoadOver;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HandTrigger handTrigger))
            RoadOver?.Invoke();
    }
}
