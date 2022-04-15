using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private LevelsLoader _levelsLoader;
    [SerializeField] private InputController _inputController;
    [Header("Canvas")]
    [SerializeField] private CanvasGroup _gamePanel;
    [SerializeField] private CanvasGroup _startPanel;
    [Header("Buttons")]
    [SerializeField] private Button _restart;

    private void OnEnable()
    {
        _restart.onClick.AddListener(Restart);
        _inputController.Clicked += StartGame;
    }

    private void OnDisable()
    {
        _inputController.Clicked -= StartGame;
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
