using UnityEngine;

[RequireComponent(typeof(Egg))]
[RequireComponent(typeof(EggAnimator))]
[RequireComponent(typeof(SphereCollider))]
public class EggMover : MonoBehaviour
{
    private SphereCollider _collider;
    private EggAnimator _eggAnimator;
    private EggMover _previousEgg;
    private EggMover _nextEgg;

    private bool _hasStack = false;

    private const float Step = 0.9f;
    private const float Power = 30f;
    private const float Delay = 0.05f;

    public Egg Egg { get; private set; }
    public PlayerHand PlayerHand { get; private set; }

    public void Disable(Transform parent)
    {
        if(this == PlayerHand.LastInStack)
            PlayerHand.SetLastEgg(_previousEgg);

        // If this egg in midlle stack
        if (_nextEgg != null && _previousEgg != null)
        {
            _previousEgg.SetNextEgg(_nextEgg);
            _nextEgg.SetPreviousEgg(_previousEgg);
        }

        // If this egg is first in stack
        if(_nextEgg != null && _previousEgg == null)
        {
            _nextEgg.SetPreviousEgg(null);
            _nextEgg.transform.parent = PlayerHand.EggStackPosition;
            _nextEgg.transform.position = PlayerHand.EggStackPosition.position;
        }

        // If this egg is last in stack
        if(_nextEgg == null && _previousEgg != null)
            _previousEgg.SetNextEgg(null);

        // If this egg is single in stack
        if (_nextEgg == null && _previousEgg == null)
            PlayerHand.OnHandEmpty();

        _collider.enabled = false;
        transform.parent = parent;

        _nextEgg = null;
        _previousEgg = null;

        enabled = false;
    }

    public void SetNextEgg(EggMover egg)
    {
        _nextEgg = egg;
    }

    public void SetPreviousEgg(EggMover previousEgg)
    {
        _previousEgg = previousEgg;
    }

    public void OnTakedHand(PlayerHand playerHand)
    {
        PlayerHand = playerHand;
    }

    public void OnTaked(PlayerHand playerHand, EggMover followEgg, Transform parent)
    {
        transform.parent = parent;
        PlayerHand = playerHand;
        _hasStack = Egg.HasInStack;
        SetPreviousEgg(followEgg);
        Animate();
    }

    public void Animate()
    {
        _eggAnimator.ScaleAnimation();
        Invoke(nameof(AnimateLeaderEgg), Delay);
    }

    public void AnimateLeaderEgg()
    {
        if(_previousEgg != null)
            _previousEgg.Animate();
    }

    private void OnEnable()
    {
        Egg = GetComponent<Egg>();
        _collider = GetComponent<SphereCollider>();
        _eggAnimator = GetComponent<EggAnimator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg))
        {
            if (egg.HasInStack == false && this == PlayerHand.LastInStack)
            {
                SetNextEgg(egg.EggMover);
                egg.OnNextTaked(this, PlayerHand, PlayerHand.EggStackParent);
                PlayerHand.SetLastEgg(egg.EggMover);
            }
            else if (egg.HasInStack == false && this != PlayerHand.LastInStack)
            {
                PlayerHand.LastInStack.SetNextEgg(egg.EggMover);
                egg.OnNextTaked(PlayerHand.LastInStack, PlayerHand, PlayerHand.EggStackParent);
                PlayerHand.SetLastEgg(egg.EggMover);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_hasStack == false || _previousEgg == null)
            return;

        Vector3 position;
        Vector3 previuosEgg = _previousEgg.transform.position;

        Vector3 targetPosition = new Vector3(previuosEgg.x,
            previuosEgg.y, previuosEgg.z + Step);

        position = Vector3.Lerp(transform.position, targetPosition,
            Mathf.SmoothStep(0f, 1f, Power * Time.deltaTime));

        transform.position = position;
    }
}
