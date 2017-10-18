#if UNITY_ANDROID

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using Facebook.Unity;
using System;

public class EndGameSharing : MonoBehaviour {
	public GameObject shareScreen;
	public GameObject successScreen;
	public GameObject failureScreen;
	private bool sucess;
	public GameObject ShareEffects;

	private const string SHARELINK = "https://play.google.com/store/apps/details?id=com.YupiPlay.Luna&referrer=utm_source%3DLunaEndGameShare%26utm_medium%3Dapp%26anid%3Dadmob";

	private const string FINISH_PT = "Você finalizou O Show da Luna! Vamos Colorir!";
	private const string FINISH_EN = "You finished Earth to Luna! Let's Color!";
	private const string FINISH_ES = "Terminaste El Mundo de Luna! Vamos a color!";

	// Use this for initialization
	void Start () {
		StarsSystemEvents.FinishGameAchievement();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CashIn() {
		//VANVAN ATRIBUI X ESTRELAS AO JOGADOR
		//VANVAN CONFIGURA COMPARTILHAMENTO
//		if (FB.IsLoggedIn) {
//			ShareFeed();
//		} else {        
//            List<string> perms = new List<string>(){"public_profile"};
//			FB.LogInWithReadPermissions(perms, FBLoginCallback);
//		}
	}

//	public void FBLoginCallback(ILoginResult result) {
//		if (result.Error != null) {
//			failureScreen.SetActive(true);
//			shareScreen.SetActive (false);
//		} else {
//			ShareFeed();
//		}
//	}

	public void ShareFeed() {
//        Uri uri = new Uri(SHARELINK);
//		FB.ShareLink(uri, getShareTitle(), null,  null, ShareCallback);
	}

//	public void ShareCallback(IShareResult result) {
//		if (result.Error != null) {
//			failureScreen.SetActive(true);
//			shareScreen.SetActive (false);
//		} else {
//			successScreen.SetActive(true);
//			shareScreen.SetActive (false);
//			StarsSystemEvents.Event04();
//		}
//	}

	private static string getShareTitle() {
		switch(Application.systemLanguage) {
		case SystemLanguage.Portuguese:
			return FINISH_PT;
		case SystemLanguage.English:
			return FINISH_EN;
		case SystemLanguage.Spanish:
			return FINISH_ES;
		}

		return FINISH_PT;
	}

	void Awake() {
//		if (!FB.IsInitialized) {
//			FB.Init(InitCallback, OnHideUnity);
//		} else {		
//			FB.ActivateApp();
//		}
	}
	
	private void InitCallback() {
//		if (FB.IsInitialized) {
//			FB.ActivateApp();
//		} else {
//			Debug.Log ("Failed to Initialize the Facebook SDK");
//		}
	}
	
	private void OnHideUnity (bool isGameShown) {
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 0;
		}
	}

	public void ShareStarsEffects() {
		Debug.Log ("test");
		ShareEffects.SetActive(true);
		ShareEffects.GetComponent<AudioSource>().Play ();
		ShareEffects.GetComponent<Animator>().SetTrigger("Spend");
	}

	void OnEnable() {
		LunaStoreManager.OnBalanceChanged += ShareStarsEffects;
	}

	void OnDisable() {
		LunaStoreManager.OnBalanceChanged -= ShareStarsEffects;
	}
}

#endif