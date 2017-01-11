
using UnityEngine;
using System.Collections;



namespace YupiStudios.API.Utils {


	public class UnityUtils : MonoBehaviour {

		public void LoadScene (string sceneName) {
			Application.LoadLevel(sceneName);		
		}

		public void ResetPlayerPrefs()
		{
			PlayerPrefs.DeleteAll();
			PlayerPrefs.Save();
		}

		public void ResetToVersion(string version_name)
		{
			if (PlayerPrefs.GetString ("version","") != version_name) {
				ResetPlayerPrefs();
				PlayerPrefs.SetString("version",version_name);
				PlayerPrefs.Save();
			}
		}

		public void SetSleepTimeout(int secs)
		{
			Screen.sleepTimeout = secs;
		}

		public void NeverSleep() {
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}

		public void DefaultSleep() {
			Screen.sleepTimeout = SleepTimeout.SystemSetting;
		}

		public void DontDestroyObjectOnLoad (GameObject obj) {
			DontDestroyOnLoad (obj);
		}

		public void DestroyObject (GameObject obj) {
			GameObject.Destroy(obj);
		}

		public void SetTimeScale(float scale) {
			Time.timeScale = scale;
		}

	}

}