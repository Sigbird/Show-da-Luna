using UnityEngine;
using YupiPlay.Luna;

#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif

#if UNITY_IOS
//dependências IOS
using UnityEngine.SocialPlatforms.GameCenter;
#endif

public class GameServicesInit : MonoBehaviour {	

	// Use this for initialization
	void Start () {
        //GPGSEnabled é uma configuração multiplataforma
		if (BuildConfiguration.GPGSEnabled == false) {
			gameObject.SetActive(false);
			return;
		}

#if UNITY_ANDROID
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			.EnableSavedGames()
				.Build();

		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
#endif

#if UNITY_IOS
        //vem IOS        
#endif
    }
}