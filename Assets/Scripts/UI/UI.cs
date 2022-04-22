using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    [SerializeField] private Player _player;
    [SerializeField] private GameOver _gameOver;
    [SerializeField] private LevelsLoader _levelsLoader;
    [SerializeField] private InputController _inputController;
    [Header("Canvas")]
    [SerializeField] private CanvasGroup _gamePanel;
    [SerializeField] private CanvasGroup _startPanel;
    [SerializeField] private CanvasGroup _winPanel;
    [SerializeField] private CanvasGroup _losePanel;
    [Header("Buttons")]
    [SerializeField] private Button _retry;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _fightTap;

    private const float DURATION = 0.5f;

    public event Action FightClicked;

    private void OnEnable()
    {
        _boss.Won += OnGameLosed;
        _boss.Died += OnBossDied;
        _gameOver.Losed += OnGameLosed;
        _inputController.Clicked += StartGame;

        _retry.onClick.AddListener(Restart);
        _restart.onClick.AddListener(Restart);
        _fightTap.onClick.AddListener(OnFightCliked);
    }

    private void OnDisable()
    {
        _boss.Won -= OnGameLosed;
        _boss.Died -= OnBossDied;
        _gameOver.Losed -= OnGameLosed;
        _inputController.Clicked -= StartGame;

        _retry.onClick.RemoveListener(Restart);
        _restart.onClick.RemoveListener(Restart);
        _fightTap.onClick.RemoveListener(OnFightCliked);
    }

    private void StartGame()
    {
        _player.EnableMove();
        EnableCanvas(_gamePanel);
        DisableCanvas(_startPanel);
    }

    private void Restart()
    {
        _levelsLoader.Restart();
    }

    private void OnFightCliked()
    {
        _inputController.enabled = false;
        FightClicked?.Invoke();
    }

    private void OnBossDied()
    {
        _fightTap.enabled = false;
        EnableCanvas(_winPanel);
        DisableCanvas(_gamePanel);
    }

    private void OnGameLosed()
    {
        _fightTap.enabled = false;
        EnableCanvas(_losePanel);
        DisableCanvas(_gamePanel);
    }

    private void EnableCanvas(CanvasGroup canvasGroup)
    {
        canvasGroup.DOFade(1, DURATION);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void DisableCanvas(CanvasGroup canvasGroup)
    {
        canvasGroup.DOFade(0, DURATION);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
