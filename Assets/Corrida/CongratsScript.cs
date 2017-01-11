using UnityEngine;
using System.Collections;

public class CongratsScript : MonoBehaviour {
	public GameObject endingPanel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EndCongrats(){
		endingPanel.SetActive (true);
		this.gameObject.SetActive (false);
	}
}
