using UnityEngine;
using System;

public static class AdsCooldown {
    public const string LastVideoRewardTime = "LastVideoRewardTime";
    public const string LastAdTime = "LastAdTime";

    // 1 day = 86400 seconds
    // 1 hour = 3600 seconds
    public const float RewaredVideoIntervalSeconds = 86400f;
    public const float AdIntervalSeconds = 120f;
    public const int StarsToReward = 1;
    
    public static bool CanShowRewardedVideo() {       
        var nextTime = GetRewardedVideoNextTime();

        if (nextTime.CompareTo(DateTime.Now) <= 0) return true;        

        return false;
    }

    public static DateTime GetRewardedVideoNextTime()
    {
        var lastString = PlayerPrefs.GetString(LastVideoRewardTime);        

        if (string.IsNullOrEmpty(lastString)) return new DateTime();

        var nextTime = DateTime.Parse(lastString).AddSeconds(RewaredVideoIntervalSeconds);

        return nextTime;
    }

    public static string GetRewardedVideoClockString() {
        var nextTime = AdsCooldown.GetRewardedVideoNextTime();
        var deadline = nextTime.Subtract(DateTime.Now);
        return deadline.Hours + ":" + deadline.Minutes + ":" + deadline.Seconds;        
    }

    public static void UpdateLastVideoRewardTime() {
        string saveTime = DateTime.Now.ToString();      
        PlayerPrefs.SetString(LastVideoRewardTime, saveTime);
    }

    public static void UpdateLastAdTime() {
        PlayerPrefs.SetString(LastAdTime, DateTime.Now.ToString());
    }

    public static bool CanShowAd()
    {
        var lastString = PlayerPrefs.GetString(LastAdTime);

        if (string.IsNullOrEmpty(lastString)) return true;

        var nextTime = DateTime.Parse(lastString).AddSeconds(AdIntervalSeconds);

        if (nextTime.CompareTo(DateTime.Now) <= 0) return true;

        return false;
    }


}
