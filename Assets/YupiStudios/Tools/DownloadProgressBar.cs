using UnityEngine;
using UnityEngine.UI;

public class DownloadProgressBar : MonoBehaviour, IDownloadListener {
	public VideoDownload download;
	//public Text text;
	public GameObject ProgressBar;
	public Image Progress;
	
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
		float progress = download.GetProgress();
		
		if (!downloadComplete && progress > 0) {
			Progress.fillAmount = progress;
			return;
		} 
		
		if (!download.IsDone()) {
			OnRequestStarted();
		}
	}
	
	public void OnRequestStarted() {
		if (download.IsDownloadStarted()) {
			Progress.fillAmount = 0f;
			ProgressBar.SetActive(true);
		}
		//		if (download.FileExists()) {
		//			text.text = "File Exists";
		//		}
	}
	
	public void OnDownloadComplete() {
		Progress.fillAmount = 1f;
		if (string.IsNullOrEmpty(download.GetError())) {
			downloadComplete = true;
			ProgressBar.SetActive(false);
			//download.PlayVideoOnMobile();
		}
	}
	
	public void OnDownloadError(string error) {
		ProgressBar.SetActive(false);
	}
}
