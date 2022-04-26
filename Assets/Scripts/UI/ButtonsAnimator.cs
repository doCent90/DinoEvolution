using TMPro;
using UnityEngine;

[RequireComponent(typeof(UI))]
public class ButtonsAnimator : MonoBehaviour
{
    [SerializeField] private DinoHealthBar _healthBar;
    [SerializeField] private BossAreaTrigger _bossAreaTrigger;
    [SerializeField] private CanvasGroup _fightButton;
    [SerializeField] private CanvasGroup _tapsCanvas;
    [Header("Taps text")]
    [SerializeField] private TMP_Text[] _tapsText;
    [SerializeField] private bool _tapsCanDisable = false;

    private Boss _boss;
    private UI _uI;

    private const float Delay = 4f;

    private void OnEnable()
    {
        _uI = GetComponent<UI>();
        _boss = _bossAreaTrigger.Boss;

        _boss.Died += HideRestart;
        _uI.FightClicked += DisableTapsText;
        _bossAreaTrigger.BossAreaReached += OnBossAreaTrigged;
    }

    private void OnDisable()
    {
        _boss.Died -= HideRestart;
        _uI.FightClicked -= DisableTapsText;
        _bossAreaTrigger.BossAreaReached -= OnBossAreaTrigged;
    }

    private void OnBossAreaTrigged()
    {
        Invoke(nameof(EnableFightButton), Delay);
    }

    private void EnableFightButton()
    {
        _healthBar.Show();
        EnableTapsText();
        EnableCanvas(_fightButton);
    }

    private void HideRestart()
    {
        DisableCanvas(_fightButton);
    }

    private void EnableTapsText()
    {
        EnableCanvas(_tapsCanvas);
    }

    private void DisableTapsText()
    {
        if(_tapsCanDisable)
            DisableCanvas(_tapsCanvas);
    }

    private void EnableCanvas(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void DisableCanvas(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
