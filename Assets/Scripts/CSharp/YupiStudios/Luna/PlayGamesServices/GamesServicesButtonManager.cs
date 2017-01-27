using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using YupiPlay.Luna;

public class GamesServicesButtonManager : MonoBehaviour {

	public GameObject LoginText;
	public GameObject LogoutText;
	// Use this for initialization
	void Start () {
		if (!BuildConfiguration.GPGSEnabled) {
			this.gameObject.SetActive(false);
			LoginText.SetActive(false);
			LogoutText.SetActive(false);
			return;
		}
	
		setButtonState();
	}

	private void setButtonState() {
        bool auth = Social.localUser.authenticated;
        Debug.Log(auth);

		if (Social.localUser.authenticated) {
			LoginText.SetActive(false);
			LogoutText.SetActive(true);
		} else {
			LoginText.SetActive(true);
			LogoutText.SetActive(false);
		}
	}
		
	// Update is called once per frame
	void Update () {
	
	}

	public void ButtonAction() {
		if (Social.localUser.authenticated) {
			PlayGamesPlatform.Instance.SignOut();
			LoginText.SetActive(true);
			LogoutText.SetActive(false);

		} else {
			if (PlayerPrefs.GetInt(GameSave.LOADEDSAVEKEY) == 0) {
				GamesServicesSignIn.SignIn();
				return;
			}

			Social.localUser.Authenticate((bool success) => {
				GameSave.WriteSave();
				PlayGamesOn();
			});
		}
	}

	private void PlayGamesOn() {
		LoginText.SetActive(false);
		LogoutText.SetActive(true);
	}

	void OnEnable() {
		GameSave.OnCallInitEvents += PlayGamesOn;
		setButtonState();
	}

	void OnDisable() {
		GameSave.OnCallInitEvents -= PlayGamesOn;
	}
}
