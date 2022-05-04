using UnityEngine;

[RequireComponent(typeof(BossArea))]
public class ParticlesSwitcher : MonoBehaviour
{
    [SerializeField] private GameOver _gameOver;
    [SerializeField] private ParticleSystem[] _finishParticleSystems;

    private void OnEnable()
    {
        _gameOver.Won += PlayFinishFX;
    }

    private void OnDisable()
    {
        _gameOver.Won -= PlayFinishFX;
    }

    private void PlayFinishFX()
    {
        foreach (ParticleSystem particle in _finishParticleSystems)
            particle.Play();
    }
}
