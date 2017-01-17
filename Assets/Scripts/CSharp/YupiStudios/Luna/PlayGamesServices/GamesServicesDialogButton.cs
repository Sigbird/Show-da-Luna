using UnityEngine;
using System.Collections;

public class GamesServicesDialogButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
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
