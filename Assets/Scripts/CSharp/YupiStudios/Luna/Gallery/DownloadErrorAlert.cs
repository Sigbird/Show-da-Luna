using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DownloadErrorAlert : MonoBehaviour {
	public GameObject Container;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnDonwloadError(string error) {
		Container.SetActive(true);
	}

	void OnEnable() {
		VideoDownload.OnDownloadStartError += OnDonwloadError;
	}

	void OnDisable() {
		VideoDownload.OnDownloadStartError -= OnDonwloadError;
	}
}
