using UnityEngine;

public class RoadMover : MonoBehaviour
{
    [SerializeField] private RoadOverTrigger _roadEndTrigger;
    [Header("Moveable objects")]
    [SerializeField] private RoadTrigger _road;

    private bool _hasOnTrap = false;
    private float _currentSpeed;
    private float _spentTime;
    private float _speed = 5f;

    private const float ToolsSpeed = 3.2f;
    private const float DisableTime = 2f;
    private const float Acceleration = 3;
    private const float BackUpSpeed = -25f;
    private const float SortGateSpeed = 3f;
    private const float SpentTimeDelay = -0.1f;

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

    public void OnTrapWorked(float dealy)
    {
        _hasOnTrap = true;
        SetSpeed(ref _currentSpeed, BackUpSpeed);
        Invoke(nameof(ResetAfterTrap), dealy);
    }

    public void OnToolsWorked()
    {
        if(_hasOnTrap == false)
            SetSpeed(ref _currentSpeed, ToolsSpeed, SpentTimeDelay);
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

    private void ResetAfterTrap()
    {
        _hasOnTrap = false;
    }

    private void Disable()
    {
        enabled = false;
    }

    private void SetSpeed(ref float speed, float targetSpeed, float spentTime = 0)
    {
        speed = targetSpeed;
        _spentTime = spentTime;
    }
}
