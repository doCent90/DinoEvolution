using System;
using UnityEngine;
using IJunior.TypedScenes;
using Random = UnityEngine.Random;

public class LevelsLoader : MonoBehaviour, ISceneLoadHandler<LevelData>
{
    [SerializeField] private Boss _boss;
    [Header("Settings")]
    [SerializeField] private bool _isStartApp = false;
    [SerializeField] private bool _isTestLevel = false;

    private readonly int _levelsAmount = 3;
    private int _levelNumber;
    private int _levelIndex;
    private float _spentTime;

    private const string Level = "level";

    public event Action<int> LevelLoaded;

    private void OnEnable()
    {
        if (_isStartApp && PlayerPrefs.GetInt(Level) == 0)
            PlayerPrefs.SetInt(Level, 1);

        if (_isTestLevel)
        {
            Debug.Log("TEST LEVEL");
        }
        else
        {
            _levelNumber = PlayerPrefs.GetInt(Level);
            LevelLoaded?.Invoke(_levelNumber);

            _boss.Died += OnLevelDone;
            AmplitudeHandler.SetLevelStart(_levelNumber);
        }
    }

    private void Update()
    {
        _spentTime += Time.deltaTime;
    }

    public void LoadCurrentLevelOnStartApp()
    {
        int level = PlayerPrefs.GetInt(Level);
        Load(level);
    }

    public void Next()
    {
        Load(_levelNumber);
    }

    public void Restart()
    {
        Load(_levelIndex);
        
        AmplitudeHandler.SetRestartLevel(_levelNumber);
    }

    private void OnLevelDone()
    {
        if (_isTestLevel == false)
        {
            AmplitudeHandler.SetLevelComplete(_levelNumber, (int)_spentTime);
            _levelNumber++;
            PlayerPrefs.SetInt(Level, _levelNumber);
        }
        else
        {
            Debug.Log("Test Level Done");
        }

        _boss.Died -= OnLevelDone;
    }

    private void RandomLevel()
    {
        int randomLevel = Random.Range(1, _levelsAmount + 1);
        Load(randomLevel);
    }

    private void Load(int number)
    {
        LevelData levelData = new LevelData(number, _levelNumber);

        switch (number)
        {
            case 1:
                LVL_1.Load(levelData);
                break;
            case 2:
                LVL_2.Load(levelData);
                break;
            case 3:
                LVL_3.Load(levelData);
                break;
            default:
                RandomLevel();
                break;
        }
    }

    public void OnSceneLoaded(LevelData levelData)
    {
        _levelIndex = levelData.LevelIndex;
        _levelNumber = levelData.LevelNumber;
    }
}

public class LevelData
{
    public int LevelIndex;
    public int LevelNumber;

    public LevelData(int levelIndex, int levelNumber)
    {
        LevelIndex = levelIndex;
        LevelNumber = levelNumber;
    }
}
