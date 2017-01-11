using UnityEngine;
using System.Collections;
using Soomla.Store;
using System;
using YupiStudios.Analytics;

public class StarsSystemEvents {	
	public static void Event01() {
		StoreInventory.GiveItem(LunaStoreAssets.STARS_CURRENCY_ID, 10);

		if (Social.localUser.authenticated) {
			Social.ReportProgress(GPGSIds.achievement_welcome_to_earth_to_luna, 100.0f, (bool done) => {});
			GameSave.WriteSave();
		}

		YupiAnalyticsEventHandler.StarsEvent("Credit", "Event01", 5);
	}

	public static void Event03() {
		StoreInventory.GiveItem (LunaStoreAssets.STARS_CURRENCY_ID, 20);

		if (Social.localUser.authenticated) {
			Social.ReportProgress(GPGSIds.achievement_it_is_good_to_see_you_again, 100.0f, (bool done) => {});
			GameSave.WriteSave();
		}

		YupiAnalyticsEventHandler.StarsEvent("Credit", "Event03", 20);
	}

	public static void FinishGameAchievement() {
		if (Social.localUser.authenticated) {
			Social.ReportProgress(GPGSIds.achievement_you_finished_earth_to_luna_lets_color, 100.0f, (bool done) => {});
		}
	}

	//finish share on facebook
	public static void Event04() {
		if (!PlayerPrefs.HasKey(StarsSystemManager.EVENT04_KEY)) {
			StoreInventory.GiveItem(LunaStoreAssets.STARS_CURRENCY_ID, 5);
			PlayerPrefs.SetInt(StarsSystemManager.EVENT04_KEY, 1);
			PlayerPrefs.Save();

			YupiAnalyticsEventHandler.StarsEvent("Credit", "Event04", 5);

			if (Social.localUser.authenticated) {
				Social.ReportProgress(GPGSIds.achievement_share_earth_to_luna, 100.0f, (bool done) => {});
				GameSave.WriteSave();
			}
		}
	}

	//share painting on facebook
	public static void Event05() {
		//todo
		DateTime now = DateTime.Now;
		string nowString = now.ToString();
		Debug.Log (now);

		if (PlayerPrefs.HasKey(StarsSystemManager.DATE_KEY)) {
			string previousDateString = PlayerPrefs.GetString(StarsSystemManager.DATE_KEY);

			if (String.IsNullOrEmpty(previousDateString)) {
				PlayerPrefs.SetString(StarsSystemManager.DATE_KEY, nowString);
				PlayerPrefs.Save();
				GiveOneStar();
				return;
			}

			DateTime previousDate = DateTime.Parse(previousDateString);
			DateTime limitDate = previousDate.AddDays(1);

			int comp = now.CompareTo(limitDate);

			Debug.Log (comp);
			if (comp > 0) {
				PlayerPrefs.SetString(StarsSystemManager.DATE_KEY, nowString);
				PlayerPrefs.Save();
				GiveOneStar();
			} 
			return;
		}

		Debug.Log(nowString);

		PlayerPrefs.SetString(StarsSystemManager.DATE_KEY, nowString);
		PlayerPrefs.Save();
		GiveOneStar();
	}

	public static void GiveOneStar() {
		StoreInventory.GiveItem(LunaStoreAssets.STARS_CURRENCY_ID, 1);
		
		if (Social.localUser.authenticated) {
			GameSave.WriteSave();
		}
		
		YupiAnalyticsEventHandler.StarsEvent("Credit", "Event05", 1);
	}
}
