using System;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SphereCollider))]
public class Egg : MonoBehaviour
{
    [SerializeField] private EggType _eggType;
    [SerializeField] private Transform _eggParent;
    [SerializeField] private MeshRenderer _dino;
    [SerializeField] private MeshRenderer _nest;

    private float _health;
    private float _damage;

    private EggModel _eggModel;
    private MeshRenderer _cleanEgg;
    private SphereCollider _sphereCollider;

    public Player Player { get; private set; }
    public PlayerHand PlayerHand { get; private set; }

    public bool HasInStack { get; private set; } = false;
    public bool HaveNest { get; private set; } = false;
    public bool WasWashed { get; private set; } = false;
    public bool WasLightsHeated { get; private set; } = false;

    public event Action Washed;
    public event Action Triggered;
    public event Action<EggModel> UVHeated;
    public event Action<FinalPlace> BossAreaReached;
    public event Action<Transform, float, float> Taked;

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

    public void OnTaked(Player player, PlayerHand playerHand, Transform followPosition, float step, float time)
    {
        HasInStack = true;
        Player = player;
        PlayerHand = playerHand;
        Taked?.Invoke(followPosition, step, time);
    }

    public void Animate()
    {
        Triggered?.Invoke();
    }

    public void MoveToPlace(FinalPlace place)
    {
        Animate();
        BossAreaReached?.Invoke(place);
        _sphereCollider.enabled = false;
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

    private void ToHatch()
    {
        if(WasWashed && WasLightsHeated && HaveNest)
        {
            Animate();
            _dino.enabled = true;
            _nest.enabled = false;
            _cleanEgg.enabled = false;
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
        UVHeated?.Invoke(_eggModel);
    }

    private void Wash()
    {
        WasWashed = true;
        Washed?.Invoke();
    }

    private void TakeNest()
    {
        Animate();
        HaveNest = true;
        _nest.enabled = true;
    }

    private void Die()
    {
        Destroy(gameObject, 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Washer washer))
            Wash();

        if(other.TryGetComponent(out UVLamp uFLamp))
            UVLampHeating();

        if(other.TryGetComponent(out Grinder grinder))
            ToHatch();

        if (other.TryGetComponent(out NestGate nestGate))
            TakeNest();

        if (other.TryGetComponent(out FinalPlace nest))
            MoveToPlace(nest);
    }
}
