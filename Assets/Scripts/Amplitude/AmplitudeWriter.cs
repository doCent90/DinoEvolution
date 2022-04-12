using System.Collections.Generic;

public static class AmplitudeHandler
{
    private const string Key = "0";

    public const string COUNT = "count";
    public const string LEVEL = "level";
    public const string REG_DAY = "reg_day";
    public const string MAIN_MENU = "main_menu";
    public const string PLACEMENT = "placement";
    public const string GAME_START = "game_start";
    public const string TIME_SPENT = "time_spent";
    public const string DAYS_IN_GAME = "days_in_game";
    public const string CURRENT_SOFT = "current_soft";
    public const string SESSION_COUNT = "session_count";

    public static void InitAmplitude()
    {
        Amplitude amplitude = Amplitude.getInstance();
        amplitude.setServerUrl("https://api2.amplitude.com");
        amplitude.logging = true;
        amplitude.trackSessionEvents(true);
        amplitude.init(Key);
    }

    //Set Amplitude User Properties
    public static void SetSessionCount(int count)
    {
        string userEventName = SESSION_COUNT;
        Amplitude.Instance.setUserProperty(userEventName, count);
    }

    public static void SetCurrentSoft(int count)
    {
        string userEventName = CURRENT_SOFT;
        Amplitude.Instance.setUserProperty(userEventName, count);
    }

    public static void SetDaysInGame(int count)
    {
        string userEventName = DAYS_IN_GAME;
        Amplitude.Instance.setUserProperty(userEventName, count);
    }

    public static void SetRegDay(string date)
    {
        string userEventName = REG_DAY;
        Amplitude.Instance.setUserProperty(userEventName, date);
    }

    //Set Events In Game
    public static void SetGameStart(int count)
    {
        string eventName = GAME_START;
        string properties = COUNT;

        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            {properties, count}
        };

        Amplitude.Instance.logEvent(eventName, dictionary);
    }

    public static void SetLevelStart(int level)
    {
        string eventName = "level_start";
        string properties = LEVEL;

        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            {properties, level}
        };

        Amplitude.Instance.logEvent(eventName, dictionary);
    }

    public static void SetRestartLevel(int level)
    {
        string eventName = "restart";
        string properties = LEVEL;

        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            {properties, level}
        };

        Amplitude.Instance.logEvent(eventName, dictionary);
    }

    public static void SetOpenMainMenu()
    {
        string eventName = MAIN_MENU;
        string properties1 = "open";
        string properties2 = MAIN_MENU;

        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            {properties1, properties2}
        };

        Amplitude.Instance.logEvent(eventName, dictionary);
    }

    public static void SetLevelComplete(int level, int timeSpent)
    {
        string eventName = "level_complete";
        string properties1 = LEVEL;
        string properties2 = TIME_SPENT;

        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            {properties1, level},
            {properties2, timeSpent}
        };

        Amplitude.Instance.logEvent(eventName, dictionary);
    }

    public static void SetLevelFail(int level, string reason, int timeSpent)
    {
        string eventName = "fail";
        string properties1 = LEVEL;
        string properties2 = "reason";
        string properties3 = TIME_SPENT;

        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            {properties1, level},
            {properties2, reason},
            {properties3, timeSpent}
        };

        Amplitude.Instance.logEvent(eventName, dictionary);
    }

    public static void SetSoftSpent(string type, string name, int amount)
    {
        string eventName = "soft_spent";
        string properties1 = "type";
        string properties2 = "name";
        string properties3 = "amount";

        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            {properties1, type},
            {properties2, name},
            {properties3, amount}
        };

        Amplitude.Instance.logEvent(eventName, dictionary);
    }

    //Invoke For Ad
    public static void SetInterstitialStart(string placement)
    {
        string eventName = "interstitial_start";
        string properties = PLACEMENT;

        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            {properties, placement}
        };

        Amplitude.Instance.logEvent(eventName, dictionary);
    }

    public static void SetRewardedShown(string placement)
    {
        string eventName = "rewarded_shown";
        string properties = PLACEMENT;

        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            {properties, placement}
        };

        Amplitude.Instance.logEvent(eventName, dictionary);
    }

    public static void SetRewardedStart(string placement)
    {
        string eventName = "rewarded_start";
        string properties = PLACEMENT;

        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            {properties, placement}
        };

        Amplitude.Instance.logEvent(eventName, dictionary);
    }
}
