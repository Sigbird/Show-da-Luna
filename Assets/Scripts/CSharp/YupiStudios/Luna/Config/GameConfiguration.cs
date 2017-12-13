using UnityEngine;
using System.Collections;
using YupiPlay.Luna.Store;

namespace YupiStudios.Luna.Config {

	public class GameConfiguration {

		public enum EGameVersion 
		{
			DemoVersion,
			FullVersion
		}

		public static EGameVersion gameVersion { 
			get {

                if (Inventory.Instance.HasProduct(LunaStoreAssets.STARS_FULL_GAME_ITEM_ID)) {
                    return EGameVersion.FullVersion;
                } else {
                    return EGameVersion.DemoVersion;
                }				    
			}
		}

	}

}