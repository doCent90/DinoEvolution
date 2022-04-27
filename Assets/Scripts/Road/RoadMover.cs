using UnityEngine;

public class RoadMover : MonoBehaviour
{
    [SerializeField] private RoadOverTrigger _roadEndTrigger;
    [Header("Moveable objects")]
    [SerializeField] private RoadTrigger _road;

    private float _currentSpeed;
    private float _spentTime;
    private float _speed = 6f;

    private const float ToolsSpeed = 4f;
    private const float DisableTime = 2f;
    private const float Acceleration = 3;
    private const float BackUpSpeed = -20f;
    private const float SortGateSpeed = 4f;

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
            _spentTime += Time.deltaTime / Acceleration;

        _currentSpeed = Mathf.Lerp(_currentSpeed, _speed, _spentTime);

        transform.Translate(_currentSpeed * Time.deltaTime * Vector3.forward);
        _road.transform.Translate(_currentSpeed * Time.deltaTime * Vector3.back);
    }

    public void OnTrapWorked()
    {
        SetSpeed(ref _currentSpeed, BackUpSpeed);
    }

    public void OnToolsWorked()
    {
        SetSpeed(ref _currentSpeed, ToolsSpeed);
    }

    public void OnSortGateReached()
    {
        SetSpeed(ref _speed, SortGateSpeed);
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

    private void SetSpeed(ref float speed, float targetSpeed)
    {
        speed = targetSpeed;
        _spentTime = 0;
    }
}
