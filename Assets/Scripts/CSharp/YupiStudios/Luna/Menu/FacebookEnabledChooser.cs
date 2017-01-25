using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacebookEnabledChooser : MonoBehaviour {
	public GameObject[] FacebookEnabled;
	public GameObject[] FacebookDisabled;
	// Use this for initialization
	void Start () {
		if (YupiPlay.Luna.BuildConfiguration.FacebookEnabled) {
			foreach (GameObject item in FacebookEnabled) {
				item.SetActive(true);	
			}
			foreach (GameObject item in FacebookDisabled) {
				item.SetActive(false);	
			}				
		} else {
			foreach (GameObject item in FacebookEnabled) {
				item.SetActive(false);	
			}
			foreach (GameObject item in FacebookDisabled) {
				item.SetActive(true);	
			}
		}			
	}
}
