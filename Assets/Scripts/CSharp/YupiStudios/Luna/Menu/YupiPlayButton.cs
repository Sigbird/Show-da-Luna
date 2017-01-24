using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YupiPlayButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (!YupiPlay.Luna.BuildConfiguration.YupiTimeEnabled) {
			gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
