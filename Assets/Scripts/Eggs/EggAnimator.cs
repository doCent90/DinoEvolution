using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Egg))]
public class EggAnimator : MonoBehaviour
{
    [SerializeField] private Transform _model;
    [SerializeField] private Animator _animator;
    [SerializeField] private MeshRenderer _dirtyEgg;
    [SerializeField] private CellDestroy[] _cellsDirt;

    private RoadParent _roadParent;
    private float _targetScale = 1.6f;

    private const string NEST = "Nest";
    private const float DURATION = 0.07f;
    private const float DURATION_WASH = 1f;

    public void TakeNest()
    {
        _animator.SetTrigger(NEST);
    }

    public void Wash()
    {
        _dirtyEgg.transform.DOScale(0, DURATION_WASH);

        foreach (var item in _cellsDirt)
            item.Destroy(_roadParent.transform);

        ScaleAnimation();
    }

    public void ScaleAnimation()
    {
        var tween1 = _model.DOScale(_targetScale, DURATION);
        var tween2 = _model.DOScale(1, DURATION).SetDelay(DURATION);
    }

    private void OnEnable()
    {
        _roadParent = FindObjectOfType<RoadParent>();
    }
}
