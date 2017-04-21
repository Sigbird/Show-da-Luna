#if UNITY_ANDROID

using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using YupiPlay.Luna;

public class GameServicesInit : MonoBehaviour {	

	// Use this for initialization
	void Start () {
		if (BuildConfiguration.GPGSEnabled == false) {
			gameObject.SetActive(false);
			return;
		}

		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			.EnableSavedGames()
				.Build();

		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();

	}
}

#endif