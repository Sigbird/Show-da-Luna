using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.Luna;

public class RestoreButton : MonoBehaviour {
	public GameObject TxtRestore;
	// Use this for initialization
	void Start () {
		if (BuildConfiguration.CurrentPurchaseType == BuildType.Free) {
			TxtRestore.SetActive(false);
			gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
