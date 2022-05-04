using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DinoHealthBar : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    [SerializeField] private Image _fillBack;
    [SerializeField] private Image _fillFront;
    [SerializeField] private TMP_Text _levelBoss;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _speed = 0.005f;

    private const int Number = 14; 
    private const float Duration = 0.3f;
    private const string Level = "level";

    private void OnEnable()
    {
        _boss.HealthChanged += OnHealthChanged;        
    }

    private void OnDisable()
    {
        _boss.HealthChanged -= OnHealthChanged;        
    }

    public void Show()
    {
        SetRandomLevel();
        _canvasGroup.alpha = 1f;
        transform.DOScale(1, Duration);
    }

    public void Hide()
    {
        _canvasGroup.DOFade(0, Duration);
    }

    private void SetRandomLevel()
    {
        int multiply = PlayerPrefs.GetInt(Level);
        int level = Number * multiply;
        _levelBoss.text = $"{level} lvl";
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
