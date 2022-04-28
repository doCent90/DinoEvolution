using UnityEngine;
using IJunior.TypedScenes;
using System;
using Random = UnityEngine.Random;

public class LevelsLoader : MonoBehaviour, ISceneLoadHandler<int>
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
        if(_isTestLevel)
            Debug.Log("TEST LEVEL");

        if (_isStartApp && PlayerPrefs.GetInt(Level) == 0)
            PlayerPrefs.SetInt(Level, 1);

        if (_isStartApp == false && _isTestLevel == false)
        {
            int currentLevel = PlayerPrefs.GetInt(Level);
            _levelNumber = currentLevel;

            AmplitudeHandler.SetLevelStart(_levelNumber);

            LevelLoaded?.Invoke(currentLevel);
            _boss.Died += OnLevelDone;
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
        int nextLevel = _levelNumber + 1;
        Load(nextLevel);
    }

    public void Restart()
    {
        Load(_levelIndex);
        
        AmplitudeHandler.SetRestartLevel(_levelNumber);
    }

    private void OnLevelDone()
    {
        _boss.Died -= OnLevelDone;

        if (_isTestLevel == false)
        {
            int nextLevel = _levelNumber + 1;
            PlayerPrefs.SetInt(Level, nextLevel);
        }
        else
        {
            Debug.Log("Test Level Done");
        }

        AmplitudeHandler.SetLevelComplete(_levelNumber, (int)_spentTime);
    }

    private void RandomLevel()
    {
        int randomLevel = Random.Range(1, _levelsAmount + 1);
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
