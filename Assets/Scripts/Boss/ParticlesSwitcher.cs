using UnityEngine;

[RequireComponent(typeof(BossArea))]
public class ParticlesSwitcher : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _finishParticleSystems;

    private Boss _boss;
    private BossArea _bossArea;

    private void OnEnable()
    {
        _bossArea = GetComponent<BossArea>();
        _boss = _bossArea.Boss;
        _boss.Died += PlayFinishFX;
    }

    private void OnDisable()
    {
        _boss.Died -= PlayFinishFX;
    }

    private void PlayFinishFX()
    {
        _boss.Died -= PlayFinishFX;

        foreach (ParticleSystem particle in _finishParticleSystems)
            particle.Play();
    }
}
