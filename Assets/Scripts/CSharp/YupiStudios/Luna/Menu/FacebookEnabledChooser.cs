using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacebookEnabledChooser : MonoBehaviour {
	public GameObject FacebookEnabled;
	public GameObject FacebookDisabled;
	// Use this for initialization
	void Start () {
		if (!YupiPlay.Luna.BuildConfiguration.FacebookEnabled) {
			FacebookEnabled.SetActive(false);
			FacebookDisabled.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
