using System;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SphereCollider))]
public class Egg : MonoBehaviour
{
    [SerializeField] private EggLevel level;
    [SerializeField] private Tools _tools;
    [SerializeField] private EggType _eggType;
    [SerializeField] private EggMover _eggMover;
    [SerializeField] private Transform _eggParent;
    [SerializeField] private EggAnimator _eggAnimator;
    [Header("Meshes")]
    [SerializeField] private MeshRenderer _nest;
    [SerializeField] private MeshRenderer _dirt;

    private EggData _data;
    private EggModel _model;
    private GameOver _gameOver;
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
        _gameOver = _tools.GameOver;
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
            TakeNest(nestGate);

        if (other.TryGetComponent(out EggUpgradeGate colorChangeGate))
            TypeUpgrade();
    }

    public void Sort(Transform parent)
    {
        _eggMover.Disable(parent);
    }

    public void Sell(Transform parent)
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
        PlayerHand.OnEggAdded();
        _eggMover.OnTakedHand(playerHand);
    }

    public void OnNextTaked(EggMover follwerEgg, PlayerHand playerHand, Transform parent)
    {
        HasInStack = true;
        PlayerHand = playerHand;
        PlayerHand.OnEggAdded();
        _eggMover.OnTaked(PlayerHand, follwerEgg, parent);
    }

    public void Animate()
    {
        _eggAnimator.ScaleAnimation();
    }

    private void InitType()
    {
        _data = _eggType.GetTypeData(level);
        EggModel model = _data.EggModelType;

        _model = Instantiate(model, _eggParent.position, Quaternion.identity, _eggParent);
        _cleanEgg = _model.GetComponent<MeshRenderer>();
    }

    private void SpawnDino()
    {
        DinoMini dino = Instantiate(_data.Dino, transform.position, Quaternion.identity);
        dino.Init(_gameOver, PlayerHand.BossArea, _data.Health, _data.Damage);
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

    private void TakeNest(NestGate nestGate)
    {
        if(HaveNest == false)
        {
            nestGate.GiveNest();
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
