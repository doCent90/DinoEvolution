using System;
using UnityEngine;

namespace RunnerMovementSystem
{
    public class TransitionTrigger : MonoBehaviour
    {
        public event Action<Collider> Triggered;

        private void OnTriggerEnter(Collider other)
        {
            Triggered?.Invoke(other);
        }
    }
}
