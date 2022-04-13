using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private LevelsLoader _levelsLoader;
    [Header("Canvas")]
    [SerializeField] private CanvasGroup _gamePanel;
    [SerializeField] private CanvasGroup _startPanel;
    [Header("Buttons")]
    [SerializeField] private Button _start;
    [SerializeField] private Button _restart;

    private void OnEnable()
    {
        _start.onClick.AddListener(StartGame);
        _restart.onClick.AddListener(Restart);
    }

    private void OnDisable()
    {
        _start.onClick.RemoveListener(StartGame);
        _restart.onClick.RemoveListener(Restart);
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
