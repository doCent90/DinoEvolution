using System.Collections.Generic;
using UnityEngine;

public static class AppMetricaEvents
{
    //Sets User Properties
    public static void OnSessionInitialize(int sessionCount)
    {
        AppMetrica.Instance.ReportEvent("session_count", new Dictionary<string, object>()
        {
            {"count", sessionCount}
        });
    }

    public static void SetCurrentSoft(int count)
    {
        AppMetrica.Instance.ReportEvent("current_soft", new Dictionary<string, object>()
        {
            {"count", count}
        });
    }

    public static void SetRegDay(string date)
    {
        AppMetrica.Instance.ReportEvent("reg_day", new Dictionary<string, object>()
        {
            {"day", date}
        });
    }

    public static void SetDaysInGame(int count)
    {
        AppMetrica.Instance.ReportEvent("days_in_game", new Dictionary<string, object>()
        {
            {"days", count}
        });
    }

    //Sets Events In Game
    public static void OnGameStarted(int gameSessionCount)
    {
        AppMetrica.Instance.ReportEvent("game_start", new Dictionary<string, object>()
        {
            {"count", gameSessionCount}
        });
    }

    public static void OnLevelStarted(int levelNumber)
    {
        AppMetrica.Instance.ReportEvent("level_start", new Dictionary<string, object>()
        {
            {"level", levelNumber}
        });
    }

    public static void OnLevelComplete(int levelNumber)
    {
        AppMetrica.Instance.ReportEvent("level_complete", new Dictionary<string, object>()
        {
            {"level", levelNumber},
            {"time_spent", (int)Time.timeSinceLevelLoad}
        });
    }

    public static void OnFail(int levelNumber, string reason)
    {
        AppMetrica.Instance.ReportEvent("fail", new Dictionary<string, object>()
        {
            {"level", levelNumber},
            {"reason", reason},
            {"time_spent", (int)Time.timeSinceLevelLoad}
        });
    }

    public static void OnLevelRestart(int levelNumber)
    {
        AppMetrica.Instance.ReportEvent("restart", new Dictionary<string, object>()
        {
            {"level", levelNumber}
        });
    }

    public static void OnOpenMenu()
    {
        AppMetrica.Instance.ReportEvent("main_menu", new Dictionary<string, object>()
        {
            {"main_menu", "Menu_was_open"}
        });
    }

    public static void SetSoftSpent(string type, string name, int amount)
    {
        AppMetrica.Instance.ReportEvent("soft_spent", new Dictionary<string, object>()
        {
            {"type", type},
            {"name", name},
            {"amount", amount}
        });
    }

    // Set Ads ivents
    public static void OnInterstitialStart(string placement)
    {
        AppMetrica.Instance.ReportEvent("interstitial_start", new Dictionary<string, object>()
        {
            {"placement", placement}
        });
    }

    public static void OnRewardedShown(string placement)
    {
        AppMetrica.Instance.ReportEvent("rewarded_shown", new Dictionary<string, object>()
        {
            {"placement", placement}
        });
    }

    public static void OnRewardedStart(string placement)
    {
        AppMetrica.Instance.ReportEvent("rewarded_start", new Dictionary<string, object>()
        {
            {"placement", placement}
        });
    }
}
