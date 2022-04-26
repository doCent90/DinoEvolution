using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _gameCamera;
    [SerializeField] private CinemachineVirtualCamera _bossFightCamera;
    [Header("Triggers")]
    [SerializeField] private BossAreaTrigger _bossAreaTrigger;
    [Header("Shake Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float _amplitude;
    [SerializeField] private float _time;
    [SerializeField] private int _gain;

    private Boss _boss;
    private CinemachineVirtualCamera[] _cameras;

    private const float Delay = 1f;
    private const int MinPrioritet = 1;
    private const int MaxPrioritet = 2;

    private void OnEnable()
    {
        _cameras = GetComponentsInChildren<CinemachineVirtualCamera>();
        _boss = _bossAreaTrigger.Boss;

        _boss.SuperAttacked += OnSuperAttacked;
        _bossAreaTrigger.BossAreaReached += OnBossAreaReached;
    }

    private void OnDisable()
    {
        _boss.SuperAttacked -= OnSuperAttacked;
        _bossAreaTrigger.BossAreaReached -= OnBossAreaReached;        
    }

    private void OnSuperAttacked()
    {
        StartCoroutine(ShakeCameraSwitcher(_bossFightCamera));
    }

    private void OnFightEnable()
    {
        EnableCamera(_bossFightCamera);
    }

    private void OnBossAreaReached()
    {
        Invoke(nameof(OnFightEnable), Delay);
    }

    private void EnableCamera(CinemachineVirtualCamera mainCamera)
    {
        ResetPriority();
        mainCamera.Priority = MaxPrioritet;
    }

    private void ResetPriority()
    {
        foreach (var camera in _cameras)
        {
            camera.Priority = MinPrioritet;
        }
    }

    private IEnumerator ShakeCameraSwitcher(CinemachineVirtualCamera orig)
    {
        var waitForSeconds = new WaitForSeconds(_time);
        Vector3 originalPositions = orig.transform.localPosition;

        for (int i = 0; i < _gain; i++)
        {
            float x = Random.Range(-_amplitude, _amplitude);
            float y = Random.Range(-_amplitude, _amplitude);
            float z = Random.Range(-_amplitude, _amplitude);

            Vector3 shakePos = new Vector3(originalPositions.x + x, originalPositions.y + y, originalPositions.z + z);
            orig.transform.localPosition = shakePos;
            yield return waitForSeconds;
            orig.transform.localPosition = originalPositions;
        }
    }
}
