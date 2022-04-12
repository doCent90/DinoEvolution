using System;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private Nests _nests;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg))
        {
            _nests.TakeEgg(egg);
            Time.timeScale = 0.05f;
        }
    }
}
