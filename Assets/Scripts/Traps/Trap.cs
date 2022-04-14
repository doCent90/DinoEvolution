using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [Range(1, 5)]
    [SerializeField] private int _damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Egg egg))
            egg.TakeDamage(_damage);
    }
}
