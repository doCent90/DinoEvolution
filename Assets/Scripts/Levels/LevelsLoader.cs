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

    public event Action<int> LevelChanged;

    private void OnEnable()
    {
        if(_isTestLevel)
            Debug.Log("TEST LEVEL");

        if (_isStartApp == false && _isTestLevel == false)
        {
            string currentLevelName = AmplitudeHandler.LEVEL;
            int currentLevel = PlayerPrefs.GetInt(currentLevelName);
            _levelNumber = currentLevel;

            AmplitudeHandler.SetLevelStart(_levelNumber);

            LevelChanged?.Invoke(currentLevel);
            _boss.Died += OnLevelDone;
        }
    }

    private void OnDisable()
    {
        if (_isStartApp == false)
            _boss.Died -= OnLevelDone;
    }

    private void Update()
    {
        _spentTime += Time.deltaTime;
    }

    public void LoadCurrentLevelOnStartApp()
    {
        string currentLevel = AmplitudeHandler.LEVEL;
        int level = PlayerPrefs.GetInt(currentLevel);

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
        string level = AmplitudeHandler.LEVEL;

        if (_isTestLevel == false)
        {
            int nextLevel = _levelNumber + 1;
            PlayerPrefs.SetInt(level, nextLevel);
        }
        else
        {
            Debug.Log("Test Level Done");
        }

        AmplitudeHandler.SetLevelComplete(_levelNumber, (int)_spentTime);
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
