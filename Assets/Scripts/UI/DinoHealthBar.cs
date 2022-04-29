using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DinoHealthBar : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    [SerializeField] private Image _fillBack;
    [SerializeField] private Image _fillFront;
    [SerializeField] private GameOver _gameOver;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _speed = 0.005f;


    private const float Duration = 0.3f;

    private void OnEnable()
    {
        _gameOver.Won += Hide;
        _gameOver.Losed += Hide;
        _boss.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _gameOver.Won -= Hide;
        _gameOver.Losed -= Hide;
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
