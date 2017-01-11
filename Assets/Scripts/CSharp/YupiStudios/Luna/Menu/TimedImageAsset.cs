using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class TimedImageAsset : MonoBehaviour {
	
	public Sprite AssetToUse;
	public Sprite DefaultAsset;

	public enum TimedEvents {XMAS};
	public TimedEvents EventToCheck;

	private string XmasEventDate = "2017-01-06";

	void Awake() {
		changeAssetOnDate();
	}

	void OnApplicationFocus(bool hasFocus) {
		if (hasFocus) {
			changeAssetOnDate();
		}
	}

	private void changeImageOnDate(string date) {
		DateTime now = DateTime.Now;
		
		DateTime expireDate = DateTime.Parse(date);
		Image image = GetComponent<Image>();
		
		if (now.CompareTo(expireDate) < 0) {
			image.sprite = AssetToUse;
		} else {
			image.sprite = DefaultAsset;
		}
	}
	private void changeAssetOnDate() {
		if (EventToCheck == TimedEvents.XMAS) {
			changeImageOnDate(XmasEventDate);
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
