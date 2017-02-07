using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace YupiStudios.Analytics {

	[RequireComponent(typeof(GoogleAnalyticsV3))]
	public class YupiAnalyticsEventHandler : MonoBehaviour {

		private string CurrentLevel;
	
		#region Singleton
		private GoogleAnalyticsV3 googleAnalyticsPlugin;

		private static YupiAnalyticsEventHandler _singleton = null;		

		private static bool started = false;

		/**
		 * Deve ser acessado somente apos metodo Start
		 */
		private static YupiAnalyticsEventHandler Instance
		{
			get {
				return _singleton;
			}
			
			set {
				if (_singleton)
					return;
				else
					_singleton = value;
			}
			
		}

		#endregion


		#region Application
		private static int? _startNumber;
		public static int StartNumber
		{
			get
			{

				if ( !_startNumber.HasValue )
				{
					// Get Current StartNumber

					int val = PlayerPrefs.GetInt("AnalyticsStartN",1);
					_startNumber = val;

					// Increment to next start
					PlayerPrefs.SetInt("AnalyticsStartN",val+1);
					PlayerPrefs.Save();
				}


				return _startNumber ?? 0;

			}
		}

		public static void ApplicationEvent(string act, string label, long ?value = null)
		{
			CallEvent ("Application", act, label, value);
		}
		


		private void RegisterCurrentSceneTime()
		{
			int secs = (int)Time.timeSinceLevelLoad;
			ApplicationEvent ("ExitScene", CurrentLevel, secs);
		}

		private void StartSession()
		{
			googleAnalyticsPlugin.StartSession ();
		}
		
		private void EndSession()
		{
			if ( !string.IsNullOrEmpty(CurrentLevel) )
			{
				RegisterCurrentSceneTime();
			}
			googleAnalyticsPlugin.StopSession ();
		}

		private void UpdateLevel()
		{
			SetCurrentLevel (Application.loadedLevelName);
			googleAnalyticsPlugin.LogScreen(Application.loadedLevelName);
			ApplicationEvent ("EnterScene", Application.loadedLevelName);
		}

		private void SetCurrentLevel(string levelName)
		{
			CurrentLevel = levelName;
		}

		IEnumerator RegisterAppStart()
		{
			yield return new WaitForEndOfFrame();
			
			int times = StartNumber;
			
			ApplicationEvent ("Start", "StartNumber", times);
		}

		void Awake()
		{
			if ( Instance != null ) {
				Destroy (gameObject);
				return;
			}
			
			googleAnalyticsPlugin = GetComponent<GoogleAnalyticsV3> ();
			
			Instance = this;
			DontDestroyOnLoad (gameObject);
		}

		void Start()
		{
			UpdateLevel ();
			
			// Avoid non started plugins
			StartCoroutine (RegisterAppStart());
			
			started = true;
			
		}
		
		void OnApplicationQuit()
		{
			EndSession();
		}
		
		void OnApplicationPause(bool paused)
		{
			if (paused) {
				EndSession();
			} else {
				StartSession();
			}
		}
		
		void OnLevelWasLoaded(int level) {
			
			if (started && Application.loadedLevelName != CurrentLevel)
			{
				if ( !string.IsNullOrEmpty(CurrentLevel) )
				{
					RegisterCurrentSceneTime();
				}
				UpdateLevel();
			}
		}

		#endregion

		public void CallEvent(EventHitBuilder evt)
		{
			googleAnalyticsPlugin.LogEvent(evt);
		}

		private static bool IsInstanceNull()
		{
			if (Instance == null) {
				Debug.LogWarning ("Analytics not started");
				return true;
			} else {
				return false;
			}
		}

		#region Generic Events
		private static void CallEvent(string cat, string act, string lab = null, long ?val = null)
		{
			if ( !IsInstanceNull() ) {
				EventHitBuilder evt = new EventHitBuilder ()
					.SetEventCategory (cat)
						.SetEventAction (act);
				
				if (lab != null)
					evt.SetEventLabel (lab);
				
				if (val.HasValue)
					evt.SetEventValue(val.Value);
				
				Instance.CallEvent(evt);
			}
		}
		#endregion

		#region Commercial Events
		public static void AdvertisingEvent(string act, string label)
		{
			CallEvent ("Advertising", act, label);
		}

		public static void PurchaseEvent(string act, string label)
		{
			CallEvent ("Purchase", act, label);
		}
		#endregion

		#region Yupiplay Events

		public static void YupiPlayEvent(string act, string lab, long ? val = null)
		{
			CallEvent ("YupiPlay", act, lab, val);
		}

		public static void YupiPlayBtnClick()
		{
			int access = PlayerPrefs.GetInt ("AnalyticsYupiplayAccess",1);
			int starts = StartNumber;
			
			YupiPlayEvent ("Yupiplay Click", "Btn Click Number", access);
			YupiPlayEvent ("Yupiplay Click", "Game Start Number", starts);

			PlayerPrefs.SetInt("AnalyticsYupiplayAccess",++access);
		}

		public static void YupiPlayAvatarClick()
		{
			int access = PlayerPrefs.GetInt ("AnalyticsAvatarAccess",1);
			int starts = StartNumber;
			
			YupiPlayEvent ("Avatar Click", "Btn Click Number", access);
			YupiPlayEvent ("Avatar Click", "Game Start Number", starts);
			
			PlayerPrefs.SetInt("AnalyticsAvatarAccess",++access);
		}

		public static void SetAgeEvent(string age)
		{
			YupiPlayEvent ("PlayerAge", age);
		}

		public static void SetGenderEvent(string gender)
		{
			YupiPlayEvent ("PlayerGender", gender);
		}

		public static void AchievementEvent(string achType, string achievementName)
		{
			if (!string.IsNullOrEmpty(achType))
			{
				CallEvent ("Achievement", achType, achievementName);
			}
		}
		#endregion


		#region Game Events
		public static void MenuEvent(string act, string button_name)
		{
			CallEvent ("Menu", act, button_name);
		}

		public static void ClickMenuButton(string button_name)
		{
			MenuEvent ("ButtonClick", button_name);
		}


		public static void InGameEvent(string act, string obj=null, long? val = null)
		{
			if (!IsInstanceNull () ) {

				string evName = "InGame"; 
				if (!string.IsNullOrEmpty (Instance.CurrentLevel)) {
					evName += "_" + Instance.CurrentLevel;
					CallEvent (evName, act, obj, val);
				} else {
					CallEvent (evName, act, obj, val);
				}

			}
		}	

		public static void InGameHitPlayer(string obj)
		{
			InGameEvent ("HitPlayer", obj);
		}

		public static void InGameCollect(string obj)
		{
			InGameEvent ("Collect", obj);
		}

		public static void InGamePlayerAction(string actionName)
		{
			InGameEvent ("PlayerAction", actionName);
		}

		public static void InGamePlayerDeath(long score)
		{
			InGameEvent ("PlayerDeath", "Score", score);
		}

		#endregion

		public static void StarsEvent(string act, string label, long ?value = null)
		{
			CallEvent ("Stars System", act, label, value);
		}

		public static void VirtualItemEvent(string item, string language, long ?value = null) {
			CallEvent("Virtual Items Purchases", item, language, value);
		}
	}

}