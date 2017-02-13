using UnityEngine;

public class DownloadButtonAnim : MonoBehaviour, IDownloadListener {
	public VideoDownload download;
	public Animator animator;
	
	private bool downloadComplete = false;
	private string error = null;

	private bool isAnimating;

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

			if (!string.IsNullOrEmpty(download.GetError())) {
				OnDownloadError(error);
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
		OnRequestStarted();
	}

	public void OnRequestStarted() {
		if (download.IsDownloadStarted() && !animator.GetCurrentAnimatorStateInfo(0).IsName("DownloadAnim")) {
			animator.SetTrigger("AnimateButton");	
		}
	}
	
	public void OnDownloadComplete() {
		if (string.IsNullOrEmpty(download.GetError())) {
			downloadComplete = true;
			animator.SetTrigger("DownloadComplete");
		}
	}
	
	public void OnDownloadError(string error = null) {
		animator.SetTrigger("DownloadIdle");
	}
}
