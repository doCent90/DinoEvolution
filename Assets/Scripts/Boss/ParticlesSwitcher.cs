using UnityEngine;

[RequireComponent(typeof(BossArea))]
public class ParticlesSwitcher : MonoBehaviour
{
    [SerializeField] private GameOver _gameOver;
    [SerializeField] private ParticleSystem[] _finishParticleSystems;
    [SerializeField] private ParticleSystem[] _fireWorksParticleSystems;

    private const float Delay = 2f;

    private void OnEnable()
    {
        _gameOver.Won += PlayFireWorks;
    }

    private void OnDisable()
    {
        _gameOver.Won -= PlayFireWorks;
    }

    private void PlayFireWorks()
    {
        PlayFX(_fireWorksParticleSystems);
        Invoke(nameof(PlayFinishFX), Delay);
    }

    private void PlayFinishFX()
    {
        PlayFX(_finishParticleSystems);
    }

    private void PlayFX(ParticleSystem[] particles)
    {
        foreach (ParticleSystem particle in particles)
            particle.Play();

    }
}
