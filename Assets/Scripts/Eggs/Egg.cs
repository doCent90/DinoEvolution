using System;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SphereCollider))]
public class Egg : MonoBehaviour
{
    [SerializeField] private EggType _eggType;
    [SerializeField] private EggMover _eggMover;
    [SerializeField] private Transform _eggParent;
    [SerializeField] private EggAnimator _eggAnimator;
    [SerializeField] private MeshRenderer _dino;
    [SerializeField] private MeshRenderer _nest;

    private float _health;
    private float _damage;
    private readonly float _power = 0.4f;
    private readonly float _eggStackStep = 0.8f;

    private EggModel _eggModel;
    private PlayerHand _playerHand;
    private MeshRenderer _cleanEgg;
    private SphereCollider _sphereCollider;

    public EggMover Mover => _eggMover;

    public bool HasInStack { get; private set; } = false;
    public bool HaveNest { get; private set; } = false;
    public bool WasWashed { get; private set; } = false;
    public bool WasLightsHeated { get; private set; } = false;

    public event Action<FinalPlace> BossAreaReached;

    public void ResetEgg()
    {
        _eggMover.Disable();
    }

    public void TakeDamage(float damage)
    {
        Animate();

        if(_health < damage)
            Die();
        else
            _health -= damage;

        if(_health <= 0)
            Die();
    }

    public void OnHandTaked(PlayerHand playerHand)
    {
        HasInStack = true;
        _playerHand = playerHand;
        _eggMover.OnTakedHand(playerHand);
    }

    public void OnNextTaked(Transform follwer, PlayerHand playerHand)
    {
        HasInStack = true;
        _playerHand = playerHand;
        _eggMover.OnTaked(_playerHand, follwer, _eggStackStep, _power);
    }

    public void Animate()
    {
        _eggAnimator.ScaleAnimation();
    }

    private void OnEnable()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        Init();
    }

    private void Init()
    {
        _health = _eggType.Health;
        _damage = _eggType.Damage;

        var model = _eggType.Init();
        var egg = Instantiate(model, _eggParent.position, Quaternion.identity, _eggParent);

        _eggModel = egg;
        _cleanEgg = egg.GetComponent<MeshRenderer>();
    }

    private void ToGrinder()
    {
        if(WasWashed && WasLightsHeated && HaveNest)
        {
            _dino.enabled = true;
            _nest.enabled = false;
            _cleanEgg.enabled = false;
            _eggModel.DestroyCells();
        }
        else
        {
            Die();
            Animate();
        }
    }

    private void UVLampHeating()
    {
        WasLightsHeated = true;
        _cleanEgg.enabled = false;
        _eggModel.EnableCleanCells();
        Animate();
    }

    private void Wash()
    {
        WasWashed = true;
        _eggAnimator.Wash();
    }

    private void TakeNest()
    {
        HaveNest = true;
        _nest.enabled = true;
        _eggAnimator.TakeNest();
    }

    private void Die()
    {
        Destroy(gameObject, 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out WashGate washer))
            Wash();

        if(other.TryGetComponent(out UVLamp uFLamp))
            UVLampHeating();

        if(other.TryGetComponent(out Grinder grinder))
            ToGrinder();

        if (other.TryGetComponent(out NestGate nestGate))
            TakeNest();
    }
}
