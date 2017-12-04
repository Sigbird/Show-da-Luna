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

    public const string VIDEOREWARDSGIVENKEY = "videoRewardsGiven";
    private const int starsPerVideo = 10;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt(VIDEOREWARDSGIVENKEY) == 1) {
            // já foi
            Destroy(this.gameObject);
            return;
        }

        var numFiles = getNumberOfFiles();
        Debug.Log(numFiles);

        if (numFiles > 0) {

            var reward = numFiles * starsPerVideo;

            Inventory.Instance.AddToBalance(reward);

            Modal.SetActive(true);

            string text = IntText.GetCurrentText();

            IntText.SetFormattedText(string.Format(text, reward));                        
        }

        PlayerPrefs.SetInt(VIDEOREWARDSGIVENKEY, 1);
        PlayerPrefs.Save();
    }

    private int getNumberOfFiles() {
        try {
            var path = Path.Combine(Application.persistentDataPath, VideoDownload.VIDEODIR);
            var files = Directory.GetFiles(path);
            return files.Length;
        } catch (Exception e) {
            return 0;
        }
    }

    public void Close() {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
