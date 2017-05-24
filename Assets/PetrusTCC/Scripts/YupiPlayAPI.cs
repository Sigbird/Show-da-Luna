namespace YupiStudios.YupiPlay.API {
	using YupiStudios.YupiPlay.Plugin;
	using UnityEngine;


	public class YupiPlayAPI {
		private static YupiPlayAPI instance = null;
		private YupiPlayPlugin plugin = null;

#if UNITY_ANDROID
        private AndroidJavaObject apiInstance = null;
#endif

		public static YupiPlayAPI getInstance() {
			if (instance == null) {
				instance = new YupiPlayAPI();
			}
			return instance;
		}

		private YupiPlayAPI() {

		}

		public bool init(string gameId, string apiKey) {
#if UNITY_ANDROID
			plugin = YupiPlayPlugin.getInstance();

			if (plugin != null) {
				apiInstance = plugin.getApiInstance(gameId, apiKey);
				if (apiInstance != null) {
					return true;
				}
			}
#endif
			return false;
		}

		public void startLevel(int level, string levelName) {
			this._startLevel (level, levelName);
		}

		public void finishLevel(int level, string levelName, int levelScore, int rating) {
			this._finishLevel(level, levelName, levelScore, rating);
		}

		public void registerAchievement(string achievementId) {
			this._registerAchievement(achievementId);
		}

		public void startSession() {
			this._startSession();
		}

		public void finishSession() {
			this._finishSession ();
		}

		public void startLevel(int level, string levelName, int trial){
			this._startLevel(level, levelName, trial);
		}

		public void finishLevel(int level, string levelName, int levelScore, int rating, int trial) {
			this._finishLevel(level, levelName, levelScore, rating, trial);
		}

		public void registerAchievement(string achievementId, int level, int trial) {
			this._registerAchievement(achievementId, level, trial);
		}

		public string getChildName() {
			return plugin.getChildName();
		}

		public int getChildAge() {
			return plugin.getChildAge();
		}

		public string getChildGender() {
			return plugin.getChildGender();
		}

		public string getChildId() {
			return plugin.getChildId();
		}

		public string getChildAvatar() {
			return plugin.getChildAvatar();
		}

		//Reposta vai para YupiPlayIcon, onNotificationsReponse
		public void getNotificationsNumber() {
			apiInstance.Call("getNotificationsNumberUnity");
		}

		private bool _startLevel(int level, string levelName) {
			if (apiInstance != null) {
				object[] args = {level, levelName};
				return apiInstance.Call<bool>("startLevel", args);
			}
			return false;
		}

		private bool _finishLevel(int level, string levelName, int levelScore, int rating) {
			if (apiInstance != null) {
				object[] args = {level, levelName, levelScore, rating};
				return apiInstance.Call<bool>("finishLevel", args);
			}
			return false;
		}

		private bool _registerAchievement(string achievementId) {
			if (apiInstance != null) {
				object[] args = {achievementId};
				return apiInstance.Call<bool>("registerAchievement", args);
			}
			return false;
		}

		private bool _startSession() {
			if (apiInstance != null) {
				return apiInstance.Call<bool>("startSession");
			}
			return false;
		}

		private bool _finishSession() {
			if (apiInstance != null) {
				return apiInstance.Call<bool>("finishSession");
			}
			return false;
		}

		private bool _startLevel(int level, string levelName, int trial) {
			if (apiInstance != null) {
				object[] args = {level, levelName, trial};
				return apiInstance.Call<bool>("startLevel", args);
			}
			return false;
		}

		private bool _finishLevel(int level, string levelName, int levelScore, int rating, int trial) {
			if (apiInstance != null) {
				object[] args = {level, levelName, levelScore, rating, trial};
				return apiInstance.Call<bool>("finishLevel", args);
			}
			return false;
		}

		private bool _registerAchievement(string achievementId, int level, int trial) {
			if (apiInstance != null) {
				object[] args = {achievementId, level, trial};
				return apiInstance.Call<bool>("registerAchievement", args);
			}
			return false;
		} 
	}
}