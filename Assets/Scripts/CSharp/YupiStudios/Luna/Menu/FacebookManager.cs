using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using Facebook.Unity;
using MiniJSON;
//
public class FacebookManager : MonoBehaviour {
//
//	private byte[] screenshot;
//	private string mCaption;
//	private FacebookDelegate<IGraphResult> mCallback;
//	public GameObject ShareEffects;
//
//	private const string fbPhotoUrlFormat = "https://www.facebook.com/photo.php?fbid={0}";
//
//	void Awake() {
//		if (!FB.IsInitialized) {
//			FB.Init(InitCallback, OnHideUnity);
//		} else {		
//			FB.ActivateApp();
//		}
//	}
//
//	private void InitCallback() {
//		if (FB.IsInitialized) {
//			FB.ActivateApp();
//		} else {
//			Debug.Log ("Failed to Initialize the Facebook SDK");
//		}
//	}
//
//	private void OnHideUnity (bool isGameShown) {
//		if (!isGameShown) {
//			Time.timeScale = 0;
//		} else {
//			Time.timeScale = 0;
//		}
//	}
//
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//
//	public void PhotoToFacebook(string caption, byte[] pngImage, FacebookDelegate<IGraphResult> photoCallback = null) {
//		mCaption = caption;
//		screenshot = pngImage;
//		mCallback = (photoCallback == null) ? dummyCallback : photoCallback;
//
//		if (FB.IsLoggedIn) {
//			share();
//		} else {
//            List<string> perms = new List<string>(){"publish_actions"};
//			FB.LogInWithPublishPermissions(perms, loginCallback);
//		}
//	}
//
//	private void loginCallback(ILoginResult result) {
//		share();
//	}
//
//	private void share() {
//		WWWForm data = new WWWForm();
//		data.AddField("caption", mCaption);
//		data.AddField ("privacy", getPrivacy());
//		//data.AddField ("no_story", "true");
//		data.AddBinaryData("image", screenshot, "Screenshot.png");
//
//		FB.API ("me/photos", HttpMethod.POST, mCallback, data);
//	}
//
//	private void dummyCallback(IGraphResult result) {
//
//	}
//
////	private void photoShareCallback(FBResult result) {
////		if (result.Error != null) {
////			Debug.LogError ("facebook photo error");
////			Debug.LogError (result.Error);
////			return;
////		}	
////	}
//
//	private string getPrivacy() {
//		Hashtable data = new Hashtable();
//		data.Add ("value", "EVERYONE");
//		return Json.Serialize(data);
//	}
//
//	public void ShareEffectsAction() {
//		ShareEffects.SetActive(true);
//		ShareEffects.GetComponent<AudioSource>().Play();
//		ShareEffects.GetComponent<Animator>().SetTrigger("Spend");
//	}
//
//	void OnEnable() {
//		LunaStoreManager.OnBalanceChanged += ShareEffectsAction;
//	}
//
//	void OnDisable() {
//		LunaStoreManager.OnBalanceChanged -= ShareEffectsAction;
//	}
//
}
