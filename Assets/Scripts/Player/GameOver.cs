using System;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private RoadMover _roadMover;
    [SerializeField] private InputController _inputController;

    private const float Delay = 0.5f;

    public event Action Won;
    public event Action Losed;

    public void OnBossDied()
    {
        Won?.Invoke();
    }

    public void OnBossWin()
    {
        Losed?.Invoke();
    }

    public void StopGame()
    {
        _roadMover.enabled = false;
        _inputController.enabled = false;

        Invoke(nameof(OnBossWin), Delay);
    }
}
