using DG.Tweening;
using UnityEngine;

public class HealthBarAnimator : MonoBehaviour
{
    [SerializeField] private UI _uI;
    [SerializeField] private Transform _bar;

    private bool _hasEncreased = false;

    private const float Duration = 0.07f;
    private const float TargetSize = 1.1f;

    private void OnEnable()
    {
        _uI.FightClicked += IncreaseSize;
    }

    private void OnDisable()
    {
        _uI.FightClicked -= IncreaseSize;
    }

    private void IncreaseSize()
    {
        if(_hasEncreased == false)
        {
            _hasEncreased = true;
            var tween = _bar.DOScale(TargetSize, Duration);
            tween.OnComplete(SetDefaultSize);
        }
    }

    private void SetDefaultSize()
    {
        _bar.DOScale(1, Duration);
        _hasEncreased = false;
    }
}
