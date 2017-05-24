namespace YupiStudios.YupiPlay.Plugin {

	using UnityEngine;

	public class YupiPlayPlugin
	{
	#if UNITY_ANDROID
		private AndroidJavaObject unityBridge = null;	
		private AndroidJavaObject apiInstance = null;
	#endif
		private static YupiPlayPlugin instance = null;


		public static YupiPlayPlugin getInstance() {
			if (instance == null) {
				instance = new YupiPlayPlugin();
			}
			return instance;
		}

		private YupiPlayPlugin() {
#if UNITY_ANDROID
				if (unityBridge == null) {
					try {
						unityBridge = new AndroidJavaObject("com.yupistudios.yupiplay.plugin.GamePlugin");
					} catch {
						unityBridge = null;
					}
				}			
#endif
		}

		public string getAvatar() {
		#if UNITY_ANDROID
			if (unityBridge != null) {
				return unityBridge.Call<string>("getAvatar");
			}
		#endif
			return null;
		}

		public string getChildName() {
			#if UNITY_ANDROID
			if (unityBridge != null) {
				return unityBridge.Call<string>("getChildName");
			}
			#endif
			return null;
		}

		public int getChildAge() {
			#if UNITY_ANDROID
			if (unityBridge != null) {
				return unityBridge.Call<int>("getChildAge");
			}
			#endif
			return -1;
		}

		public string getChildGender() {
			#if UNITY_ANDROID
			if (unityBridge != null) {
				return unityBridge.Call<string>("getChildGender");
			}
			#endif
			return null;
		}

		public string getChildId() {
			#if UNITY_ANDROID
			if (unityBridge != null) {
				return unityBridge.Call<string>("getChildId");
			}
			#endif
			return null;
		}

		public string getChildAvatar() {
			#if UNITY_ANDROID
			if (unityBridge != null) {
				return unityBridge.Call<string>("getChildAvatar");
			}
			#endif
			return null;
		}

		public string getEmail() {
			#if UNITY_ANDROID
				if (unityBridge != null) {
					return unityBridge.Call<string>("getEmail");
				}
			#endif
			return null;
		}

		public string getKey() {
			#if UNITY_ANDROID
			if (unityBridge != null) {
				return unityBridge.Call<string>("getKey");
			}
			#endif
			return null;
		}

		public void startYupiPlay() {
	#if UNITY_ANDROID
			if (unityBridge != null) {
				unityBridge.Call("openYupiPlay");
			}
	#endif
		}

		public void openYupiPlay() {
			#if UNITY_ANDROID
			if (unityBridge != null) {
				unityBridge.Call("openYupiPlay");
			}
			#endif
		}

		public void openAvatar() {
			#if UNITY_ANDROID
			if (unityBridge != null) {
				unityBridge.Call("openAvatar");
			}
			#endif
		}

	#if UNITY_ANDROID
		public AndroidJavaObject getApiInstance(string gameId, string apiKey) {
			if (unityBridge != null) {
				object[] args = {gameId, apiKey};
				apiInstance = unityBridge.Call<AndroidJavaObject>("getApiInstance", args);
			}			
			return apiInstance;
		}
	#endif

#if UNITY_ANDROID
		public void subscribePushChannel(string channel) {
			if (unityBridge != null) {
				object[] args = {channel};
				unityBridge.Call("subscribePushChannel", args);
			}
		}
#endif

		public string getPushMessage() {
			#if UNITY_ANDROID
				if (unityBridge != null) {
					return unityBridge.Call<string>("getPushMessage");
				}				
			#endif
			return null;
		}
	}
}
