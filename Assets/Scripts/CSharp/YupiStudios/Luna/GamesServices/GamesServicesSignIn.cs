using UnityEngine;

#if UNITY_ANDROID
using GooglePlayGames;
#endif

public class GamesServicesSignIn : MonoBehaviour {
    public delegate void SignInResult(bool success);
    public static event SignInResult OnSignInResultEvent;

	public static void SignIn() {
		Social.localUser.Authenticate(authMethod);
	}

	private static void authMethod(bool success) {
		if (success) {            
			if (PlayerPrefs.GetInt(GPGSIds.achievement_welcome_to_earth_to_luna) == 0) {
#if UNITY_ANDROID
                Social.ReportProgress(GPGSIds.achievement_welcome_to_earth_to_luna, 100.0f, (bool done) => {});
#endif
#if UNITY_IOS
                //reporta o achievement com o ID do IOS
                //Social.ReportProgress(ID_DO_IOS, 100.0f, (bool done) => {});
#endif
                PlayerPrefs.SetInt(GPGSIds.achievement_welcome_to_earth_to_luna, 1);
				PlayerPrefs.Save();
			} 

			Debug.Log ("auth success");
			GameSave.InitSave();

		} else {
			Debug.Log ("auth failed");

            //GameSave.CallInitEvent();
		}

        if (OnSignInResultEvent != null) {           
            OnSignInResultEvent(success);
        }
    }

    public static void SignOut() {
#if UNITY_ANDROID
        PlayGamesPlatform.Instance.SignOut();
#endif
    }
}