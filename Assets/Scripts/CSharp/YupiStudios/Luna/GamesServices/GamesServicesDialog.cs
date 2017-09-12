#if UNITY_ANDROID
using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class GamesServicesDialog : MonoBehaviour {
    public GameObject DialogContainer;

	void Start() {
		
	}
	public void CallSignIn() {
		GamesServicesSignIn.SignIn();
	}


    public void ActivateDialog() {
        DialogContainer.SetActive(true);
    }

    void OnEnable() {
        LunaStoreManager.OnPurchaseTrySaveEvent += ActivateDialog;
    }

    void OnDisable() {
        LunaStoreManager.OnPurchaseTrySaveEvent -= ActivateDialog;
    }
}

#endif