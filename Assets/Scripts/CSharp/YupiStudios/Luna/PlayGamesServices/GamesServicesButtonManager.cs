using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GamesServicesButtonManager : MonoBehaviour {

	public GameObject LoginText;
	public GameObject LogoutText;
	// Use this for initialization
	void Start () {
	
		setButtonState();

	}

	private void setButtonState() {
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
