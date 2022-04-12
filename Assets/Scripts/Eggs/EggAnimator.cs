using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Egg))]
public class EggAnimator : MonoBehaviour
{
    [SerializeField] private Transform _model;

    private Egg _egg;
    private float _targetScale = 1.6f;

    private const float DUARTION = 0.07f;

    private void OnEnable()
    {
        _egg = GetComponent<Egg>();
        _egg.OtherEggTaked += OnTakedAnimation;
    }

    private void OnDisable()
    {
        _egg.OtherEggTaked -= OnTakedAnimation;
    }

    private void OnTakedAnimation()
    {
        var tween1 = _model.DOScale(_targetScale, DUARTION);
        var tween2 = _model.DOScale(1, DUARTION).SetDelay(DUARTION);
    }
}
