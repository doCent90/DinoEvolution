using UnityEngine;

public class RoadMover : MonoBehaviour
{
    [SerializeField] private RoadOverTrigger _roadEndTrigger;
    [SerializeField] private float _speed = 10f;

    private void OnEnable()
    {
        _roadEndTrigger.RoadOver += OnRoadOver;
    }

    private void OnDisable()
    {        
        _roadEndTrigger.RoadOver -= OnRoadOver;
    }

    private void OnRoadOver()
    {
        enabled = false;
    }

    private void Update()
    {
        transform.Translate(Vector3.back * _speed * Time.deltaTime);
    }
}
