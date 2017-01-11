using UnityEngine;
using System.Collections;

namespace YupiStudios.Luna.Config {

	public class GameConfiguration {

		public enum EGameVersion 
		{
			DemoVersion,
			FullVersion
		}

		public static EGameVersion gameVersion { 
			get {

                if (LunaStoreManager.Instance != null && LunaStoreManager.Instance.AcquiredFullGame())
                    return EGameVersion.FullVersion;
                else 
				    return EGameVersion.DemoVersion;
			}
		}

	}

}