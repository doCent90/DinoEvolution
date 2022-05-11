using UnityEngine;
using IJunior.TypedScenes;

public class LevelsLoader : MonoBehaviour, ISceneLoadHandler<int>
{
    [SerializeField] private GameOver _gameOver;
    [Header("Settings")]
    [SerializeField] private bool _isStartApp = false;
    [SerializeField] private bool _isTestLevel = false;

    private int _levelIndex;

    private const string Level = "level";

    public int LevelNumber { get; private set; }

    private void OnEnable()
    {
        Application.targetFrameRate = 60;

        if (_isStartApp == false && _isTestLevel == false)
        {
            LevelNumber = PlayerPrefs.GetInt(Level);

            _gameOver.Won += OnLevelDone;
            _gameOver.Losed += OnLevelFaled;
            AnalyticEvents.OnLevelStarted(LevelNumber);
        }

        if(_isTestLevel)
            Debug.Log("TEST LEVEL");
    }

    public void LoadCurrentLevelOnStartApp()
    {
        int level = PlayerPrefs.GetInt(Level);
        LevelNumber = level;
        Load(level);
    }

    public void Next()
    {
        int nextLevel = LevelNumber + 1;
        Load(nextLevel);
    }

    public void Restart()
    {
        Load(_levelIndex);
        
        AnalyticEvents.OnLevelComplete(LevelNumber);
    }

    public void OnLevelDone()
    {
        if (_isTestLevel)
            return;

        int nextLevel = LevelNumber + 1;
        PlayerPrefs.SetInt(Level, nextLevel);
        AnalyticEvents.OnLevelComplete(LevelNumber);

        _gameOver.Won -= OnLevelDone;
        _gameOver.Losed -= OnLevelFaled;
    }

    private void OnLevelFaled()
    {
        AnalyticEvents.OnFail(LevelNumber, "Boss Won");
    }

    private void Load(int number)
    {
        LevelsStock.LoadLevel(number);
    }

    public void OnSceneLoaded(int level)
    {
        _levelIndex = level;
    }
}
