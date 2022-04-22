using System.Collections;
using UnityEngine;

public class RoadMover : MonoBehaviour
{
    [SerializeField] private RoadOverTrigger _roadEndTrigger;
    [SerializeField] private float _speed;
    [Header("Moveable objects")]
    [SerializeField] private RoadTrigger _road;

    private float _currentSpeed;
    private float _spentTime;

    private const float BackUpSpeed = -20f;
    private const float DisableTime = 2f;

    private void OnEnable()
    {
        _roadEndTrigger.RoadOver += OnRoadOver;
    }

    private void OnDisable()
    {        
        _roadEndTrigger.RoadOver -= OnRoadOver;
    }

    private void Update()
    {
        if(_spentTime < 1)
            _spentTime += Time.deltaTime / 3;

        _currentSpeed = Mathf.Lerp(_currentSpeed, _speed, _spentTime);

        transform.Translate(Vector3.forward * _currentSpeed * Time.deltaTime);
        _road.transform.Translate(Vector3.back * _currentSpeed * Time.deltaTime);
    }

    public void OnTrapDone()
    {
        _currentSpeed = BackUpSpeed;
        _spentTime = 0;
    }

    private void OnRoadOver()
    {
        _currentSpeed = 0;
        _speed = _currentSpeed;

        Invoke(nameof(Disable), DisableTime);
    }

    private void Disable()
    {
        enabled = false;
    }
}
