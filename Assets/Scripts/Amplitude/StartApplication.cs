using System;
using UnityEngine;
using GameAnalyticsSDK;

[RequireComponent(typeof(LevelsLoader))]
public class StartApplication : MonoBehaviour
{
    [SerializeField] private GameObject _gameAnalyticsSDK;

    private LevelsLoader _levelsLoader;

    private const float Delay = 0.5f;

    private const string Level = "level";
    private const string RegDay = "reg_day";
    private const string FirstDay = "first_day";
    private const string DaysInGame = "days_in_game";
    private const string SessionCount = "session_count";

    private void OnEnable()
    {
        _levelsLoader = GetComponent<LevelsLoader>();
        Init();
    }

    private void Init()
    {
        EnableGameAnalytics();

        if (PlayerPrefs.GetInt(Level) == 0)
            PlayerPrefs.SetInt(Level, 1);

        Invoke(nameof(StartGame), Delay);

        if (PlayerPrefs.GetInt(FirstDay) == 0)
        {
            int firstDay = DateTime.Today.Day;
            PlayerPrefs.SetInt(FirstDay, firstDay);
        }

        SetRegDay();
        SetDaysInGame();
        SetCountSessions();
    }

    private void StartGame()
    {
        _levelsLoader.LoadCurrentLevelOnStartApp();
    }

    private void EnableGameAnalytics()
    {
        _gameAnalyticsSDK.SetActive(true);
        GameAnalytics.Initialize();
    }

    private void SetCountSessions()
    {
        int countStartSessions = PlayerPrefs.GetInt(SessionCount);
        countStartSessions++;

        PlayerPrefs.SetInt(SessionCount, countStartSessions);

        AnalyticEvents.OnGameStarted(countStartSessions);
        AnalyticEvents.OnSessionInitialize(countStartSessions);
    }

    private void SetDaysInGame()
    {
        int currentDay = DateTime.Today.Day;
        string daysInGame = DaysInGame;

        if(PlayerPrefs.GetInt(daysInGame) == 0)
        {
            PlayerPrefs.SetInt(daysInGame, 1);
            AnalyticEvents.SetDaysInGame(1);
        }

        if (currentDay != PlayerPrefs.GetInt(FirstDay))
        {
            int days = PlayerPrefs.GetInt(daysInGame);
            days++;

            PlayerPrefs.SetInt(daysInGame, days);
            AnalyticEvents.SetDaysInGame(days);
        }
    }

    private void SetRegDay()
    {
        int True = 1;
        string regDay = RegDay;

        DateTime dateTime = DateTime.Now;
        string date = dateTime.ToString();

        if (PlayerPrefs.GetInt(regDay) != True)
        {
            PlayerPrefs.SetInt(regDay, True);
            PlayerPrefs.SetString(regDay, date);
        }

        AnalyticEvents.SetRegDay(PlayerPrefs.GetString(regDay));
    }
}
