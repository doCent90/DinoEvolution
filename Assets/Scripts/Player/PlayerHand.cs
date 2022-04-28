using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerHand : MonoBehaviour
{
    [SerializeField] private BossArea _bossArea;
    [SerializeField] private Transform _eggStackParent;
    [SerializeField] private Transform _eggStackPosition;

    private PlayerMover _playerMover;
    private PlayerAnimator _animator;

    public bool IsBusy { get; private set; } = false;
    public EggMover LastInStack { get; private set; }
    public BossArea BossArea => _bossArea;
    public PlayerMover PlayerMover => _playerMover;
    public Transform EggStackParent => _eggStackParent;
    public Transform EggStackPosition => _eggStackPosition;

    private void OnEnable()
    {
        _animator = GetComponent<PlayerAnimator>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg) && IsBusy == false)
        {
            LastInStack = egg.EggMover;
            egg.transform.parent = transform;
            egg.transform.position = _eggStackPosition.position;
            egg.OnHandTaked(this);
            IsBusy = true;
        }
    }

    public void SetLastEgg(EggMover eggMover)
    {
        LastInStack = eggMover;
    }

    public void OnEggAdded()
    {        
        _animator.Take();
    }

    public void OnHandEmpty()
    {
        IsBusy = false;
        LastInStack = null;
    }
}
