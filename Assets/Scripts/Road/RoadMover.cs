using System.Collections;
using UnityEngine;

public class RoadMover : MonoBehaviour
{
    [SerializeField] private RoadOverTrigger _roadEndTrigger;
    [SerializeField] private float _speed;
    [Header("Moveable objects")]
    [SerializeField] private RoadTrigger _road;

    private readonly float _backUpSpeed = -20f;
    private readonly float _disableTime = 2f;
    private float _currentSpeed;
    private float _spentTime;

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
        _currentSpeed = _backUpSpeed;
        _spentTime = 0;
    }

    private void OnRoadOver()
    {
        _currentSpeed = 0;
        _speed = _currentSpeed;

        Invoke(nameof(Disable), _disableTime);
    }

    private void Disable()
    {
        enabled = false;
    }
}
