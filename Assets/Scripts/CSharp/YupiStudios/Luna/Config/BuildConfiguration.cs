using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna {
	public enum BuildType {IAP, Free}

	public class BuildConfiguration : MonoBehaviour {
		//Setar manualmente
		public BuildType PurchaseType = BuildType.IAP;
		public bool EnableGPGS = true;
		public bool EnableFacebook = true;
		public bool EnablePush = true;
		public bool EnableYupiPlayButton = true;
		public bool EnableVideoDownloads = true;

		//variáveis que são verificadas nos scripts para determinar as coisas
		public static BuildType CurrentPurchaseType {
			get {return _currentBuild == null ? BuildType.IAP : _currentBuild;}
		}

		public static bool FacebookEnabled {
			get {return _isFacebookEnabled;}	
		}

		public static bool GPGSEnabled {
			get {return _isGPGSEnabled;}
		}

		public static bool PushEnabled {
			get {return _isPushEnabled;}
		}

		public static bool YupiPlayButtonEnabled {
			get {return _isYupiPlayButtonEnabled;}
		}

		public static bool VideoDownloadsEnabled {
			get {return _isVideoDownloadsEnabled;}
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

