using UnityEngine;
using System.Collections;

public class GamesServicesDialog : MonoBehaviour {

	public GameObject Container;

	void Start() {
		if (!Social.localUser.authenticated) {
			Container.SetActive(true);
		}
	}
	public void CallSignIn() {
		GamesServicesSignIn.SignIn();
	}
}
