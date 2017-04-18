using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using YupiPlay.Ads;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour {	
	public Button AdButton;
	public Text Clock;
	public GameObject ButtonTexts;
	public GameObject FreeRewardPackage;

	void OnEnable() {		
        if (AdsCooldown.CanShowRewardedVideo() && Advertisement.IsReady("rewardedVideo")) {
            SetButtonToFree();
        } else {
            SetButtonToClock();
            StartCoroutine(UpdateClock());
        }
	}

    private IEnumerator UpdateClock() {
        while (true)
        {            
            if (AdsCooldown.CanShowRewardedVideo()) {
                SetButtonToFree();
            } else {               
                Clock.text = AdsCooldown.GetRewardedVideoClockString();
            }

            yield return new WaitForSecondsRealtime(1);
        }      
    }

    public void SetButtonToFree()
    {
        ButtonTexts.SetActive(true);
        Clock.gameObject.SetActive(false);
        AdButton.interactable = true;
    }

    public void SetButtonToClock()
    {
        ButtonTexts.SetActive(false);
        AdButton.interactable = false;
        Clock.gameObject.SetActive(true);
    }

    private void CheckAdsGoogle()
    {
        AdsManager.OnVideoLoadedEvent += OnVideoLoaded;

        if (AdsManager.IsVideoAdAvailable() || AdsManager.IsOnCooldown())
        {
            EnableFreeRewardPackage();
        } else
        {
            DisableFreeRewardPackage();
        }
    }

    private void EnableFreeRewardPackage()
    {
        ButtonTexts.SetActive(true);
        FreeRewardPackage.SetActive(true);
    }

    private void DisableFreeRewardPackage()
    {
        FreeRewardPackage.SetActive(false);
        ButtonTexts.SetActive(false);
        Clock.gameObject.SetActive(true);
        AdButton.interactable = false;
    }

    void OnDisable() {
		AdsManager.OnVideoLoadedEvent -= OnVideoLoaded;
	}
		

	private void OnVideoLoaded() {
		
	}

	public void ShowVideoAd() {
		AdsManager.ShowVideo();
	}
}
