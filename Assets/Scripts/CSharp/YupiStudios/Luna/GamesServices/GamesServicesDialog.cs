#if UNITY_ANDROID
using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class GamesServicesDialog : MonoBehaviour {
    public GameObject DialogContainer;
    public GameObject ParentalGate;

    private bool ShowParentalGate = false;

	void Start() {
		
	}
	public void CallSignIn() {
        if (ShowParentalGate) {
            ParentalGate.SetActive(true);
            ShowParentalGate = false;
        } else {
            GamesServicesSignIn.SignIn();
        }		
	}


    public void ActivateDialog(bool showParentalGate) {        
        ShowParentalGate = showParentalGate;                    
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