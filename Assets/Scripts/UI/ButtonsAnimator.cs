using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(UI))]
public class ButtonsAnimator : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    [SerializeField] private DinoHealthBar _healthBar;
    [SerializeField] private BossAreaTrigger _bossAreaTrigger;
    [SerializeField] private CanvasGroup _tapToFightButton;
    [SerializeField] private CanvasGroup _fightButton;
    [SerializeField] private CanvasGroup _tapsCanvas;
    [Header("Taps text")]
    [SerializeField] private TMP_Text[] _tapsText;

    private UI _uI;

    private readonly float _delay = 3f;
    private readonly float _delayTap = 0.35f;

    private void OnEnable()
    {
        _uI = GetComponent<UI>();
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

    private IEnumerator TapsAnimator()
    {
        var waitForSecond = new WaitForSeconds(_delayTap);

        while (true)
        {
            foreach (var tap in _tapsText)
            {
                tap.enabled = true;
                yield return waitForSecond;
                tap.enabled = false;
            }
        }        
    }

    private void OnBossAreaTrigged()
    {
        Invoke(nameof(EnableFightButton), _delay);
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
        StartCoroutine(TapsAnimator());
    }

    private void DisableTapsText()
    {
        DisableCanvas(_tapsCanvas);
        StopCoroutine(TapsAnimator());
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
