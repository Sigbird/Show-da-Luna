using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.Events;

public class GamesServicesDependency : MonoBehaviour {

	public UnityEvent IsLoggedIn;
	public UnityEvent NotLoggedIn;
	public UnityEvent OnLoginSuccess;
    public UnityEvent OnLoginFailure;

	// Use this for inaitialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CheckLogin() {
    #if UNITY_EDITOR
        IsLoggedIn.Invoke();
        return;
    #endif

        if (Social.localUser.authenticated) {
			IsLoggedIn.Invoke();
		} else {
			NotLoggedIn.Invoke();
		}
	}

	public void SignIn() {
		Social.localUser.Authenticate(authMethod);
	}

	private void authMethod(bool success) {
		if (success) {
			if (PlayerPrefs.GetInt(GPGSIds.achievement_welcome_to_earth_to_luna) == 0) {
				Social.ReportProgress(GPGSIds.achievement_welcome_to_earth_to_luna, 100.0f, (bool done) => {});
				PlayerPrefs.SetInt(GPGSIds.achievement_welcome_to_earth_to_luna, 1);
				PlayerPrefs.Save();
			} 

			Debug.Log ("auth success");
			GameSave.InitSave();
			OnLoginSuccess.Invoke();
		} else {
			Debug.Log ("auth failed");
			GameSave.CallInitEvent();
            OnLoginFailure.Invoke();
		}

	}
}
