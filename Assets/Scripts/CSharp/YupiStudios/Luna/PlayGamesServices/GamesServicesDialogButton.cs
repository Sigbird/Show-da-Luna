using UnityEngine;
using System.Collections;
using YupiPlay.Luna;

public class GamesServicesDialogButton : MonoBehaviour {
	void Awake() {
		if (!BuildConfiguration.GPGSEnabled) {
			this.gameObject.SetActive(false);
		}
	}
	// Use this for initialization
	void Start () {


		if (!Social.localUser.authenticated) {
			this.gameObject.SetActive(true);
		}

		int loadedSave = PlayerPrefs.GetInt(GameSave.LOADEDSAVEKEY);
		if (loadedSave == 1) {            
			GamesServicesSignIn.SignIn();
			this.gameObject.SetActive(false);
		} 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
