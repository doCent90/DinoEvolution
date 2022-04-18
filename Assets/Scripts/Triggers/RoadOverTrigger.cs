using System;
using UnityEngine;

public class RoadOverTrigger : MonoBehaviour
{
    public event Action RoadOver;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            RoadOver?.Invoke();
    }
}
