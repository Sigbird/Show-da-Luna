﻿using UnityEngine;
using System.Collections;

public class SpendAnimationHelper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DisableAfterAnimation() {
		gameObject.SetActive(false);
	}
}
