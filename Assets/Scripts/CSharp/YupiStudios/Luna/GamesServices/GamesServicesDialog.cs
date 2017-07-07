#if UNITY_ANDROID
using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class GamesServicesDialog : MonoBehaviour {

	void Start() {
		
	}
	public void CallSignIn() {
		GamesServicesSignIn.SignIn();
	}
}

#endif