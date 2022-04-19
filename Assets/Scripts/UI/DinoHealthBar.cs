using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DinoHealthBar : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    [SerializeField] private Image _fillBack;
    [SerializeField] private Image _fillFront;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _speed = 0.008f;

    private readonly float _delay = 0.6f;

    private const float DURATION = 1f;

    private void OnEnable()
    {
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
        transform.DOScale(1, DURATION);
    }

    private void Hide()
    {
        Invoke(nameof(DisableCanvas), _delay);
    }

    private void DisableCanvas()
    {
        _canvasGroup.alpha = 0;
    }

    private void OnHealthChanged()
    {
        var currentHealth = Mathf.InverseLerp(0, _boss.HealthMax, _boss.Health);
        _fillFront.fillAmount = currentHealth;
        StartCoroutine(ShowFillBack(currentHealth));
    }

    private IEnumerator ShowFillBack(float health)
    {
        var waitForSeconds = new WaitForSeconds(_delay);
        var waitForFixedUpdate = new WaitForFixedUpdate();
        yield return waitForSeconds;

        while(_fillBack.fillAmount >= _fillFront.fillAmount)
        {
            _fillBack.fillAmount -= _speed; 
            yield return waitForFixedUpdate;
        }
    }
}
