using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class GamesServicesDialog : MonoBehaviour {

	public GameObject Container;

	void Start() {

//		if (PlayerPrefs.GetInt(GameSave.LOADEDSAVEKEY) == 0) {
//			Container.SetActive(true);
//		} else {
//			GamesServicesSignIn.SignIn();
//		}
	}
	public void CallSignIn() {
		GamesServicesSignIn.SignIn();
	}
}
