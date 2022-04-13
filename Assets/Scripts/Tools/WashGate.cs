using UnityEngine;

public class WashGate : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particleSystems;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg))
            foreach (var item in _particleSystems)
                item.Play();
    }
}
