using System.Collections.Generic;
using UnityEngine;
using Soomla.Store;
using System.Text;
using MiniJSON;
using YupiPlay.Ads;

public class SaveData {

	public static byte[] GetBytes() {
        LunaStoreManager storeManager = LunaStoreManager.Instance;

        VirtualCurrency starsCurrency = (VirtualCurrency)StoreInfo.GetItemByItemId(LunaStoreAssets.STARS_CURRENCY_ID);
        long balance = starsCurrency.GetBalance();

        Dictionary<string, object> dict = new Dictionary<string, object>();

        dict[LunaStoreAssets.STARS_CURRENCY_ID] = balance;
        dict[LunaStoreAssets.FULLGAME_LTVG_ITEM_ID] = storeManager.AcquiredFullGame();
        dict[LunaStoreAssets.MINIGAME_ASAS_ITEM_ID] = storeManager.AcquiredMinigameAsas();

        dict[LunaStoreAssets.COLLECTION01_LTVG_ITEM_ID] = storeManager.AcquiredCollection(1);
        dict[LunaStoreAssets.COLLECTION02_LTVG_ITEM_ID] = storeManager.AcquiredCollection(2);
        dict[LunaStoreAssets.COLLECTION03_LTVG_ITEM_ID] = storeManager.AcquiredCollection(3);
        dict[LunaStoreAssets.COLLECTION04_LTVG_ITEM_ID] = storeManager.AcquiredCollection(4);
        dict[LunaStoreAssets.COLLECTION05_LTVG_ITEM_ID] = storeManager.AcquiredCollection(5);
        dict[LunaStoreAssets.COLLECTION06_LTVG_ITEM_ID] = storeManager.AcquiredCollection(6);

        dict[LunaStoreAssets.VIDEO_01_COL_01_LTVG_ITEM_ID] = storeManager.AcquiredVideo(1, 1);
        dict[LunaStoreAssets.VIDEO_02_COL_01_LTVG_ITEM_ID] = storeManager.AcquiredVideo(2, 1);
        dict[LunaStoreAssets.VIDEO_03_COL_01_LTVG_ITEM_ID] = storeManager.AcquiredVideo(3, 1);
        dict[LunaStoreAssets.VIDEO_04_COL_01_LTVG_ITEM_ID] = storeManager.AcquiredVideo(4, 1);

        dict[LunaStoreAssets.VIDEO_01_COL_02_LTVG_ITEM_ID] = storeManager.AcquiredVideo(1, 2);
        dict[LunaStoreAssets.VIDEO_02_COL_02_LTVG_ITEM_ID] = storeManager.AcquiredVideo(2, 2);
        dict[LunaStoreAssets.VIDEO_03_COL_02_LTVG_ITEM_ID] = storeManager.AcquiredVideo(3, 2);
        dict[LunaStoreAssets.VIDEO_04_COL_02_LTVG_ITEM_ID] = storeManager.AcquiredVideo(4, 2);

        dict[LunaStoreAssets.VIDEO_01_COL_03_LTVG_ITEM_ID] = storeManager.AcquiredVideo(1, 3);
        dict[LunaStoreAssets.VIDEO_02_COL_03_LTVG_ITEM_ID] = storeManager.AcquiredVideo(2, 3);
        dict[LunaStoreAssets.VIDEO_03_COL_03_LTVG_ITEM_ID] = storeManager.AcquiredVideo(3, 3);
        dict[LunaStoreAssets.VIDEO_04_COL_03_LTVG_ITEM_ID] = storeManager.AcquiredVideo(4, 3);

        dict[LunaStoreAssets.VIDEO_01_COL_04_LTVG_ITEM_ID] = storeManager.AcquiredVideo(1, 4);
        dict[LunaStoreAssets.VIDEO_02_COL_04_LTVG_ITEM_ID] = storeManager.AcquiredVideo(2, 4);
        dict[LunaStoreAssets.VIDEO_03_COL_04_LTVG_ITEM_ID] = storeManager.AcquiredVideo(3, 4);
        dict[LunaStoreAssets.VIDEO_04_COL_04_LTVG_ITEM_ID] = storeManager.AcquiredVideo(4, 4);

        dict[LunaStoreAssets.VIDEO_01_COL_05_LTVG_ITEM_ID] = storeManager.AcquiredVideo(1, 5);
        dict[LunaStoreAssets.VIDEO_02_COL_05_LTVG_ITEM_ID] = storeManager.AcquiredVideo(2, 5);
        dict[LunaStoreAssets.VIDEO_03_COL_05_LTVG_ITEM_ID] = storeManager.AcquiredVideo(3, 5);
        dict[LunaStoreAssets.VIDEO_04_COL_05_LTVG_ITEM_ID] = storeManager.AcquiredVideo(4, 5);
        dict[LunaStoreAssets.VIDEO_05_COL_05_LTVG_ITEM_ID] = storeManager.AcquiredVideo(5, 5);

        dict[LunaStoreAssets.VIDEO_01_COL_06_LTVG_ITEM_ID] = storeManager.AcquiredVideo(1, 6);
        dict[LunaStoreAssets.VIDEO_02_COL_06_LTVG_ITEM_ID] = storeManager.AcquiredVideo(2, 6);
        dict[LunaStoreAssets.VIDEO_03_COL_06_LTVG_ITEM_ID] = storeManager.AcquiredVideo(3, 6);
        dict[LunaStoreAssets.VIDEO_04_COL_06_LTVG_ITEM_ID] = storeManager.AcquiredVideo(4, 6);

        dict[StarsSystemManager.EVENT01_KEY] = PlayerPrefs.GetInt(StarsSystemManager.EVENT01_KEY);
        dict[StarsSystemManager.EVENT03_KEY] = PlayerPrefs.GetInt(StarsSystemManager.EVENT03_KEY);
        dict[StarsSystemManager.EVENT04_KEY] = PlayerPrefs.GetInt(StarsSystemManager.EVENT04_KEY);
        dict[StarsSystemManager.DATE_KEY] = PlayerPrefs.GetString(StarsSystemManager.DATE_KEY);

        dict[GPGSIds.achievement_welcome_to_earth_to_luna] = PlayerPrefs.GetInt(GPGSIds.achievement_welcome_to_earth_to_luna);
        dict[GPGSIds.achievement_it_is_good_to_see_you_again] = PlayerPrefs.GetInt(GPGSIds.achievement_it_is_good_to_see_you_again);
        dict[GPGSIds.achievement_you_finished_earth_to_luna_lets_color] = PlayerPrefs.GetInt(GPGSIds.achievement_you_finished_earth_to_luna_lets_color);
        dict[GPGSIds.achievement_share_earth_to_luna] = PlayerPrefs.GetInt(GPGSIds.achievement_share_earth_to_luna);
        dict[GPGSIds.achievement_share_a_painting_on_facebook] = PlayerPrefs.GetInt(GPGSIds.achievement_share_a_painting_on_facebook);

        dict[AdsCooldown.LastVideoRewardTime] = PlayerPrefs.GetString(AdsCooldown.LastVideoRewardTime);

        string json = Json.Serialize(dict);
        return Encoding.UTF8.GetBytes(json);
    }
}
