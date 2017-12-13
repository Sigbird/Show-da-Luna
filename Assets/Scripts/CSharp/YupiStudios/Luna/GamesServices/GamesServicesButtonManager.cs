using UnityEngine;
using YupiPlay.Luna;
using UnityEngine.UI;
using System.Collections;

public class GamesServicesButtonManager : MonoBehaviour {

	public GameObject LoginText;
	public GameObject LogoutText;

    public GameObject GPGSButton;

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
        GPGSButton.gameObject.SetActive(true);

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
            GPGSButton.GetComponent<Button>().interactable = false;
            GamesServicesSignIn.SignIn();            
        }
	}	    

    public void OnSignInResult(bool success) {               
        if (success) {
            LoginText.SetActive(false);
            LogoutText.SetActive(true);            
        } else {
            LoginText.SetActive(true);
            LogoutText.SetActive(false);
        }
                
        GPGSButton.GetComponent<Button>().interactable = true;        
    }

    void OnEnable() {        
        GamesServicesSignIn.OnSignInResultEvent += OnSignInResult;
        setButtonState();
	}

	void OnDisable() {        
        GamesServicesSignIn.OnSignInResultEvent -= OnSignInResult;
    }
}