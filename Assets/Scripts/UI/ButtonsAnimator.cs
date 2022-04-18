using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(UI))]
public class ButtonsAnimator : MonoBehaviour
{
    [SerializeField] private BossAreaTrigger _bossAreaTrigger;
    [SerializeField] private CanvasGroup _tapToFightButton;
    [SerializeField] private CanvasGroup _fightButton;
    [SerializeField] private CanvasGroup _tapsCanvas;
    [Header("Taps text")]
    [SerializeField] private TMP_Text[] _tapsText;

    private UI _uI;

    private readonly float _delay = 2f;
    private readonly float _delayTap = 0.35f;

    private void OnEnable()
    {
        _uI = GetComponent<UI>();
        _uI.FightClicked += DisableTapsText;
        _uI.TapToFightClicked += DisableTapToFight;
        _bossAreaTrigger.BossAreaReached += OnBossAreaTrigged;
    }

    private void OnDisable()
    {
        _uI.FightClicked -= DisableTapsText;
        _uI.TapToFightClicked -= DisableTapToFight;        
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
        Invoke(nameof(EnableTapToFight), _delay);
    }

    private void EnableTapToFight()
    {
        EnableCanvas(_tapToFightButton);
    }

    private void DisableTapToFight()
    {
        DisableCanvas(_tapToFightButton);
        Invoke(nameof(EnableFightButton), _delay);
    }

    private void EnableFightButton()
    {
        EnableCanvas(_fightButton);
        EnableTapsText();
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
