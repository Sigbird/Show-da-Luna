using System.Collections;
using YupiPlay.Luna;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using YupiPlay.Ads;

public class AdsController : MonoBehaviour {	
	public Button AdButton;
	public Text Clock;
	public GameObject ButtonTexts;
	public GameObject FreeRewardPackage;

	void OnEnable() {		
        if (BuildConfiguration.AdsEnabled) {
            StartCoroutine(CheckAdsAndUpdate());
        } else {
            FreeRewardPackage.SetActive(false);
        }

        //if (AdsCooldown.CanShowRewardedVideo() && Advertisement.IsReady("rewardedVideo")) {
        //    SetButtonToFree();
        //} else if (!AdsCooldown.CanShowRewardedVideo() && Advertisement.IsReady("rewardedVideo")) {
        //    SetButtonToClock();
        //    StartCoroutine(UpdateClock());            
        //} else {
        //    FreeRewardPackage.SetActive(false);
        //}
	}

    private IEnumerator CheckAdsAndUpdate() {
        while (true) {
            bool showRewardVideo = AdsCooldown.CanShowRewardedVideo();
            Debug.Log("show reward vid " + showRewardVideo);
            if (showRewardVideo && Advertisement.IsReady("rewardedVideo")) {
                SetButtonToFree();
            } else if (!AdsCooldown.CanShowRewardedVideo()) {
                SetButtonToClock();
                Clock.text = AdsCooldown.GetRewardedVideoClockString();
            }
            else {
                FreeRewardPackage.SetActive(false);
            }

            yield return new WaitForSecondsRealtime(1);
        }      
    }

    public void SetButtonToFree()
    {
        FreeRewardPackage.SetActive(true);
        ButtonTexts.SetActive(true);
        Clock.gameObject.SetActive(false);
        AdButton.interactable = true;
    }

    public void SetButtonToClock()
    {
        FreeRewardPackage.SetActive(true);
        ButtonTexts.SetActive(false);
        AdButton.interactable = false;
        Clock.gameObject.SetActive(true);
    }       

    void OnDisable() {
        StopAllCoroutines();
	}		

	private void OnVideoLoaded() {
		
	}	
}
