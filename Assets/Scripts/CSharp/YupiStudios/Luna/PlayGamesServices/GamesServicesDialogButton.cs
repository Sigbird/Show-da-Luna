using UnityEngine;
using System.Collections;

public class GamesServicesDialogButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey(GameSave.LOADEDSAVEKEY)) {
			GamesServicesSignIn.SignIn();
			this.gameObject.SetActive(false);
		} 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
