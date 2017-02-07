using UnityEngine;

public class DownloadButtonAnim : MonoBehaviour, IDownloadListener {
	public VideoDownload download;
	public Animator animator;
	
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
			animator.SetTrigger("AnimateButton");
			return;
		} 
		
		if (!download.IsDone()) {
			OnRequestStarted();
		}
	}
	
	public void OnRequestStarted() {
		if (download.IsDownloadStarted()) {
			//animator.SetTrigger("AnimateButton");
		}
	}
	
	public void OnDownloadComplete() {
		if (string.IsNullOrEmpty(download.GetError())) {
			downloadComplete = true;
			animator.SetTrigger("DownloadComplete");
		}
	}
	
	public void OnDownloadError(string error) {
		animator.SetTrigger("DownloadIdle");
	}
}
