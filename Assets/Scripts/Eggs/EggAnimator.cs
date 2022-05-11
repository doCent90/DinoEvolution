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
    private const string FlexAnimation = "Flex";

    private const float MaxDelay = 2f;
    private const float MinDelay = 0.5f;
    private const float Duration = 0.07f;
    private const float DurationWash = 1f;

    private void OnEnable()
    {
        _roadParent = FindObjectOfType<RoadParent>();
    }

    public void TakeNest()
    {
        _animator.SetTrigger(NestAnimation);
    }

    public void OnUVLampHeated()
    {
        float randomDelay = Random.Range(MinDelay, MaxDelay);
        Invoke(nameof(PlayFlex), randomDelay);
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

    private void PlayFlex()
    {
        _animator.SetTrigger(FlexAnimation);
    }
}
