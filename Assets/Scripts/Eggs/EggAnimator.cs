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

    private const string NestAnimation = "Nest";
    private const float Duration = 0.07f;
    private const float DurationWash = 1f;

    public void TakeNest()
    {
        _animator.SetTrigger(NestAnimation);
    }

    public void Wash()
    {
        _dirtyEgg.transform.DOScale(0, DurationWash);

        foreach (var item in _cellsDirt)
            item.Destroy(_roadParent.transform);

        ScaleAnimation();
    }

    public void ScaleAnimation()
    {
        var tween1 = _model.DOScale(_targetScale, Duration);
        var tween2 = _model.DOScale(1, Duration).SetDelay(Duration);
    }

    private void OnEnable()
    {
        _roadParent = FindObjectOfType<RoadParent>();
    }
}
