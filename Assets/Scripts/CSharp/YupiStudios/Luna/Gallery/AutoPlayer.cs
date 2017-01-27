using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class AutoPlayer : MonoBehaviour {

	private string absolutePath;
	private FileInfo[] files;
	private int current = 0;
	private bool isPlaying = false;
	private bool pauseStatus;
	private bool isStopped = true;
	private bool canPlay = false;

	void Awake() {
		absolutePath = System.IO.Path.Combine(Application.persistentDataPath, VideoDownload.VIDEODIR);
		#if UNITY_IOS
		absolutePath = "file://" + absolutePath;
		#endif
	}
	// Use this for initialization
	void Start () {
		DirectoryInfo dir = new DirectoryInfo(absolutePath);
		files = dir.GetFiles();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			isStopped = true;
			canPlay = false;
		}
		if (isPlaying || isStopped) {
			return;
		} 	

		if (!isStopped) {
			canPlay = true;
		}

		StartCoroutine(WaitToPlay());
	}

	private void Play() {
		if (canPlay) {
			PlayList();
		}
	}

	private IEnumerator WaitToPlay() {
		yield return new WaitForSeconds(1);

		Play();
	}

	public void PlayList() {
		if (current == files.Length) {
			current = 0;
		}

		bool test = Handheld.PlayFullScreenMovie(files[current].FullName, Color.black, FullScreenMovieControlMode.Minimal, 
			FullScreenMovieScalingMode.AspectFit);

		if (test) {
			isPlaying = true;
			isStopped = false;
			canPlay = false;
			current++;
		} 
	}

	void OnApplicationFocus(bool hasFocus) {
		if (hasFocus) {
			isPlaying = false;
		}
	}

	void OnApplicationPause(bool pauseStatus) {
		this.pauseStatus = pauseStatus;
	}
}
