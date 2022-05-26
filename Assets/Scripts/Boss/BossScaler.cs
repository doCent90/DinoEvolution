using UnityEngine;

public class BossScaler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _model;

    private readonly Vector3 _bigSize = new Vector3(3, 3, 3);
    private Vector3 _defaultScale;
    private float  _maxDistance;

    private const float MinDistacne = 100f;

    private void OnEnable()
    {
        _defaultScale = _model.localScale;
        _model.localScale = _bigSize;
        _maxDistance = GetDistance();
    }

    private void FixedUpdate()
    {
        float distance = GetDistance();

        if(distance > MinDistacne)
        {
            float normalDistance;
            normalDistance = Mathf.InverseLerp(MinDistacne, _maxDistance, distance);
            _model.localScale = Vector3.Lerp(_defaultScale, _bigSize, normalDistance);
        }
        else
        {
            _model.localScale = _defaultScale;
            enabled = false;
        }
    }

    private float GetDistance()
    {
        Vector3 direction = _model.position - _player.transform.position;
        float distance = direction.sqrMagnitude;

        return distance;
    }
}
