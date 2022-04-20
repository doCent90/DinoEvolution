using System;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private SortGate _sortGate;
    [SerializeField] private RoadMover _roadMover;
    [SerializeField] private InputController _inputController;

    private const float DELAY = 1f;

    public event Action Losed;

    private void OnEnable()
    {
        _sortGate.EggStackEmpty += StopGame;
    }

    private void OnDisable()
    {
        _sortGate.EggStackEmpty -= StopGame;        
    }

    private void StopGame()
    {
        _roadMover.enabled = false;
        _inputController.enabled = false;

        Invoke(nameof(Lose), DELAY);
    }

    private void Lose()
    {
        Losed?.Invoke();
    }
}
