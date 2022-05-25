using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(UI))]
public class ButtonsAnimator : MonoBehaviour
{
    [SerializeField] private DinoHealthBar _healthBar;
    [SerializeField] private CanvasGroup _fightButton;
    [SerializeField] private CanvasGroup _tapsCanvas;
    [SerializeField] private CanvasGroup _powerBarCanvas;
    [Header("Taps text")]
    [SerializeField] private TMP_Text[] _tapsText;

    private BossAreaTrigger _bossAreaTrigger;
    private Boss _boss;
    private UI _uI;

    private const float Delay = 2f;
    private const float Duration = 0.3f;

    private void OnEnable()
    {
        _uI = GetComponent<UI>();
        _bossAreaTrigger = _uI.BossAreaTrigger;
        _boss = _bossAreaTrigger.Boss;

        _bossAreaTrigger.BossAreaReached += OnBossAreaTrigged;
    }

    private void OnDisable()
    {
        _bossAreaTrigger.BossAreaReached -= OnBossAreaTrigged;
    }

    public void HideRestart()
    {
        DisableCanvas(_fightButton);
        DisableCanvas(_tapsCanvas);
    }

    private void OnBossAreaTrigged()
    {
        Invoke(nameof(EnableFightButton), Delay);
        DisableCanvas(_powerBarCanvas);
    }

    private void EnableFightButton()
    {
        _healthBar.Show();
        EnableTapsText();
        EnableCanvas(_fightButton);
    }

    private void EnableTapsText()
    {
        EnableCanvas(_tapsCanvas);
    }

    private void EnableCanvas(CanvasGroup canvasGroup)
    {
        canvasGroup.DOFade(1, Duration);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void DisableCanvas(CanvasGroup canvasGroup)
    {
        canvasGroup.DOFade(0, Duration);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
