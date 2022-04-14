using UnityEngine;

[RequireComponent(typeof(Egg))]
[RequireComponent(typeof(SphereCollider))]
public class EggMover : MonoBehaviour
{
    private SphereCollider _collider;
    private Transform _origParent;
    private EggMover _followEgg;
    private EggMover _nextEgg;

    private float _power;
    private float _step;
    private bool _hasStack = false;

    private const float SPEED = 20f;

    public Egg Egg { get; private set; }
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

    public void SetNextEgg(EggMover egg)
    {
        _nextEgg = egg;
    }

    public void OnTakedHand(PlayerHand playerHand)
    {
        PlayerHand = playerHand;
    }

    public void OnTaked(PlayerHand playerHand, EggMover followEgg, float step, float power)
    {
        transform.parent = null;
        PlayerHand = playerHand;
        _step = step;
        _power = power;
        _followEgg = followEgg;
        _hasStack = Egg.HasInStack;
    }

    private void OnEnable()
    {
        Egg = GetComponent<Egg>();
        _collider = GetComponent<SphereCollider>();

        _origParent = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg))
        {
            if (egg.HasInStack == false && this == PlayerHand.LastInStack)
            {
                SetNextEgg(egg.EggMover);
                egg.OnNextTaked(this, PlayerHand);
                PlayerHand.SetLastEgg(egg.EggMover);
            }
            else if (egg.HasInStack == false && this != PlayerHand.LastInStack)
            {
                PlayerHand.LastInStack.SetNextEgg(egg.EggMover);
                egg.OnNextTaked(PlayerHand.LastInStack, PlayerHand);
                PlayerHand.SetLastEgg(egg.EggMover);
            }
        }
    }

    private void LateUpdate()
    {
        if (_hasStack == false || _followEgg == null)
            return;

        Vector3 position;
        Vector3 targetPosition = new Vector3(_followEgg.transform.position.x, _followEgg.transform.position.y, _followEgg.transform.position.z + _step);

        position = Vector3.Lerp(transform.position, targetPosition, _power);
        transform.position = position;
    }
}
