using UnityEngine;
using IJunior.TypedScenes;
using Random = UnityEngine.Random;

public class LevelsLoader : MonoBehaviour, ISceneLoadHandler<int>
{
    [SerializeField] private GameOver _gameOver;
    [Header("Settings")]
    [SerializeField] private bool _isStartApp = false;
    [SerializeField] private bool _isTestLevel = false;

    private readonly int _levelsAmount = 3;
    private float _spentTime;
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
            AmplitudeHandler.SetLevelStart(LevelNumber);
        }

        if(_isTestLevel)
            Debug.Log("TEST LEVEL");
    }

    private void Update()
    {
        _spentTime += Time.deltaTime;
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
        
        AmplitudeHandler.SetRestartLevel(LevelNumber);
    }

    public void OnLevelDone()
    {
        if (_isTestLevel)
            return;

        int nextLevel = LevelNumber + 1;
        PlayerPrefs.SetInt(Level, nextLevel);
        AmplitudeHandler.SetLevelComplete(LevelNumber, (int)_spentTime);
        _gameOver.Won -= OnLevelDone;
    }

    private void RandomLevel()
    {
        int randomLevel = Random.Range(3, _levelsAmount + 1);
        Load(randomLevel);
    }

    private void Load(int number)
    {
        switch (number)
        {
            case 1:
                LVL_1.Load(number);
                break;
            case 2:
                LVL_2.Load(number);
                break;
            case 3:
                LVL_3.Load(number);
                break;
            case 4:
                LVL_4.Load(number);
                break;
            case 5:
                LVL_5.Load(number);
                break;
            case 6:
                LVL_6.Load(number);
                break;
            case 7:
                LVL_7.Load(number);
                break;
            case 8:
                LVL_8.Load(number);
                break;
            case 9:
                LVL_9.Load(number);
                break;
            default:
                RandomLevel();
                break;
        }
    }

    public void OnSceneLoaded(int level)
    {
        _levelIndex = level;
    }
}
