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
		public bool EnableYupiTime = true;

		//variáveis que são verificadas nos scripts para determinar as coisas
		public static BuildType CurrentPurchaseType {
			get {return _currentBuild;}
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

		public static bool YupiTimeEnabled {
			get {return _isYupiTimeEnabled;}
			private set {_isYupiTimeEnabled = value;}
		}

		//variáveis internas
		private static BuildType _currentBuild;
		private static bool _isGPGSEnabled;
		private static bool _isFacebookEnabled;
		private static bool _isPushEnabled;
		private static bool _isYupiTimeEnabled;

		void Awake() {
			_currentBuild = PurchaseType;
			_isGPGSEnabled = EnableGPGS;
			_isFacebookEnabled = EnableFacebook;
			_isPushEnabled = EnablePush;
			_isYupiTimeEnabled = EnableYupiTime;
		}

		// Use this for initialization
		void Start () {
			
		}

		// Update is called once per frame
		void Update () {

		}
	}	
}

