using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugHelper : MonoBehaviour {
    public Text DebugText;
    public static DebugHelper Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    public void Print(string msg) {
        DebugText.text += msg + "\n";
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
