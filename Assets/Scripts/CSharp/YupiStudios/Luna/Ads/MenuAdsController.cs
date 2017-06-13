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

        public UnityEvent OnRewardedVideoClick;

        private bool simpleAd;
        private bool rewardedVideoAd;

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

                rewardedVideoAd = AdsCooldown.CanShowRewardedVideo() && AdsManager.IsVideoLoaded();

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
            Advertisement.Show();
            AdsCooldown.UpdateLastAdTime();
            NewMessage.SetActive(false);
        }

        private void ShowRewardedVideo() {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
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
            StoreInventory.GiveItem(LunaStoreAssets.STARS_CURRENCY_ID, AdsCooldown.StarsToReward);
            AdsCooldown.UpdateLastVideoRewardTime();
            LunaStoreManager.CallBoughtStarsEvent();

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
    }
}

#endif