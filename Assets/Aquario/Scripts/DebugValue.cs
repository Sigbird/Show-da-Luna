using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugValue : MonoBehaviour {
	Text debugTxt;

	static DebugValue Instance;

	void Awake() {
		if (Instance == null) {
			Instance = this;
			debugTxt = GetComponent<Text>();
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	public static void Show(string tag, string msg) {		
		Instance.debugTxt.text = tag + ": " + msg;		
	}
}
