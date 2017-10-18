#if UNITY_ANDROID || UNITY_IOS

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;
using Soomla.Store;
using YupiStudios.Analytics;
using YupiPlay.Luna;
using System.Collections;

namespace YupiPlay.Ads 
{
    public class MenuAdsController : MonoBehaviour {
        public float AdsCheckIntervalSeconds = 1f;
        public GameObject NewMessage;
        public GameObject RewardPanel;
        public GameObject NativeAdPanel; 

        public UnityEvent OnRewardedVideoClick;

        private bool simpleAd = false;
        private bool rewardedVideoAd = false;

        private int starsToReward;

        // Use this for initialization
        void Start() {
            if (BuildConfiguration.AdsEnabled) {
                StartCoroutine(CheckAds());
            } else {
                NewMessage.SetActive(false);
            }
        }

        private IEnumerator CheckAds() {
            while (true) {
                //simpleAd = AdsCooldown.CanShowAd() && Advertisement.IsReady();
                //rewardedVideoAd = AdsCooldown.CanShowRewardedVideo() && Advertisement.IsReady("rewardedVideo");
                bool isNativeLoaded   = AdsManager.CanShowNativeAd();
				bool isRewardedLoaded = true;//AdsManager.IsVideoLoaded();

                if (!isNativeLoaded) {
                    AdsManager.LoadNativeAd();
                }
                if (!isRewardedLoaded) {
                    AdsManager.LoadVideoAd();
                }

                rewardedVideoAd = AdsCooldown.CanShowRewardedVideo() && isRewardedLoaded;
                simpleAd        = AdsCooldown.CanShowAd() && isNativeLoaded;

                //Debug.Log("reward bool " + rewardedVideoAd);
                //Debug.Log("ad bool " + simpleAd);                

                if (rewardedVideoAd || simpleAd) {
                    NewMessage.SetActive(true);                    
                } else {
                    NewMessage.SetActive(false);
                }

                yield return new WaitForEndOfFrame();
            }
        }

        private void ShowSimpleAd() {
            NativeAdPanel.SetActive(true);
            AdsManager.ShowNativeAd();
            AdsCooldown.UpdateLastAdTime();
            NewMessage.SetActive(false);            
        }

        private void ShowRewardedVideo() {
            // var options = new ShowOptions { resultCallback = HandleShowResult };
            //Advertisement.Show("rewardedVideo", options);       
            this.starsToReward = 0;
            AdsManager.ShowVideo();
        }

        void HandleShowResult(ShowResult result) {
            switch (result) {
                case ShowResult.Finished:
                    Debug.Log("Ad Finished");
                    RewardPanel.SetActive(true);
                    break;
            }

            NewMessage.SetActive(false);
        }

        public void RewardPlayer() {
            //StoreInventory.GiveItem(LunaStoreAssets.STARS_CURRENCY_ID, AdsCooldown.StarsToReward);
            StoreInventory.GiveItem(LunaStoreAssets.STARS_CURRENCY_ID, starsToReward);
            AdsCooldown.UpdateLastVideoRewardTime();
            LunaStoreManager.CallBoughtStarsEvent();

            this.starsToReward = 0;

            if (Social.localUser.authenticated) {
                GameSave.WriteSave();
            }

            YupiAnalyticsEventHandler.StarsEvent("Credit", "RewardedAd", AdsCooldown.StarsToReward);
        }

        public void ShowRewardedVideoAd() {
            ShowRewardedVideo();
        }

        public void ShowAvailableAd() {
            if (rewardedVideoAd) {
                OnRewardedVideoClick.Invoke();
            } else {
                ShowSimpleAd();
            }
        }

        public void CloseNativeAd() {
            AdsManager.DestroyNativeAd();
        }

        void OnEnable() {
            AdsManager.OnRewardPlayer += KeepStarsFromGoogleAds;
        }

        private void OnDisable() {
            AdsManager.OnRewardPlayer -= KeepStarsFromGoogleAds;
        }

        private void KeepStarsFromGoogleAds(int stars) {
            Debug.Log("Showing Reward to Player");
            this.starsToReward = stars;            
        }

        private void OnApplicationFocus(bool hasFocus) {
            if (hasFocus && this.starsToReward > 0) {
                ShowRewardPanel();
                NewMessage.SetActive(false);
            }
        }

        private void ShowRewardPanel() {
            Debug.Log("Ad Finished");
            RewardPanel.SetActive(true);            
        }
    }    
}

#endif