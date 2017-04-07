using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AdsController : MonoBehaviour {	
	public Button AdButton;
	public Text Clock;
	public GameObject ButtonTexts;

	void OnEnable() {
		AdsManager.OnVideoLoadedEvent += OnVideoLoaded;

		if (AdsManager.IsVideoAdAvailable()) {
			ButtonTexts.SetActive(true);
		} else {
			ButtonTexts.SetActive(false);
			Clock.gameObject.SetActive(true);
			AdButton.interactable = false;
		}

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
