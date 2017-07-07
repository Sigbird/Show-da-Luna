using UnityEngine;
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

		if (auth) {
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
            GamesServicesSignIn.SignOut();
            LoginText.SetActive(true);
			LogoutText.SetActive(false);

		} else {
			if (PlayerPrefs.GetInt(GameSave.LOADEDSAVEKEY) == 0) {
				GamesServicesSignIn.SignIn();
				return;
			}

			Social.localUser.Authenticate((bool success) => {
				GameSave.WriteSave();
				GamesServicesOn();
			});
		}
	}

	private void GamesServicesOn() {
		LoginText.SetActive(false);
		LogoutText.SetActive(true);
	}

	void OnEnable() {
		GameSave.OnCallInitEvents += GamesServicesOn;
		setButtonState();
	}

	void OnDisable() {
		GameSave.OnCallInitEvents -= GamesServicesOn;
	}
}