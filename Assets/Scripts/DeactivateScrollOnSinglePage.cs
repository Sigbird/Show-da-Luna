using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateScrollOnSinglePage : MonoBehaviour {
	public GameObject Container;
	// Use this for initialization
	void Start () {
		if (Container.transform.childCount == 1) {
			ScrollRect scrollRect = GetComponent<ScrollRect>();
			scrollRect.enabled = false;
			ScrollSnapRect snap = GetComponent<ScrollSnapRect>();
			snap.enabled = false;
		}			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
