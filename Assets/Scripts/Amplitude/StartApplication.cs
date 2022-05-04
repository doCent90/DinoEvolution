using System;
using UnityEngine;

[RequireComponent(typeof(LevelsLoader))]
public class StartApplication : MonoBehaviour
{
    private LevelsLoader _levelsLoader;

    private const float Delay = 0.5f;

    private const string Level = "level";
    private const string FirstDay = "first_day";
    private const string RegDay = "reg_day";
    private const string DaysInGame = "days_in_game";
    private const string SessionCount = "session_count";

    private void OnEnable()
    {
        _levelsLoader = GetComponent<LevelsLoader>();

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

    private void SetCountSessions()
    {
        int countStartSessions = PlayerPrefs.GetInt(SessionCount);
        countStartSessions++;

        PlayerPrefs.SetInt(SessionCount, countStartSessions);

        AppMetricaEvents.OnGameStarted(countStartSessions);
        AppMetricaEvents.OnSessionInitialize(countStartSessions);
    }

    private void SetDaysInGame()
    {
        int currentDay = DateTime.Today.Day;
        string daysInGame = DaysInGame;

        if(PlayerPrefs.GetInt(daysInGame) == 0)
        {
            PlayerPrefs.SetInt(daysInGame, 1);
            AppMetricaEvents.SetDaysInGame(1);
        }

        if (currentDay != PlayerPrefs.GetInt(FirstDay))
        {
            int days = PlayerPrefs.GetInt(daysInGame);
            days++;

            PlayerPrefs.SetInt(daysInGame, days);
            AppMetricaEvents.SetDaysInGame(days);
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

        AppMetricaEvents.SetRegDay(PlayerPrefs.GetString(regDay));
    }
}
