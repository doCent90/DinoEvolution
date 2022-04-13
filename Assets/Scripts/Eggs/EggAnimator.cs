using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Egg))]
public class EggAnimator : MonoBehaviour
{
    [SerializeField] private Transform _model;
    [SerializeField] private MeshRenderer _dirtyEgg;
    [SerializeField] private CellDestroy[] _cellsDirt;

    private Egg _egg;
    private float _targetScale = 1.6f;

    private const float DURATION = 0.07f;
    private const float DURATION_WASH = 1f;

    private void OnEnable()
    {
        _egg = GetComponent<Egg>();

        _egg.Washed += OnWashed;
        _egg.Triggered += OnTakedAnimation;
    }

    private void OnDisable()
    {
        _egg.Washed -= OnWashed;
        _egg.Triggered -= OnTakedAnimation;
    }

    private void OnWashed()
    {
        _dirtyEgg.transform.DOScale(0, DURATION_WASH);

        foreach (var item in _cellsDirt)
            item.Destroy();

        OnTakedAnimation();
    }

    private void OnTakedAnimation()
    {
        var tween1 = _model.DOScale(_targetScale, DURATION);
        var tween2 = _model.DOScale(1, DURATION).SetDelay(DURATION);
    }
}
