using UnityEngine;

[RequireComponent(typeof(Egg))]
[RequireComponent(typeof(EggAnimator))]
[RequireComponent(typeof(SphereCollider))]
public class EggMover : MonoBehaviour
{
    private SphereCollider _collider;
    private Transform _origParent;
    private EggAnimator _eggAnimator;
    private EggMover _leaderEgg;
    private EggMover _nextEgg;

    private float _power;
    private float _step;
    private bool _hasStack = false;

    private const float SPEED = 20f;
    private const float DELAY = 0.05f;

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
        _leaderEgg = null;
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
        _leaderEgg = followEgg;
        _hasStack = Egg.HasInStack;
        Animate();
    }

    public void Animate()
    {
        _eggAnimator.ScaleAnimation();
        Invoke(nameof(AnimateLeaderEgg), DELAY);
    }

    public void AnimateLeaderEgg()
    {
        if(_leaderEgg != null)
            _leaderEgg.Animate();
    }

    private void OnEnable()
    {
        Egg = GetComponent<Egg>();
        _collider = GetComponent<SphereCollider>();
        _eggAnimator = GetComponent<EggAnimator>();

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
        if (_hasStack == false || _leaderEgg == null)
            return;

        Vector3 position;
        Vector3 targetPosition = new Vector3(_leaderEgg.transform.position.x, _leaderEgg.transform.position.y, _leaderEgg.transform.position.z + _step);

        position = Vector3.Lerp(transform.position, targetPosition, _power);
        transform.position = position;
    }
}
