using UnityEngine;

[RequireComponent(typeof(Egg))]
public class EggMover : MonoBehaviour
{
    private Egg _egg;
    private Player _player;
    private Transform _followPosition;

    private float _time;
    private float _step;

    private void OnEnable()
    {
        _egg = GetComponent<Egg>();
        _egg.Taked += OnTaked;
    }

    private void OnDisable()
    {
        _egg.Taked -= OnTaked;
    }

    private void OnTaked(Transform followPosition, float step, float time)
    {
        transform.parent = null;
        _time = time;
        _step = step;
        _player = _egg.Player;
        _followPosition = followPosition;
    }

    private void LateUpdate()
    {
        if (_player == null)
            return;

        Vector3 targetPosition = new Vector3(_followPosition.position.x, _followPosition.position.y, _followPosition.position.z + _step);
        Vector3 position = Vector3.Lerp(transform.position, targetPosition, _time);

        transform.position = position;
    }
}
