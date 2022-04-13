using System;
using UnityEngine;

[RequireComponent(typeof(LevelsLoader))]
public class StartApplication : MonoBehaviour
{
    private LevelsLoader _levelsLoader;

    private const float DELAY = 1f;
    private const string FIRST_DAY = "first_day";

    private void OnEnable()
    {
        _levelsLoader = GetComponent<LevelsLoader>();
        Invoke(nameof(StartGame), DELAY);

        if (PlayerPrefs.GetInt(FIRST_DAY) == 0)
        {
            int firstDay = DateTime.Today.Day;
            PlayerPrefs.SetInt(FIRST_DAY, firstDay);
        }

        if(PlayerPrefs.GetInt(AmplitudeHandler.LEVEL) == 0)
            PlayerPrefs.SetInt(AmplitudeHandler.LEVEL, 1);

        AmplitudeHandler.InitAmplitude();
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
        string sessionCount = AmplitudeHandler.SESSION_COUNT;
        int countStartSessions = PlayerPrefs.GetInt(sessionCount);
        countStartSessions++;

        PlayerPrefs.SetInt(sessionCount, countStartSessions);

        AmplitudeHandler.SetGameStart(countStartSessions);
        AmplitudeHandler.SetSessionCount(countStartSessions);
    }

    private void SetDaysInGame()
    {
        int currentDay = DateTime.Today.Day;
        string daysInGame = AmplitudeHandler.DAYS_IN_GAME;

        if(PlayerPrefs.GetInt(daysInGame) == 0)
        {
            PlayerPrefs.SetInt(daysInGame, 1);
            AmplitudeHandler.SetDaysInGame(1);
        }

        if (currentDay != PlayerPrefs.GetInt(FIRST_DAY))
        {
            int days = PlayerPrefs.GetInt(daysInGame);
            days++;

            PlayerPrefs.SetInt(daysInGame, days);
            AmplitudeHandler.SetDaysInGame(days);
        }
    }

    private void SetRegDay()
    {
        int True = 1;
        string regDay = AmplitudeHandler.REG_DAY;

        DateTime dateTime = DateTime.Now;
        string date = dateTime.ToString();

        if (PlayerPrefs.GetInt(regDay) != True)
        {
            PlayerPrefs.SetInt(regDay, True);
            PlayerPrefs.SetString(regDay, date);
        }

        AmplitudeHandler.SetRegDay(PlayerPrefs.GetString(regDay));
    }
}
