using UnityEngine;

[RequireComponent(typeof(Egg))]
[RequireComponent(typeof(SphereCollider))]
public class EggMover : MonoBehaviour
{
    private Egg _egg;
    private SphereCollider _collider;
    private Transform _followEgg;
    private Transform _origParent;
    private Transform _nextEgg;

    private float _power;
    private float _step;
    private bool _hasStack = false;

    private const float SPEED = 20f;

    public PlayerHand PlayerHand { get; private set; }

    public void Disable()
    {
        _collider.enabled = false;
        ResetFollow();
    }

    public void ResetFollow()
    {
        transform.parent = _origParent;
        _followEgg = null;
    }

    public void SetNextEgg(Transform egg)
    {
        _nextEgg = egg;
    }

    public void OnTakedHand(PlayerHand playerHand)
    {
        PlayerHand = playerHand;
    }

    public void OnTaked(PlayerHand playerHand, Transform followEgg, float step, float power)
    {
        transform.parent = null;
        PlayerHand = playerHand;
        _step = step;
        _power = power;
        _followEgg = followEgg;
        _hasStack = _egg.HasInStack;
    }

    private void OnEnable()
    {
        _egg = GetComponent<Egg>();
        _collider = GetComponent<SphereCollider>();

        _origParent = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg))
        {
            if (egg.HasInStack == false && this == PlayerHand.LastInStack)
            {
                SetNextEgg(egg.transform);
                egg.OnNextTaked(transform, PlayerHand);
                PlayerHand.SetLastEgg(egg.Mover);
            }
            else if (egg.HasInStack == false && this != PlayerHand.LastInStack)
            {
                PlayerHand.LastInStack.SetNextEgg(egg.transform);
                egg.OnNextTaked(PlayerHand.LastInStack.transform, PlayerHand);
                PlayerHand.SetLastEgg(egg.Mover);
            }
        }
    }

    private void LateUpdate()
    {
        if (_hasStack == false || _followEgg == null)
            return;

        Vector3 position;
        Vector3 targetPosition = new Vector3(_followEgg.position.x, _followEgg.position.y, _followEgg.position.z + _step);

        position = Vector3.Lerp(transform.position, targetPosition, _power);
        transform.position = position;
    }
}
