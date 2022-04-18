using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _gameCamera;
    [SerializeField] private CinemachineVirtualCamera _bossAreaCamera;
    [SerializeField] private CinemachineVirtualCamera _bossFightCamera;
    [Header("Triggers")]
    [SerializeField] private BossAreaTrigger _bossAreaTrigger;
    [SerializeField] private UI _uI;

    private CinemachineVirtualCamera[] _cameras;

    private const int MIN_PRIORITET = 1;
    private const int MAX_PRIORITET = 2;

    private void OnEnable()
    {
        _cameras = GetComponentsInChildren<CinemachineVirtualCamera>();
        _uI.TapToFightClicked += OnFightEnable;
        _bossAreaTrigger.BossAreaReached += OnBossAreaReached;
    }

    private void OnDisable()
    {
        _uI.TapToFightClicked -= OnFightEnable;
        _bossAreaTrigger.BossAreaReached -= OnBossAreaReached;        
    }

    private void OnFightEnable()
    {
        EnableCamera(_bossFightCamera);
    }

    private void OnBossAreaReached()
    {
        EnableCamera( _bossAreaCamera);
    }

    private void EnableCamera(CinemachineVirtualCamera mainCamera)
    {
        ResetPriority();
        mainCamera.Priority = MAX_PRIORITET;
    }

    private void ResetPriority()
    {
        foreach (var camera in _cameras)
        {
            camera.Priority = MIN_PRIORITET;
        }
    }
}
