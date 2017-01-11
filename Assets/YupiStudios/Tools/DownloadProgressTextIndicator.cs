using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DownloadProgressTextIndicator : MonoBehaviour, IDownloadListener {
	public VideoDownload download;
	public Text text;

	private bool downloadComplete = false;
	private string error = null;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!downloadComplete) {
			if (download.IsDone()) {
				OnDownloadComplete();

				if (!string.IsNullOrEmpty(download.GetError())) {
					OnDownloadError(error);
				}
				return;
			}
		}

		OnProgress();
	}

	public void OnProgress() {
		float progress = 100f * download.GetProgress();
		
		if (!downloadComplete && progress > 0) {
			text.text = "Downloading... " + ((int) progress) + "%";
		}
	}

	public void OnRequestStarted() {
		if (!download.FileExists()) {
			text.text = "Downloading...";
		}
		if (download.FileExists()) {
			text.text = "File Exists";
		}
	}

	public void OnDownloadComplete() {
		text.text = "Dowload Complete";
		if (string.IsNullOrEmpty(download.GetError())) {
			downloadComplete = true;
		}
	}

	public void OnDownloadError(string error) {
		text.text = "Download Error: " + error;
	}
}
