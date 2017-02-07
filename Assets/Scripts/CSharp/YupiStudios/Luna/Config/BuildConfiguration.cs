using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna {
	public enum BuildType {IAP, Free}

	public class BuildConfiguration : MonoBehaviour {
		//Setar manualmente
		//[HideInInspector]
		public BuildType PurchaseType = BuildType.IAP;
		//[HideInInspector]
		public bool EnableGPGS = true;
		//[HideInInspector]
		public bool EnableFacebook = true;
		//[HideInInspector]
		public bool EnablePush = true;
		//[HideInInspector]
		public bool EnableYupiPlayButton = true;
		//[HideInInspector]
		public bool EnableVideoDownloads = true;

		//variáveis que são verificadas nos scripts para determinar as coisas
		public static BuildType CurrentPurchaseType {
			get {return _currentBuild == null ? BuildType.IAP : _currentBuild;}
			private set {_currentBuild = value;}
		}

		public static bool FacebookEnabled {
			get {return _isFacebookEnabled;}	
			private set {_isFacebookEnabled = value;}
		}

		public static bool GPGSEnabled {
			get {return _isGPGSEnabled;}
			private set {_isGPGSEnabled = value;}
		}

		public static bool PushEnabled {
			get {return _isPushEnabled;}
			private set {_isPushEnabled = value;}
		}

		public static bool YupiPlayButtonEnabled {
			get {return _isYupiPlayButtonEnabled;}
			private set {_isYupiPlayButtonEnabled = value;}
		}

		public static bool VideoDownloadsEnabled {
			get {return _isVideoDownloadsEnabled;}
			private set {_isVideoDownloadsEnabled = value;}
		}

		//variáveis internas
		private static BuildType _currentBuild;
		private static bool _isGPGSEnabled;
		private static bool _isFacebookEnabled;
		private static bool _isPushEnabled;
		private static bool _isYupiPlayButtonEnabled;
		private static bool _isVideoDownloadsEnabled;

		void Awake() {
			_currentBuild = PurchaseType;
			_isGPGSEnabled = EnableGPGS;
			_isFacebookEnabled = EnableFacebook;
			_isPushEnabled = EnablePush;
			_isYupiPlayButtonEnabled = EnableYupiPlayButton;
			_isVideoDownloadsEnabled = EnableVideoDownloads;
		}			
	}	
}

