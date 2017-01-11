using UnityEngine;
using System.Collections;

public class GamesServicesSignIn : MonoBehaviour {



	public static void SignIn() {
		Social.localUser.Authenticate(authMethod);
	}

	private static void authMethod(bool success) {
		if (success) {
			if (PlayerPrefs.HasKey(StarsSystemManager.EVENT01_KEY)) {
				Social.ReportProgress(GPGSIds.achievement_welcome_to_earth_to_luna, 100.0f, (bool done) => {});
			} 

			Debug.Log ("auth success");
			GameSave.InitSave();

		} else {
			Debug.Log ("auth failed");
			GameSave.CallInitEvent();
		}

	}
}
