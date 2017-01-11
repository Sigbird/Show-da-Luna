using UnityEngine;
using UnityEngine.UI;

public class SimpleProgressTextIndicator : MonoBehaviour, IDownloadListener {
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
			text.text = ((int) progress) + "%";
			return;
		} 

		if (!download.IsDone()) {
			OnRequestStarted();
		}
	}

	public void OnRequestStarted() {
		if (download.IsDownloadStarted()) {
			text.text = "0%";
		}
//		if (download.FileExists()) {
//			text.text = "File Exists";
//		}
	}

	public void OnDownloadComplete() {
		text.text = "100%";
		if (string.IsNullOrEmpty(download.GetError())) {
			downloadComplete = true;
			text.gameObject.SetActive(false);
			//download.PlayVideoOnMobile();
		}
	}

	public void OnDownloadError(string error) {
		text.text = "";
	}
}
