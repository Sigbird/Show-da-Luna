using UnityEngine;
using YupiPlay.Luna;
using UnityEngine.UI;

public class GamesServicesDialogButton : MonoBehaviour {
	public Sprite IOS;

	private Image imageComponent;

	void Awake() {
		if (!BuildConfiguration.GPGSEnabled) {
			this.gameObject.SetActive(false);
		}
	}
	// Use this for initialization
	void Start () {
		imageComponent = GetComponent<Image> ();
		#if UNITY_IOS
		imageComponent.sprite = this.IOS;
		#endif

		if (!Social.localUser.authenticated) {
			this.gameObject.SetActive(true);
		}

		int loadedSave = PlayerPrefs.GetInt(GameSave.LOADEDSAVEKEY);        

        if (loadedSave == 1) {            
			GamesServicesSignIn.SignIn();
			this.gameObject.SetActive(false);
		} 
	}

    public void OnSignInResult(bool success) {
        if (success) {
            gameObject.SetActive(false);
        } else {            
            gameObject.SetActive(true);
        }

        GetComponent<Button>().interactable = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnEnable() {
        GamesServicesSignIn.OnSignInResultEvent += OnSignInResult;
    }
    private void OnDisable() {
        GamesServicesSignIn.OnSignInResultEvent -= OnSignInResult;
    }
}