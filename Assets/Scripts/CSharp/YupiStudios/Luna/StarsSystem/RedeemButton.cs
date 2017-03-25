using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay.Luna;

public class RedeemButton : MonoBehaviour {
	// Use this for initialization
	void Start () {
		if (!BuildConfiguration.RedeemCodeEnabled) {
			this.gameObject.SetActive(false);
		}
	}
}
