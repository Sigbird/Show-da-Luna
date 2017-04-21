#if UNITY_ANDROID

using UnityEngine;
using System.Collections;

public class GamesServicesSignIn : MonoBehaviour {



	public static void SignIn() {
		Social.localUser.Authenticate(authMethod);
	}

	private static void authMethod(bool success) {
		if (success) {
			if (PlayerPrefs.GetInt(GPGSIds.achievement_welcome_to_earth_to_luna) == 0) {
				Social.ReportProgress(GPGSIds.achievement_welcome_to_earth_to_luna, 100.0f, (bool done) => {});
				PlayerPrefs.SetInt(GPGSIds.achievement_welcome_to_earth_to_luna, 1);
				PlayerPrefs.Save();
			} 

			Debug.Log ("auth success");
			GameSave.InitSave();

		} else {
			Debug.Log ("auth failed");
			GameSave.CallInitEvent();
		}

	}
}

#endif