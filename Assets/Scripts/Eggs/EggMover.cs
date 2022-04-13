using UnityEngine;

[RequireComponent(typeof(Egg))]
public class EggMover : MonoBehaviour
{
    [SerializeField] private bool _firstEgg = false;

    private Egg _egg;
    private Nest _nest;
    private Player _player;
    private Transform _followPosition;

    private float _time;
    private float _step;
    private bool _nestReached = false;

    private const float SPEED = 20f;

    private void OnEnable()
    {
        _egg = GetComponent<Egg>();
        _egg.Taked += OnTaked;
        _egg.NestsReached += OnNestsReached;
    }

    private void OnDisable()
    {
        _egg.Taked -= OnTaked;
        _egg.NestsReached -= OnNestsReached;
    }

    private void OnNestsReached(Nest nest)
    {
        _nest = nest;
        _nestReached = true;
        transform.parent = nest.transform;
    }

    private void OnTaked(Transform followPosition, float step, float time)
    {
        transform.parent = null;

        if (_firstEgg)
            _time = 1;
        else
            _time = time;

        _step = step;
        _player = _egg.Player;
        _followPosition = followPosition;
    }

    private void LateUpdate()
    {
        if (_player == null)
            return;

        Vector3 position;

        if (_nestReached == false)
        {
            Vector3 targetPosition = new Vector3(_followPosition.position.x, _followPosition.position.y, _followPosition.position.z + _step);
            position = Vector3.Lerp(transform.position, targetPosition, _time);
        }
        else
        {
            Vector3 targetPosition = _nest.GetNestPosition().position;
            position = Vector3.MoveTowards(transform.position, targetPosition, SPEED * Time.deltaTime);
        }

        transform.position = position;
    }
}
