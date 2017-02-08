using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.Luna;

public class DeleteContentButton : MonoBehaviour {
	public GameObject TxtDelete;
	// Use this for initialization
	void Start () {
		if (BuildConfiguration.VideoDownloadsEnabled == false) {
			TxtDelete.SetActive(false);
			gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
