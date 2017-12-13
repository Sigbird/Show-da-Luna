using System.Collections.Generic;
using UnityEngine;
using System.Text;
using MiniJSON;
using YupiPlay.Ads;
using YupiPlay.Luna.Store;

public class SaveData {

	public static byte[] GetBytes() {        
        var inventory = Inventory.Instance;
        
        Dictionary<string, object> dict = new Dictionary<string, object>();

        dict[Inventory.WalletKey] = inventory.GetBalance();

        dict[LunaStoreAssets.STARS_FULL_GAME_ITEM_ID] = inventory.HasProduct(LunaStoreAssets.STARS_FULL_GAME_ITEM_ID);
        dict[LunaStoreAssets.MINIGAME_ASAS_ITEM_ID] = inventory.HasProduct(LunaStoreAssets.MINIGAME_ASAS_ITEM_ID);
        dict[LunaStoreAssets.MINIGAME_CARACOL_ITEM_ID] = inventory.HasProduct(LunaStoreAssets.MINIGAME_CARACOL_ITEM_ID);

        dict[LunaStoreAssets.COLLECTION01_LTVG_ITEM_ID] = inventory.AcquiredCollection(1);
        dict[LunaStoreAssets.COLLECTION02_LTVG_ITEM_ID] = inventory.AcquiredCollection(2);
        dict[LunaStoreAssets.COLLECTION03_LTVG_ITEM_ID] = inventory.AcquiredCollection(3);
        dict[LunaStoreAssets.COLLECTION04_LTVG_ITEM_ID] = inventory.AcquiredCollection(4);
        dict[LunaStoreAssets.COLLECTION05_LTVG_ITEM_ID] = inventory.AcquiredCollection(5);
        dict[LunaStoreAssets.COLLECTION06_LTVG_ITEM_ID] = inventory.AcquiredCollection(6);
        dict[LunaStoreAssets.COLLECTION07_LTVG_ITEM_ID] = inventory.AcquiredCollection(7);
        dict[LunaStoreAssets.COLLECTION08_LTVG_ITEM_ID] = inventory.AcquiredCollection(8);
        dict[LunaStoreAssets.COLLECTION09_LTVG_ITEM_ID] = inventory.AcquiredCollection(9);

        dict[LunaStoreAssets.VIDEO_01_COL_01_LTVG_ITEM_ID] = inventory.AcquiredVideo(1, 1);
        dict[LunaStoreAssets.VIDEO_02_COL_01_LTVG_ITEM_ID] = inventory.AcquiredVideo(2, 1);
        dict[LunaStoreAssets.VIDEO_03_COL_01_LTVG_ITEM_ID] = inventory.AcquiredVideo(3, 1);
        dict[LunaStoreAssets.VIDEO_04_COL_01_LTVG_ITEM_ID] = inventory.AcquiredVideo(4, 1);

        dict[LunaStoreAssets.VIDEO_01_COL_02_LTVG_ITEM_ID] = inventory.AcquiredVideo(1, 2);
        dict[LunaStoreAssets.VIDEO_02_COL_02_LTVG_ITEM_ID] = inventory.AcquiredVideo(2, 2);
        dict[LunaStoreAssets.VIDEO_03_COL_02_LTVG_ITEM_ID] = inventory.AcquiredVideo(3, 2);
        dict[LunaStoreAssets.VIDEO_04_COL_02_LTVG_ITEM_ID] = inventory.AcquiredVideo(4, 2);

        dict[LunaStoreAssets.VIDEO_01_COL_03_LTVG_ITEM_ID] = inventory.AcquiredVideo(1, 3);
        dict[LunaStoreAssets.VIDEO_02_COL_03_LTVG_ITEM_ID] = inventory.AcquiredVideo(2, 3);
        dict[LunaStoreAssets.VIDEO_03_COL_03_LTVG_ITEM_ID] = inventory.AcquiredVideo(3, 3);
        dict[LunaStoreAssets.VIDEO_04_COL_03_LTVG_ITEM_ID] = inventory.AcquiredVideo(4, 3);

        dict[LunaStoreAssets.VIDEO_01_COL_04_LTVG_ITEM_ID] = inventory.AcquiredVideo(1, 4);
        dict[LunaStoreAssets.VIDEO_02_COL_04_LTVG_ITEM_ID] = inventory.AcquiredVideo(2, 4);
        dict[LunaStoreAssets.VIDEO_03_COL_04_LTVG_ITEM_ID] = inventory.AcquiredVideo(3, 4);
        dict[LunaStoreAssets.VIDEO_04_COL_04_LTVG_ITEM_ID] = inventory.AcquiredVideo(4, 4);

        dict[LunaStoreAssets.VIDEO_01_COL_05_LTVG_ITEM_ID] = inventory.AcquiredVideo(1, 5);
        dict[LunaStoreAssets.VIDEO_02_COL_05_LTVG_ITEM_ID] = inventory.AcquiredVideo(2, 5);
        dict[LunaStoreAssets.VIDEO_03_COL_05_LTVG_ITEM_ID] = inventory.AcquiredVideo(3, 5);
        dict[LunaStoreAssets.VIDEO_04_COL_05_LTVG_ITEM_ID] = inventory.AcquiredVideo(4, 5);
        dict[LunaStoreAssets.VIDEO_05_COL_05_LTVG_ITEM_ID] = inventory.AcquiredVideo(5, 5);

        dict[LunaStoreAssets.VIDEO_01_COL_06_LTVG_ITEM_ID] = inventory.AcquiredVideo(1, 6);
        dict[LunaStoreAssets.VIDEO_02_COL_06_LTVG_ITEM_ID] = inventory.AcquiredVideo(2, 6);
        dict[LunaStoreAssets.VIDEO_03_COL_06_LTVG_ITEM_ID] = inventory.AcquiredVideo(3, 6);
        dict[LunaStoreAssets.VIDEO_04_COL_06_LTVG_ITEM_ID] = inventory.AcquiredVideo(4, 6);

        dict[LunaStoreAssets.VIDEO_01_COL_07_LTVG_ITEM_ID] = inventory.AcquiredVideo(1, 7);
        dict[LunaStoreAssets.VIDEO_02_COL_07_LTVG_ITEM_ID] = inventory.AcquiredVideo(2, 7);
        dict[LunaStoreAssets.VIDEO_03_COL_07_LTVG_ITEM_ID] = inventory.AcquiredVideo(3, 7);
        dict[LunaStoreAssets.VIDEO_04_COL_07_LTVG_ITEM_ID] = inventory.AcquiredVideo(4, 7);
        dict[LunaStoreAssets.VIDEO_05_COL_07_LTVG_ITEM_ID] = inventory.AcquiredVideo(5, 7);

        dict[LunaStoreAssets.VIDEO_01_COL_08_LTVG_ITEM_ID] = inventory.AcquiredVideo(1, 8);
        dict[LunaStoreAssets.VIDEO_02_COL_08_LTVG_ITEM_ID] = inventory.AcquiredVideo(2, 8);
        dict[LunaStoreAssets.VIDEO_03_COL_08_LTVG_ITEM_ID] = inventory.AcquiredVideo(3, 8);
        dict[LunaStoreAssets.VIDEO_04_COL_08_LTVG_ITEM_ID] = inventory.AcquiredVideo(4, 8);
        dict[LunaStoreAssets.VIDEO_05_COL_08_LTVG_ITEM_ID] = inventory.AcquiredVideo(5, 8);

        dict[LunaStoreAssets.VIDEO_01_COL_09_LTVG_ITEM_ID] = inventory.AcquiredVideo(1, 9);
        dict[LunaStoreAssets.VIDEO_02_COL_09_LTVG_ITEM_ID] = inventory.AcquiredVideo(2, 9);
        dict[LunaStoreAssets.VIDEO_03_COL_09_LTVG_ITEM_ID] = inventory.AcquiredVideo(3, 9);
        dict[LunaStoreAssets.VIDEO_04_COL_09_LTVG_ITEM_ID] = inventory.AcquiredVideo(4, 9);
        dict[LunaStoreAssets.VIDEO_05_COL_09_LTVG_ITEM_ID] = inventory.AcquiredVideo(5, 9);

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

    public static void LoadFromBytes(byte[] data) {
        string json = Encoding.UTF8.GetString(data);
        Dictionary<string, object> dict = Json.Deserialize(json) as Dictionary<string, object>;

        RestoreStarsBalance(dict);

        RestoreSavedItem(LunaStoreAssets.FULLGAME_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.MINIGAME_ASAS_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.MINIGAME_CARACOL_ITEM_ID, dict);

        RestoreSavedItem(LunaStoreAssets.COLLECTION01_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.COLLECTION02_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.COLLECTION03_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.COLLECTION04_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.COLLECTION05_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.COLLECTION06_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.COLLECTION07_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.COLLECTION08_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.COLLECTION09_LTVG_ITEM_ID, dict);

        RestoreSavedItem(LunaStoreAssets.VIDEO_01_COL_01_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_02_COL_01_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_03_COL_01_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_04_COL_01_LTVG_ITEM_ID, dict);

        RestoreSavedItem(LunaStoreAssets.VIDEO_01_COL_02_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_02_COL_02_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_03_COL_02_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_04_COL_02_LTVG_ITEM_ID, dict);

        RestoreSavedItem(LunaStoreAssets.VIDEO_01_COL_03_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_02_COL_03_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_03_COL_03_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_04_COL_03_LTVG_ITEM_ID, dict);

        RestoreSavedItem(LunaStoreAssets.VIDEO_01_COL_04_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_02_COL_04_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_03_COL_04_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_04_COL_04_LTVG_ITEM_ID, dict);

        RestoreSavedItem(LunaStoreAssets.VIDEO_01_COL_05_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_02_COL_05_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_03_COL_05_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_04_COL_05_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_05_COL_05_LTVG_ITEM_ID, dict);

        RestoreSavedItem(LunaStoreAssets.VIDEO_01_COL_06_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_02_COL_06_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_03_COL_06_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_04_COL_06_LTVG_ITEM_ID, dict);

        RestoreSavedItem(LunaStoreAssets.VIDEO_01_COL_07_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_02_COL_07_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_03_COL_07_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_04_COL_07_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_05_COL_07_LTVG_ITEM_ID, dict);

        RestoreSavedItem(LunaStoreAssets.VIDEO_01_COL_08_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_02_COL_08_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_03_COL_08_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_04_COL_08_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_05_COL_08_LTVG_ITEM_ID, dict);

        RestoreSavedItem(LunaStoreAssets.VIDEO_01_COL_09_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_02_COL_09_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_03_COL_09_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_04_COL_09_LTVG_ITEM_ID, dict);
        RestoreSavedItem(LunaStoreAssets.VIDEO_05_COL_09_LTVG_ITEM_ID, dict);

        RestoreIntSetting(StarsSystemManager.EVENT01_KEY, dict);
        RestoreIntSetting(StarsSystemManager.EVENT03_KEY, dict);
        RestoreIntSetting(StarsSystemManager.EVENT04_KEY, dict);

        RestoreIntSetting(GPGSIds.achievement_welcome_to_earth_to_luna, dict);
        RestoreIntSetting(GPGSIds.achievement_it_is_good_to_see_you_again, dict);
        RestoreIntSetting(GPGSIds.achievement_you_finished_earth_to_luna_lets_color, dict);
        RestoreIntSetting(GPGSIds.achievement_share_earth_to_luna, dict);
        RestoreIntSetting(GPGSIds.achievement_share_a_painting_on_facebook, dict);

        string date = dict[StarsSystemManager.DATE_KEY] as string;
        PlayerPrefs.SetString(StarsSystemManager.DATE_KEY, date);
        PlayerPrefs.SetInt(GameSave.LOADEDSAVEKEY, 1);        

        if (dict.ContainsKey(AdsCooldown.LastVideoRewardTime)) {
            string lastRewardTime = dict[AdsCooldown.LastVideoRewardTime] as string;
            PlayerPrefs.SetString(AdsCooldown.LastVideoRewardTime, lastRewardTime);
        }

        PlayerPrefs.Save();        
    }

    private static long RestoreStarsBalance(Dictionary<string, object> values) {
        object o;
        int balance;
        long balanceLong = 0;
        long balanceLong2 = 0;

        if (values.TryGetValue(LunaStoreAssets.STARS_CURRENCY_ID, out o)) {
            balanceLong = (long) o;
            Debug.Log("balance previous:" + balanceLong);                                    
        }
        if (values.TryGetValue(Inventory.WalletKey, out o)) {
            balanceLong2 = (long) o;
            Debug.Log("balance:" + balanceLong2);
        }

        if (balanceLong > balanceLong2) {
            balance = (int) balanceLong;
        } else {
            balance = (int) balanceLong2;
        }        

        if (balance > 0) {
            Inventory.Instance.SetBalance(balance);
            return balance;
        }        

        return 0;
    }

    private static bool RestoreSavedItem(string ITEM_ID, Dictionary<string, object> values) {
        var inventory = Inventory.Instance;

        if (inventory.HasProduct(ITEM_ID)) {
            return true;
        }

        object o;
        if (values.TryGetValue(ITEM_ID, out o)) {
            bool isPurchased = (bool)o;

            if (isPurchased) {
                inventory.SetProduct(ITEM_ID);
                return true;
            }
        }

        return false;
    }

    private static void RestoreIntSetting(string id, Dictionary<string, object> dict) {
        object o;

        if (dict.TryGetValue(id, out o)) {
            if (o is long) {
                long setting = (long)o;

                if (setting == 1) {
                    PlayerPrefs.SetInt(id, (int)1);
                }
            }
        }
    }
}
