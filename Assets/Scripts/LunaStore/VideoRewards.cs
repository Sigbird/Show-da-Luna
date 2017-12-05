using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using YupiPlay.Luna.Store;
using YupiStudios.API.Language;
using UnityEngine.UI;

public class VideoRewards : MonoBehaviour {

    public GameObject Modal;
    public LanguageChooser IntText;
    public LanguageChooser IntText2;
    public GameObject TextSet1;
    public GameObject TextSet2;

    public const string VIDEOREWARDSGIVENKEY = "videoRewardsGiven";
    private const int starsPerVideo = 10;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt(VIDEOREWARDSGIVENKEY) == 1) {
            // já foi
            Destroy(this.gameObject);
            return;
        }

        var numFiles = GetNumberOfFiles();
        
        bool showTextSet1 = false;
        bool showTextSet2 = false;

        int reward = 0;

        if (numFiles > 0) {
            reward = numFiles * starsPerVideo + 150;
            Inventory.Instance.AddToBalance(reward);
            showTextSet1 = true;                                
        }
        if (EligibleforGift() && !showTextSet1) {
            reward = 30;        
            Inventory.Instance.AddToBalance(reward);
            showTextSet2 = true;
        }

        if (showTextSet1 || showTextSet2) {
            Modal.SetActive(true);            
        }

        if (showTextSet1) {
            TextSet1.SetActive(true);
            string text = IntText.GetCurrentText();
            IntText.SetFormattedText(string.Format(text, reward));
        }
        if (showTextSet2) {
            TextSet2.SetActive(true);
            string text = IntText2.GetCurrentText();
            IntText2.SetFormattedText(string.Format(text, reward));
        }

        PlayerPrefs.SetInt(VIDEOREWARDSGIVENKEY, 1);
        PlayerPrefs.Save();
    }

    private int GetNumberOfFiles() {
        try {
            var path = Path.Combine(Application.persistentDataPath, VideoDownload.VIDEODIR);
            var files = Directory.GetFiles(path);
            return files.Length;
        } catch (Exception e) {
            return 0;
        }
    }

    private bool EligibleforGift() {
        var var1 = PlayerPrefs.GetInt(StarsSystemManager.EVENT01_KEY) == 1;
        var var2 = PlayerPrefs.GetInt(GPGSIds.achievement_welcome_to_earth_to_luna) == 1;

        return var1 || var2;        
    }

    public void Close() {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
