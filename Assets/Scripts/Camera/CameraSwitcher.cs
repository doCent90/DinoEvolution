using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _gameCamera;
    [SerializeField] private CinemachineVirtualCamera _roadOverCamera;
    [SerializeField] private CinemachineVirtualCamera _nestsAreaCamera;
    [Header("Triggers")]
    [SerializeField] private RoadOverTrigger _roadOverTrigger;
    [SerializeField] private NestsAreaTrigger _nestsAreaTrigger;

    private CinemachineVirtualCamera[] _cameras;

    private const float DELAY = 2f;
    private const int MIN_PRIORITET = 1;
    private const int MAX_PRIORITET = 2;

    private void OnEnable()
    {
        _cameras = GetComponentsInChildren<CinemachineVirtualCamera>();

        _roadOverTrigger.RoadOver += OnRoadOver;
        _nestsAreaTrigger.NestsReached += OnNestsReached;
    }

    private void OnDisable()
    {
        _roadOverTrigger.RoadOver -= OnRoadOver;
        _nestsAreaTrigger.NestsReached -= OnNestsReached;        
    }

    private void OnRoadOver()
    {
        SetPriority(MAX_PRIORITET, _roadOverCamera);
    }

    private void OnNestsReached()
    {
        SetPriority(MAX_PRIORITET, _nestsAreaCamera);
    }

    private void SetPriority(int prioritet, CinemachineVirtualCamera mainCamera)
    {
        ResetPriority();
        mainCamera.Priority = prioritet;
    }

    private void ResetPriority()
    {
        foreach (var camera in _cameras)
        {
            camera.Priority = MIN_PRIORITET;
        }
    }

    private IEnumerator TimerToSwitch(CinemachineVirtualCamera camera)
    {
        var waitForSeconds = new WaitForSeconds(DELAY);
        yield return waitForSeconds;
        SetPriority(MAX_PRIORITET, camera);
        yield return waitForSeconds;
        camera.LookAt = null;
    }
}
