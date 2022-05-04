using UnityEngine;

public class SuperAttackFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void PlayFx() //invoke from animation
    {
        _particleSystem.Play();
    }
}
