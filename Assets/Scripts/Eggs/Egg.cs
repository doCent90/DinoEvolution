using UnityEngine;

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
        _roadParent = GetComponentInParent<RoadParent>();
        _gameOver = _tools.GameOver;

        InitType();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out UVLamp uFLamp) && WasUVLightsHeated == false)
            HeatUVLamp();

        if (other.TryGetComponent(out NestGate nestGate) && HaveNest == false)
            TakeNest(nestGate);

        if(other.TryGetComponent(out WashGate washer) && WasWashed == false)
            Wash();

        if (other.TryGetComponent(out EggUpgradeGate colorChangeGate))
            TypeUpgrade();

        if(other.TryGetComponent(out CylinderTrigger cylinder))
            OnCylinderTriggered();

        if(other.TryGetComponent(out Grinder grinder))
            Hatch();
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
        PlayerHand.OnEggLost();
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
        _model.Init(_roadParent);
        _cleanEgg = _model.MeshRendererEgg;
    }

    private void SpawnDino()
    {
        DinoMini dino = Instantiate(_data.Dino, transform.position, Quaternion.identity);
        dino.Init(_gameOver, PlayerHand.BossArea, _data.Health, _data.Damage);
    }

    private void OnCylinderTriggered()
    {
        _eggAnimator.Jump();
    }

    private void Hatch()
    {
        SpawnDino();
        _nest.enabled = false;
        _cleanEgg.enabled = false;
        _eggMover.enabled = false;
        _eggAnimator.Stop();
        _model.DestroyCells();
    }

    private void TypeUpgrade()
    {
        level++;
        Destroy(_model.gameObject);
        InitType();
        Animate();
    }

    private void HeatUVLamp()
    {
        WasUVLightsHeated = true;
        _cleanEgg.enabled = false;
        _model.EnableCleanCells();
        _eggAnimator.OnUVLampHeated();
        Animate();
    }

    private void Wash()
    {
        WasWashed = true;
        _eggAnimator.Wash();
        _model.IncreaseScale();
    }

    private void TakeNest(NestGate nestGate)
    {
        nestGate.GiveNest();
        HaveNest = true;
        _nest.enabled = true;
        _eggAnimator.TakeNest();
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
