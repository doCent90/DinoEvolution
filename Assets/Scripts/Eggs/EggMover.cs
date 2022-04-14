using UnityEngine;

[RequireComponent(typeof(Egg))]
[RequireComponent(typeof(SphereCollider))]
public class EggMover : MonoBehaviour
{

    private Player _player;
    private FinalPlace _place;
    private PlayerHand _playerHand;
    private Transform _origParent;
    private SphereCollider _collider;
    private Transform _followPosition;

    private float _power;
    private float _step;
    private bool _bossAreaReached = false;

    private const float SPEED = 20f;

    public void Disable()
    {
        _collider.enabled = false;
        ResetFollow();
    }

    public void ResetFollow()
    {
        transform.parent = _origParent;
        _player = null;
        _playerHand = null;
        _followPosition = null;
    }

    public void OnTaked(Player player, PlayerHand playerHand, Transform followPosition, bool firstEgg, float step, float power)
    {
        transform.parent = null;

        if (firstEgg)
            _power = 1;
        else
            _power = power;

        _step = step;
        _player = player;
        _playerHand = playerHand;
        _followPosition = followPosition;
    }

    public void OnBossAreaReached(FinalPlace place)
    {
        _place = place;
        _bossAreaReached = true;
        transform.parent = place.transform;
    }

    private void OnEnable()
    {
        _origParent = transform.parent;
    }

    private void LateUpdate()
    {
        if (_player == null)
            return;

        Vector3 position;

        if (_bossAreaReached == false)
        {
            Vector3 targetPosition = new Vector3(_followPosition.position.x, _followPosition.position.y, _followPosition.position.z + _step);
            position = Vector3.Lerp(transform.position, targetPosition, _power);
            transform.position = position;
        }
        //else
        //{
        //    Vector3 targetPosition = _place.GetPlacePosition().position;
        //    position = Vector3.MoveTowards(transform.position, targetPosition, SPEED * Time.deltaTime);
        //}
    }
}
