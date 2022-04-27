using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DinoHealthBar : MonoBehaviour
{
    [SerializeField] private UI _uI;
    [SerializeField] private Image _fillBack;
    [SerializeField] private Image _fillFront;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _speed = 0.005f;

    private Boss _boss;
    private BossAreaTrigger _areaTrigger;

    private const float Duration = 0.3f;

    private void OnEnable()
    {
        _areaTrigger = _uI.BossAreaTrigger;
        _boss = _areaTrigger.Boss;

        _boss.Won += Hide;
        _boss.Died += Hide;
        _boss.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _boss.Won -= Hide;
        _boss.Died -= Hide;
        _boss.HealthChanged -= OnHealthChanged;        
    }

    public void Show()
    {
        _canvasGroup.alpha = 1f;
        transform.DOScale(1, Duration);
    }

    private void Hide()
    {
        _canvasGroup.DOFade(0, Duration);
    }

    private void OnHealthChanged()
    {
        float currentHealth = Mathf.InverseLerp(0, _boss.HealthMax, _boss.Health);
        _fillFront.fillAmount = currentHealth;
        StartCoroutine(ShowFillBack());
    }

    private IEnumerator ShowFillBack()
    {
        var waitForSeconds = new WaitForSeconds(Duration);
        var waitForFixedUpdate = new WaitForFixedUpdate();
        yield return waitForSeconds;

        while(_fillBack.fillAmount >= _fillFront.fillAmount)
        {
            _fillBack.fillAmount -= _speed; 
            yield return waitForFixedUpdate;
        }
    }
}
