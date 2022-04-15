using System;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SphereCollider))]
public class Egg : MonoBehaviour
{
    [SerializeField] private EggLevel _eggLevel;
    [SerializeField] private EggType _eggType;
    [SerializeField] private EggMover _eggMover;
    [SerializeField] private Transform _eggParent;
    [SerializeField] private EggAnimator _eggAnimator;
    [SerializeField] private MeshRenderer _dino;
    [SerializeField] private MeshRenderer _nest;

    private float _health;
    private float _damage;
    private int _upgradeCount;

    private EggModel _eggModel;
    private MeshRenderer _cleanEgg;

    public EggMover EggMover => _eggMover;
    public PlayerHand PlayerHand { get; private set; }

    public bool HasInStack { get; private set; } = false;
    public bool HaveNest { get; private set; } = false;
    public bool WasWashed { get; private set; } = false;
    public bool WasLightsHeated { get; private set; } = false;

    public event Action<FinalPlace> BossAreaReached;

    private void OnEnable()
    {
        Init();
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

        if (other.TryGetComponent(out ColorChangeGate colorChangeGate))
            TypeUpgrade();
    }

    public void Sort(Transform parent)
    {
        _eggMover.Disable(parent);
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
        PlayerHand = playerHand;
        _eggMover.OnTakedHand(playerHand);
    }

    public void OnNextTaked(EggMover follwerEgg, PlayerHand playerHand)
    {
        HasInStack = true;
        PlayerHand = playerHand;
        _eggMover.OnTaked(PlayerHand, follwerEgg);
    }

    public void Animate()
    {
        _eggAnimator.ScaleAnimation();
    }

    private void Init()
    {
        _eggType.Init(_eggLevel);

        _health = _eggType.Health;
        _damage = _eggType.Damage;
        var model = _eggType.EggModelType;

        _eggModel = Instantiate(model, _eggParent.position, Quaternion.identity, _eggParent);
        _cleanEgg = _eggModel.GetComponent<MeshRenderer>();
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

    private void TypeUpgrade()
    {
        _eggLevel++;
        Destroy(_eggModel.gameObject);
        Init();
        Animate();
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
}
