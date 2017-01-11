using UnityEngine;
using System.Collections;

public class LightStartScript : MonoBehaviour {
	public AudioClip StartAudio;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartingRace(){
		Time.timeScale = 1;
		this.gameObject.SetActive (false);
	}

	public void StartSound(){
		GameObject.Find ("GameController").GetComponent<AudioSource> ().PlayOneShot (StartAudio);
	}
}
