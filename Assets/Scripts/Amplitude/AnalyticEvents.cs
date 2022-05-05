using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public static class AnalyticEvents
{
    //Sets User Properties
    public static void OnSessionInitialize(int sessionCount)
    {
        string count = "count";
        string sessionCountEvent = "session_count";

        Dictionary<string, object> dictionary = new Dictionary<string, object>()
        {
            {count, sessionCount}
        };

        GameAnalytics.NewDesignEvent(sessionCountEvent, dictionary);
        AppMetrica.Instance.ReportEvent(sessionCountEvent, dictionary);
    }

    public static void SetCurrentSoft(int softCount)
    {
        string count = "count";
        string currentSoftCountEvent = "current_soft";

        Dictionary<string, object> dictionary = new Dictionary<string, object>()
        {
            {count, softCount}
        };

        GameAnalytics.NewDesignEvent(currentSoftCountEvent, dictionary);
        AppMetrica.Instance.ReportEvent(currentSoftCountEvent, dictionary);
    }

    public static void SetRegDay(string date)
    {
        string day = "day";
        string regDayEvent = "reg_day";

        Dictionary<string, object> dictionary = new Dictionary<string, object>()
        {
            {day, date}
        };

        GameAnalytics.NewDesignEvent(regDayEvent, dictionary);
        AppMetrica.Instance.ReportEvent(regDayEvent, dictionary);
    }

    public static void SetDaysInGame(int count)
    {
        string days = "days";
        string daysInGameEvent = "days_in_game";

        Dictionary<string, object> dictionary = new Dictionary<string, object>()
        {
            {days, count}
        };

        GameAnalytics.NewDesignEvent(daysInGameEvent, dictionary);
        AppMetrica.Instance.ReportEvent(daysInGameEvent, dictionary);
    }

    //Sets Events In Game
    public static void OnGameStarted(int gameSessionCount)
    {
        string count = "count";
        string gameCountEvent = "game_start";

        Dictionary<string, object> dictionary = new Dictionary<string, object>()
        {
            {count, gameSessionCount}
        };

        GameAnalytics.NewDesignEvent(gameCountEvent, dictionary);
        AppMetrica.Instance.ReportEvent(gameCountEvent, dictionary);
    }

    public static void OnLevelStarted(int levelNumber)
    {
        string level = "level";

        AppMetrica.Instance.ReportEvent("level_start", new Dictionary<string, object>()
        {
            {level, levelNumber}
        });

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, level, levelNumber);
    }

    public static void OnLevelComplete(int levelNumber)
    {
        string level = "level";

        AppMetrica.Instance.ReportEvent("level_complete", new Dictionary<string, object>()
        {
            {level, levelNumber},
            {"time_spent", (int)Time.timeSinceLevelLoad}
        });

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, level, levelNumber);
    }

    public static void OnFail(int levelNumber, string reason)
    {
        string level = "level";

        AppMetrica.Instance.ReportEvent("fail", new Dictionary<string, object>()
        {
            {level, levelNumber},
            {"reason", reason},
            {"time_spent", (int)Time.timeSinceLevelLoad}
        });

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, level, levelNumber);
    }

    public static void OnLevelRestart(int levelNumber)
    {
        string level = "level";

        AppMetrica.Instance.ReportEvent("restart", new Dictionary<string, object>()
        {
            {level, levelNumber}
        });

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, level, levelNumber);
    }

    public static void OnOpenMenu()
    {
        string menuOpen = "menu_was_open";

        AppMetrica.Instance.ReportEvent("main_menu", new Dictionary<string, object>()
        {
            {"main_menu", menuOpen}
        });

        GameAnalytics.NewDesignEvent(menuOpen);
    }

    public static void SetSoftSpent(string type, string name, int amount)
    {
        string softSpent = "soft_spent";

        Dictionary<string, object> dictionary = new Dictionary<string, object>()
        {
            {"type", type},
            {"name", name},
            {"amount", amount}
        };

        GameAnalytics.NewDesignEvent(softSpent, dictionary);
        AppMetrica.Instance.ReportEvent(softSpent, dictionary);
    }

    // Set Ads ivents
    public static void OnInterstitialStart(string placement)
    {
        string placementType = "interstitial_start";

        Dictionary<string, object> dictionary = new Dictionary<string, object>()
        {
            {"placement", placement}
        };

        GameAnalytics.NewDesignEvent(placementType, dictionary);
        AppMetrica.Instance.ReportEvent(placementType, dictionary);
    }

    public static void OnRewardedShown(string placement)
    {
        string placementType = "rewarded_shown";

        Dictionary<string, object> dictionary = new Dictionary<string, object>()
        {
            {"placement", placement}
        };

        GameAnalytics.NewDesignEvent(placementType, dictionary);
        AppMetrica.Instance.ReportEvent(placementType, dictionary);
    }

    public static void OnRewardedStart(string placement)
    {
        string placementType = "rewarded_start";

        Dictionary<string, object> dictionary = new Dictionary<string, object>()
        {
            {"placement", placement}
        };

        GameAnalytics.NewDesignEvent(placementType, dictionary);
        AppMetrica.Instance.ReportEvent(placementType, dictionary);
    }
}
