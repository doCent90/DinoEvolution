using System;
using UnityEngine;

[RequireComponent(typeof(LevelsLoader))]
public class StartApplication : MonoBehaviour
{
    private LevelsLoader _levelsLoader;

    private const float Delay = 0.5f;
    private const string FirstDay = "first_day";

    private void OnEnable()
    {
        _levelsLoader = GetComponent<LevelsLoader>();

        if (PlayerPrefs.GetInt(FirstDay) == 0)
        {
            int firstDay = DateTime.Today.Day;
            PlayerPrefs.SetInt(FirstDay, firstDay);
        }

        AmplitudeHandler.InitAmplitude();
        SetRegDay();
        SetDaysInGame();
        SetCountSessions();

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

        if (currentDay != PlayerPrefs.GetInt(FirstDay))
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
