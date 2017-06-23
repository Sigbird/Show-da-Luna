using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using YupiPlay.Luna;
using System;
using Soomla.Store;

namespace YupiPlay.Ads
{
    public class AdsManager : MonoBehaviour {    	    	    
	    public AdInfo[] NativeAds;
	    public AdInfo[] RewardedVideoAd;

	    public static AdsManager Instance {
		    get{return _instance;}
	    }

	    public delegate void VideoLoadedCallback();
	    public static event VideoLoadedCallback OnVideoLoadedEvent;
        public delegate void RewardPlayerCallback(int stars);
        public static event RewardPlayerCallback OnRewardPlayer;

	    private static AdsManager _instance;

	    private static AdInfo[] nativeAds;
	    private static AdInfo[] rewardedVideoAds;

	    private RewardBasedVideoAd rewardVideoAd;
	    //private string videoTestId = "ca-app-pub-3940256099942544/5224354917";	    

        private NativeExpressAdView nativeAd;
        private bool IsNativeAdReady = false;        

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
            rewardVideoAd = RewardBasedVideoAd.Instance;
            rewardVideoAd.OnAdLoaded += onVideoLoaded;
            rewardVideoAd.OnAdFailedToLoad += onVideoFailedToLoad;
            rewardVideoAd.OnAdRewarded += onVideoRewarded;
            rewardVideoAd.OnAdClosed += onVideoClosed;
            rewardVideoAd.OnAdLeavingApplication += onVideoLeaving;

            //if (BuildConfiguration.AdsEnabled) {
            //StartCoroutine(RequestVideoWorker());
            StartCoroutine(init());
		    //}
	    }

	    private IEnumerator init() {
		    yield return new WaitForEndOfFrame();

            
                //RequestNativeAd();
                RequestRewardedVideo();
                        
		    //LoadVideoAd();
	    }

	    public static bool IsVideoLoaded() {		    
			return Instance.rewardVideoAd.IsLoaded();			    
	    }

	    public static void ShowVideo() {		   
		    if (Instance.rewardVideoAd.IsLoaded()) {
			    Instance.rewardVideoAd.Show();	
			}		    
	    }

	    public static void LoadVideoAd() {		    
			    Instance.RequestRewardedVideo();			    
	    }	    
	   
	    private AdInfo getRewarededVideoInfo () {		    
			return rewardedVideoAds[0];		    					    
	    }

	  

        private AdRequest getRewardedVideoRequest() {
            return new AdRequest.Builder()
                //.AddTestDevice("57D98E23BB9C9FFF4D03C514925FF6E1")
                //.TagForChildDirectedTreatment(true)
                //.AddExtra("is_designed_for_families", "true")
                .Build();
        }

	    private void RequestRewardedVideo() {
		    AdInfo videoAdInfo = getRewarededVideoInfo();

            if (videoAdInfo == null) {
                return;
            }            		    		    		   

            rewardVideoAd.LoadAd(getRewardedVideoRequest(), videoAdInfo.AndroidId);
        }				

	    private void onVideoLoaded(object sender, EventArgs e) {

		    Debug.Log("Video Loaded");
		    if (OnVideoLoadedEvent != null) OnVideoLoadedEvent();
	    }

	    private void onVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
		    Debug.Log("VideoAd Load Failed: " + args.Message);		
	    }

	    private void onVideoRewarded(object sender, Reward reward) {           
            Debug.Log("called OnVideoReward, stars: " + reward.Amount);            
           
            if (OnRewardPlayer != null) {
                Debug.Log("calling event");
                OnRewardPlayer((int) reward.Amount);
            }

            RequestRewardedVideo();
            Debug.Log("Requested another video");
        }        

        private void onVideoClosed(object sender, EventArgs e) {
            Debug.Log("Video Closed");
        }        

        private void onVideoLeaving(object sender, EventArgs e) {
            Debug.Log("leaving video");
        }


        #region NativeAd
        private AdInfo getNativeAdInfo()
        {
            foreach (AdInfo ad in nativeAds)
            {
                if (ad.Language == Application.systemLanguage)
                {
                    return ad;
                }
            }

            return null;
        }

        private AdRequest getAdRequest()
        {
            return new AdRequest.Builder()
                //.AddTestDevice("57D98E23BB9C9FFF4D03C514925FF6E1")
                //.TagForChildDirectedTreatment(true)
                //.AddExtra("is_designed_for_families", "true")									
                .Build();
        }

        private void RequestNativeAd() {
            AdInfo nAd = getNativeAdInfo();

            if (nAd == null) {
                return;
            }

            Debug.Log("Native Ad Id: " + nAd.Id);

            nativeAd = new NativeExpressAdView(nAd.Id, new AdSize(360, 320), AdPosition.Center);

            nativeAd.OnAdLoaded += OnNativeAdLoaded;
            nativeAd.OnAdFailedToLoad += OnNativeAdFailedToLoad;

            nativeAd.LoadAd(getAdRequest());            
        }

        private void OnNativeAdLoaded(object sender, EventArgs args) {
            IsNativeAdReady = true;
            Debug.Log("Native Ad Loaded");
        }

        private void OnNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
            Debug.Log("Native Ad Failed to Load");
        }

        public static bool CanShowNativeAd() {
            return _instance.IsNativeAdReady;
        }

        public static void ShowNativeAd() {
            if (CanShowNativeAd())
            {
                _instance.nativeAd.Show();
            }
        }
        #endregion NativeAd
    }
}
