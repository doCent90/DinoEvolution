using UnityEngine;

public class Tools : MonoBehaviour
{
    [SerializeField] private RoadMover _roadMover;
    [SerializeField] private GameOver _gameOver;

    public GameOver GameOver => _gameOver;
    public RoadMover RoadMover => _roadMover;
}
