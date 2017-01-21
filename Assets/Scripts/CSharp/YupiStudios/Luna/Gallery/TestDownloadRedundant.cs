using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YupiPlay;

public class TestDownloadRedundant : MonoBehaviour {

	private bool canStartTest = true;
	DownloadRedundant red;
	// Use this for initialization
	void Start () {		
		
	}

	private IEnumerator waitToTest() {		
		yield return new WaitForSeconds(3);	
		red = DownloadRedundant.Instance;

		Debug.Log(red.IsReady());

		if (red.IsReady()) {			
			Debug.Log(red.GetServerRoundRobin() + "priority: " + red.GetCurrentPriority());
			Debug.Log(red.GetServerRoundRobin() + "priority: " + red.GetCurrentPriority());
			Debug.Log(red.GetServerRoundRobin() + "priority: " + red.GetCurrentPriority());
			Debug.Log(red.GetServerRoundRobin(2) + "priority: " + red.GetCurrentPriority());
			Debug.Log(red.GetServerRoundRobin(2) + "priority: " + red.GetCurrentPriority());
			Debug.Log(red.GetServerRoundRobin(3) + "priority: " + red.GetCurrentPriority());
			Debug.Log(red.GetServerRoundRobin(4) + "priority: " + red.GetCurrentPriority());
		} else {
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (canStartTest) {
			StartCoroutine(waitToTest());
			canStartTest = false;
		}
	}
}
