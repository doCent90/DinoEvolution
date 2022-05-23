using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameOver _gameOver;
    [SerializeField] private EggsCounter _eggsCounter;
    [SerializeField] private LevelsLoader _levelsLoader;
    [SerializeField] private BossAreaTrigger _bossAreaTrigger;
    [Header("Canvas Prefab Components")]
    [SerializeField] private PowerBarView _powerBar;
    [SerializeField] private LevelViewer _levelViewer;
    [SerializeField] private DinoHealthBar _dinoHealthBar;
    [SerializeField] private InputController _inputController;
    [SerializeField] private ButtonsAnimator _buttonsAnimator;
    [Header("Canvas")]
    [SerializeField] private CanvasGroup _gamePanel;
    [SerializeField] private CanvasGroup _startPanel;
    [SerializeField] private CanvasGroup _winPanel;
    [SerializeField] private CanvasGroup _losePanel;
    [Header("Buttons")]
    [SerializeField] private Button _next;
    [SerializeField] private Button _retry;
    [SerializeField] private Button _restartGame;
    [SerializeField] private Button _restartBoss;
    [SerializeField] private Button _fightTap;

    private Boss _boss;

    private const float Delay = 2f;
    private const float OnDelay = 0.2f;
    private const float Duration = 0.5f;

    public BossAreaTrigger BossAreaTrigger => _bossAreaTrigger;

    public event Action FightClicked;

    private void OnEnable()
    {
        _boss = _bossAreaTrigger.Boss;
        _powerBar.Init(_player, _eggsCounter);

        _gameOver.Won += OnGameWon;
        _gameOver.Losed += OnGameLosed;
        _inputController.Clicked += StartGame;

        _next.onClick.AddListener(Next);
        _retry.onClick.AddListener(Restart);
        _restartGame.onClick.AddListener(Restart);
        _restartBoss.onClick.AddListener(Restart);
        _fightTap.onClick.AddListener(OnFightCliked);
    }

    private void Awake()
    {
        Invoke(nameof(ShowLevel), OnDelay);
    }

    private void OnDisable()
    {
        _gameOver.Won -= OnGameWon;
        _gameOver.Losed -= OnGameLosed;
        _inputController.Clicked -= StartGame;

        _next.onClick.RemoveListener(Next);
        _retry.onClick.RemoveListener(Restart);
        _restartGame.onClick.RemoveListener(Restart);
        _restartBoss.onClick.RemoveListener(Restart);
        _fightTap.onClick.RemoveListener(OnFightCliked);
    }

    private void ShowLevel()
    {
        int level = _levelsLoader.LevelNumber;
        _levelViewer.Show(level);
    }

    private void StartGame()
    {
        _player.EnableMove();
        SwitchPanels(_gamePanel, _startPanel);
    }

    private void Next()
    {
        _levelsLoader.Next();
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

    private void OnGameWon()
    {
        _dinoHealthBar.Hide();
        _fightTap.enabled = false;
        _restartGame.enabled = false;
        _restartBoss.enabled = false;
        _buttonsAnimator.HideRestart();
        Invoke(nameof(EnableWinPanel), Delay);
    }

    private void OnGameLosed()
    {
        _dinoHealthBar.Hide();
        _fightTap.enabled = false;
        _restartGame.enabled = false;
        _restartBoss.enabled = false;
        _buttonsAnimator.HideRestart();
        Invoke(nameof(EnableLosePanel), Delay);
    }

    private void EnableWinPanel()
    {
        SwitchPanels(_winPanel, _gamePanel);
    }

    private void EnableLosePanel()
    {
        SwitchPanels(_losePanel, _gamePanel);
    }

    private void SwitchPanels(CanvasGroup panelOn, CanvasGroup panelOff)
    {
        EnableCanvas(panelOn);
        DisableCanvas(panelOff);
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
