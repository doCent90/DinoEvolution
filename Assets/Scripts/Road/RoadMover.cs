using System.Collections;
using UnityEngine;

public class RoadMover : MonoBehaviour
{
    [SerializeField] private RoadOverTrigger _roadEndTrigger;
    [SerializeField] private float _speed;

    private readonly float _baackUpSpeed = -20f;
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
        transform.Translate(Vector3.back * _currentSpeed * Time.deltaTime);
    }


    public void OnTrapDone()
    {
        _currentSpeed = _baackUpSpeed;
        _spentTime = 0;
    }

    private void OnRoadOver()
    {
        enabled = false;
    }
}
