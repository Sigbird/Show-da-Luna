namespace YupiStudios.YupiPlay.Plugin {

	using UnityEngine;
	using YupiStudios.YupiPlay.API;
	
	public class YupiPlayInit : MonoBehaviour {
		
		public string gameId;
		public string apiKey;

		private static YupiPlayAPI apiInstance = null;

		void Awake() {
			apiInstance = YupiPlayAPI.getInstance();

			if (apiInstance != null && apiInstance.init(gameId, apiKey)) {
				return;
			}

			apiInstance = null;
		}

		public static YupiPlayAPI getAPI() {
			return apiInstance;
		}
	}
}
