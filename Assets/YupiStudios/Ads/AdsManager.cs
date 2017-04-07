using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using YupiPlay.Luna;
using System;
using Soomla.Store;

public class AdsManager : MonoBehaviour {
	[System.Serializable]
	public struct NativeAd {
		public string Name;
		public string Id;
		public string IdIOS;
		public SystemLanguage Language;
	}

	[System.Serializable]
	public struct RewardedVideoAdType {
		public string Name;
		public string Id;
		public string IdIOS;
	}

	public const string LastRewardTime = "LastRewardTime";

	public NativeAd[] NativeAds;
	public RewardedVideoAdType[] RewardedVideoAd;

	public static AdsManager Instance {
		get{return _instance;}
	}

	public delegate void VideoLoadedCallback();
	public static event VideoLoadedCallback OnVideoLoadedEvent;

	private static AdsManager _instance;

	private static NativeAd[] nativeAds;
	private static RewardedVideoAdType[] rewardedVideoAds;

	private RewardBasedVideoAd rewardVideoAd;
	private string videoTestId = "ca-app-pub-3940256099942544/5224354917";


	void Awake() {
		if (_instance == null) {
			_instance = this;
			nativeAds = NativeAds;
			rewardedVideoAds = RewardedVideoAd;
			DontDestroyOnLoad(this.gameObject);
		} else {
			Destroy(this.gameObject);
		} 			
	}

	void Start() {
		if (BuildConfiguration.AdsEnabled) {
			StartCoroutine(init());
		}
	}

	private IEnumerator init() {
		yield return new WaitForEndOfFrame();

		RequestVideoWorker();
	}

	public static bool IsVideoLoaded() {
		if (BuildConfiguration.AdsEnabled) {
			return Instance.rewardVideoAd.IsLoaded();	
		}
		return false;
	}

	public static void ShowVideo() {
		if (BuildConfiguration.AdsEnabled) {
			if (Instance.rewardVideoAd.IsLoaded()) {
				Instance.rewardVideoAd.Show();	
			}
		}
	}

	public static void LoadVideoAd() {
		if (Instance.canLoadVideo()) {
			Instance.requestRewardedVideo();	
		}
	}

	public static bool IsVideoAdAvailable() {
		return Instance.canLoadVideo() && Instance.rewardVideoAd.IsLoaded();
	}

	private RewardedVideoAdType getRewarededVideoInfo () {
		if (rewardedVideoAds.Length > 0) {
			return rewardedVideoAds[0];
		}
			
		return new RewardedVideoAdType();
	}

	private NativeAd getNativeAdInfo() {
		foreach (NativeAd ad in nativeAds) {
			if (ad.Language == Application.systemLanguage) {
				return ad;
			}
		}			

		return new NativeAd();
	}

	//Só carrega ad se o ultimo premio foi há 1 dia
	private bool canLoadVideo() {
		string lastReward = PlayerPrefs.GetString(LastRewardTime);

		if (string.IsNullOrEmpty(lastReward)) {
			return true;
		}

		DateTime lastTime = DateTime.Parse(lastReward);
		DateTime compareTime = lastTime.AddDays(1);
		DateTime now = DateTime.Now;

		if (compareTime.CompareTo(now) > 0)  {
			return true;
		}

		return false;
	}

	private void requestRewardedVideo() {
		RewardedVideoAdType videoAdInfo = getRewarededVideoInfo();

		if (string.IsNullOrEmpty(videoAdInfo.Name)) {
			return;
		}

		string id = null;

		#if UNITY_ANDROID
			id = videoAdInfo.Id;
		#elif UNITY_IOS
			id = videoAdInfo.IdIOS;
		#endif

		RewardBasedVideoAd rv = RewardBasedVideoAd.Instance;
		AdRequest req = new AdRequest.Builder()			            
			.Build();
		rv.LoadAd(req, videoTestId);

		if (rewardVideoAd == null) {
			rewardVideoAd = rv;

			rewardVideoAd.OnAdLoaded += onVideoLoaded;
			rewardVideoAd.OnAdRewarded += onVideoRewarded;
		}
	}		

	private void onVideoLoaded(object sender, EventArgs e) {
		
		if (OnVideoLoadedEvent != null) OnVideoLoadedEvent();
	}

	private void onVideoRewarded(object sender, Reward reward) {
		double amount = reward.Amount;
		StoreInventory.GiveItem(LunaStoreAssets.STARS_CURRENCY_ID, 10);

		if (Social.localUser.authenticated) {
			PlayerPrefs.SetString(LastRewardTime, DateTime.Now.ToString());
			GameSave.WriteSave();
		}
	}

	private IEnumerator RequestVideoWorker() {
		yield return new WaitForEndOfFrame();

		while (canLoadVideo() &&  ! rewardVideoAd.IsLoaded()) {
			requestRewardedVideo();

			yield return new WaitForSecondsRealtime(10);
		}
	}





	

}
