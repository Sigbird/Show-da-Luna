using UnityEngine;
using UnityEngine.UI;

public class DownloadProgressBar : MonoBehaviour, IDownloadListener {
	public VideoDownload download;
	//public Text text;
	public GameObject ProgressBar;
	public Image Progress;
	public VideoManager Vmanager;
	
	private bool downloadComplete = false;
	private string error = null;

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
		if (!ProgressBar.activeInHierarchy && !download.IsDone()) {
			OnRequestStarted();
		}

		float progress = download.GetProgress();
		
		if (progress > 0f) {
			Progress.fillAmount = progress;
			return;
		} 
	}
	
	public void OnRequestStarted() {
		if (download.IsDownloadStarted()) {
			Progress.fillAmount = 0f;
			ProgressBar.SetActive(true);
		}
	}
	
	public void OnDownloadComplete() {
		Progress.fillAmount = 1f;
		if (string.IsNullOrEmpty(download.GetError())) {
			downloadComplete = true;
			ProgressBar.SetActive(false);
			Vmanager.CheckVideoState ();
		}
	}
	
	public void OnDownloadError(string error = null) {
		ProgressBar.SetActive(false);
	}
}
