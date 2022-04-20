using System;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SphereCollider))]
public class Egg : MonoBehaviour
{
    [SerializeField] private EggLevel level;
    [SerializeField] private EggType _eggType;
    [SerializeField] private EggMover _eggMover;
    [SerializeField] private Transform _eggParent;
    [SerializeField] private EggAnimator _eggAnimator;
    [Header("Meshes")]
    [SerializeField] private MeshRenderer _nest;
    [SerializeField] private MeshRenderer _dirt;

    private EggData _data;
    private EggModel _model;
    private RoadParent _roadParent;
    private MeshRenderer _cleanEgg;

    public EggMover EggMover => _eggMover;
    public PlayerHand PlayerHand { get; private set; }

    public bool HasInStack { get; private set; } = false;
    public bool HaveNest { get; private set; } = false;
    public bool WasWashed { get; private set; } = false;
    public bool WasUVLightsHeated { get; private set; } = false;

    private void OnEnable()
    {
        InitType();
        _roadParent = GetComponentInParent<RoadParent>();
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

    public void Destroy()
    {
        _eggMover.Disable(_roadParent.transform);
        Die();
    }

    public void OnHandTaked(PlayerHand playerHand)
    {
        HasInStack = true;
        PlayerHand = playerHand;
        _eggMover.OnTakedHand(playerHand);
    }

    public void OnNextTaked(EggMover follwerEgg, PlayerHand playerHand, Transform parent)
    {
        HasInStack = true;
        PlayerHand = playerHand;
        _eggMover.OnTaked(PlayerHand, follwerEgg, parent);
    }

    public void Animate()
    {
        _eggAnimator.ScaleAnimation();
    }

    private void InitType()
    {
        _data = _eggType.GetTypeData(level);
        var model = _data.EggModelType;

        _model = Instantiate(model, _eggParent.position, Quaternion.identity, _eggParent);
        _cleanEgg = _model.GetComponent<MeshRenderer>();
    }

    private void SpawnDino()
    {
        var dino = Instantiate(_data.Dino, transform.position, Quaternion.identity);
        dino.Init(PlayerHand.BossArea, _data.Health, _data.Damage);
    }

    private void ToGrinder()
    {
        SpawnDino();
        _nest.enabled = false;
        _cleanEgg.enabled = false;
        _model.DestroyCells();
    }

    private void TypeUpgrade()
    {
        level++;
        Destroy(_model.gameObject);
        InitType();
        Animate();
    }

    private void UVLampHeating()
    {
        if (WasUVLightsHeated == false)
        {
            WasUVLightsHeated = true;
            _cleanEgg.enabled = false;
            _model.EnableCleanCells();
            Animate();
        }
    }

    private void Wash()
    {
        if(WasWashed == false)
        {
            WasWashed = true;
            _eggAnimator.Wash();
        }
    }

    private void TakeNest()
    {
        if(HaveNest == false)
        {
            HaveNest = true;
            _nest.enabled = true;
            _eggAnimator.TakeNest();
        }
    }

    private void Die()
    {
        _nest.enabled = false;
        _dirt.enabled = false;
        _cleanEgg.enabled = false;
        _model.DestroyCells();

        Destroy(gameObject, 3f);
    }
}
