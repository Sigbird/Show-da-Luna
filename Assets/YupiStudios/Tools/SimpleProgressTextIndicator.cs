using UnityEngine;
using UnityEngine.UI;

public class SimpleProgressTextIndicator : MonoBehaviour, IDownloadListener {
	public VideoDownload download;
	public Text text;

	private bool downloadComplete = false;
	private string error = null;

	private float oldProgress = 0f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!downloadComplete) {
			OnProgress();

			if (download.hasError()) {
				OnDownloadError();
				return;
			}

			if (download.IsDone()) {
				OnDownloadComplete();

				if (!string.IsNullOrEmpty(download.GetError())) {
					OnDownloadError(error);
				}
				return;
			}				
		}			
	}

	public void OnProgress() {
		bool isdone = download.IsDone();

		if (string.IsNullOrEmpty(text.text) && !isdone) {
			OnRequestStarted();
		}

		float rawProgress = download.GetProgress();

		if (rawProgress > 0f) {
			oldProgress = rawProgress;

			float progress = 100f * rawProgress;
			text.text = ((int) progress) + "%";
			return;
		}			
	}

	public void OnRequestStarted() {
		if (download.IsDownloadStarted()) {			
			text.gameObject.SetActive(true);
			text.text = "0%";
		}
	}

	public void OnDownloadComplete() {
		text.text = "100%";
		if (string.IsNullOrEmpty(download.GetError())) {
			downloadComplete = true;
			text.text = null;
		}
	}

	public void OnDownloadError(string error = null) {
		text.text = null;
	}
}
