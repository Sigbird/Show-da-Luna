using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using Soomla.Store;
using System;
using MiniJSON;
using System.Text;

public class GameSave : MonoBehaviour {
	//production filename luna_save_production_0003
	private const string FILENAME = "luna_save_production_0003";
	private const string STARS_BALANCE_KEY = "stars_balance";

	private const string LOADSAVETITLE_EN = "Restore stars balance and purchased items?";
	private const string LOADSAVETITLE_PT = "Restaurar saldo de estrelas e itens comprados?";
	private const string LOADSAVETITLE_ES = "Restablecer el saldo de estrellas y los artículos comprados?";
	public const string LOADEDSAVEKEY = "loaded_save_on_device";

	public delegate void CallInitEventsAction();
	public static event CallInitEventsAction OnCallInitEvents;

	public static void InitSave() {
		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
		savedGameClient.OpenWithAutomaticConflictResolution(FILENAME, DataSource.ReadCacheOrNetwork, 
		                                                    ConflictResolutionStrategy.UseOriginal,
		                                                    initOnSavedGameOpened);
	}

	private static void initOnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game) {
		if (status == SavedGameRequestStatus.Success) {
			Debug.LogError ("save open success");

			if (game.TotalTimePlayed == TimeSpan.FromMilliseconds(0f)) {
				newSave(game);
			} else {
				if (PlayerPrefs.GetInt(LOADEDSAVEKEY) == 0) {
					readSave(game);
					//showSelectUI();
				}
				//readSave(game);
			}
		} else {
			Debug.LogError ("save open error");
			error();
			OnCallInitEvents();
		}
	}

	private static void showSelectUI() {
		uint maxToDisplay = 1;
		bool allowCreateNew = false;
		bool allowDelete = false;

		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
		savedGameClient.ShowSelectSavedGameUI(getSaveTitle(), maxToDisplay, allowCreateNew, allowDelete, onSavedGameSelected);

	}

	private static string getSaveTitle() {
		switch (Application.systemLanguage) {
		case SystemLanguage.Portuguese:
			return LOADSAVETITLE_PT;
		case SystemLanguage.Spanish:
			return LOADSAVETITLE_ES;
		}
		return LOADSAVETITLE_EN;
	}

	private static void onSavedGameSelected (SelectUIStatus status, ISavedGameMetadata game) {
		if (status == SelectUIStatus.SavedGameSelected) {
			readSave(game);
		} else {
			// handle cancel or error
		}
	}


	private static void readSave(ISavedGameMetadata game) {
		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
		savedGameClient.ReadBinaryData(game, onSavedGameDataRead);
	}

	private static void onSavedGameDataRead(SavedGameRequestStatus status, byte[] data) {
		if (status == SavedGameRequestStatus.Success) {
			Debug.LogError("read success");
			Debug.LogError(data.Length);
			int size = data.Length;
			Debug.Log (size);
			if (size > 0) {						
				string json = Encoding.UTF8.GetString(data);
				Dictionary<string,object> dict = Json.Deserialize(json) as Dictionary<string,object>;

				restoreStarsBalance(dict);

				restoreSavedItem(LunaStoreAssets.FULLGAME_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.MINIGAME_ASAS_ITEM_ID, dict);

				restoreSavedItem(LunaStoreAssets.COLLECTION01_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.COLLECTION02_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.COLLECTION03_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.COLLECTION04_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.COLLECTION05_LTVG_ITEM_ID, dict);

				restoreSavedItem(LunaStoreAssets.VIDEO_01_COL_01_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_02_COL_01_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_03_COL_01_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_04_COL_01_LTVG_ITEM_ID, dict);

				restoreSavedItem(LunaStoreAssets.VIDEO_01_COL_02_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_02_COL_02_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_03_COL_02_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_04_COL_02_LTVG_ITEM_ID, dict);

				restoreSavedItem(LunaStoreAssets.VIDEO_01_COL_03_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_02_COL_03_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_03_COL_03_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_04_COL_03_LTVG_ITEM_ID, dict);

				restoreSavedItem(LunaStoreAssets.VIDEO_01_COL_04_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_02_COL_04_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_03_COL_04_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_04_COL_04_LTVG_ITEM_ID, dict);

				restoreSavedItem(LunaStoreAssets.VIDEO_01_COL_05_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_02_COL_05_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_03_COL_05_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_04_COL_05_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_05_COL_05_LTVG_ITEM_ID, dict);

				restoreSavedItem(LunaStoreAssets.VIDEO_01_COL_06_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_02_COL_06_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_03_COL_06_LTVG_ITEM_ID, dict);
				restoreSavedItem(LunaStoreAssets.VIDEO_04_COL_06_LTVG_ITEM_ID, dict);

				restoreIntSetting(StarsSystemManager.EVENT01_KEY, dict);
				restoreIntSetting(StarsSystemManager.EVENT03_KEY, dict);
				restoreIntSetting(StarsSystemManager.EVENT04_KEY, dict);

				restoreIntSetting(GPGSIds.achievement_welcome_to_earth_to_luna, dict);
				restoreIntSetting(GPGSIds.achievement_it_is_good_to_see_you_again, dict);
				restoreIntSetting(GPGSIds.achievement_you_finished_earth_to_luna_lets_color, dict);
				restoreIntSetting(GPGSIds.achievement_share_earth_to_luna, dict);
				restoreIntSetting(GPGSIds.achievement_share_a_painting_on_facebook, dict);

				string date  = dict[StarsSystemManager.DATE_KEY] as string;
				PlayerPrefs.SetString(StarsSystemManager.DATE_KEY, date);
				PlayerPrefs.SetInt(LOADEDSAVEKEY, 1);
				PlayerPrefs.Save ();

				OnCallInitEvents();
			}
		} else {
			Debug.LogError("read fail");
		}
	}

	private static void restoreIntSetting(string id, Dictionary<string,object> dict) {
		object o;

		if (dict.TryGetValue(id, out o)) {
			if (o.GetType() == typeof(long)) {
				long setting = (long) o;

				if (setting == 1) {
					PlayerPrefs.SetInt(id, (int) 1);
				}	
			}
		}
	}

	private static long restoreStarsBalance(Dictionary<string, object> values) {
		object o;
		if (values.TryGetValue(LunaStoreAssets.STARS_CURRENCY_ID, out o)) {			
			long balance = (long) o;
			Debug.Log ("balance:" + balance);
			VirtualCurrency starsCurrency = (VirtualCurrency) StoreInfo.GetItemByItemId(LunaStoreAssets.STARS_CURRENCY_ID);
			starsCurrency.ResetBalance((int) balance);

			LunaStoreManager.CallBalanceChangeEvent();
			return balance;
		}

		return 0;
	}

	private static bool restoreSavedItem(string ITEM_ID, Dictionary<string, object> values) {
		if (LunaStoreManager.checkIfPurchased(ITEM_ID)) {
			return true;
		}

		object o;	
		if (values.TryGetValue(ITEM_ID, out o)) {
			bool isPurchased = (bool) o;

			if (isPurchased) {
				StoreInventory.GiveItem(ITEM_ID, 1);
				return true;
			}
		}

		return false;
	}

	public static void LoadSave() {
		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
		savedGameClient.OpenWithAutomaticConflictResolution(FILENAME, DataSource.ReadCacheOrNetwork, 
		                                                    ConflictResolutionStrategy.UseOriginal,
		                                                    onSavedGameOpenedRead);
	}

	private static void onSavedGameOpenedRead(SavedGameRequestStatus status, ISavedGameMetadata game) {
		if (status == SavedGameRequestStatus.Success) {
			readSave(game);
		} else {
			Debug.LogError ("save open error");
			error();
		}
	}

	public static void WriteSave() {
		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
		savedGameClient.OpenWithAutomaticConflictResolution(FILENAME, DataSource.ReadCacheOrNetwork, 
		                                                    ConflictResolutionStrategy.UseOriginal,
		                                                    onSavedGameOpenedWrite);
	}

	private static void onSavedGameOpenedWrite(SavedGameRequestStatus status, ISavedGameMetadata game) {
		if (status == SavedGameRequestStatus.Success) {
			newSave(game);
		} else {
			Debug.LogError ("save open error");
			error();
		}
	}

	private static void newSave(ISavedGameMetadata game) {
		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

		SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

		float seconds = Time.time;
		builder = builder.WithUpdatedPlayedTime(TimeSpan.FromSeconds(Time.time))
			.WithUpdatedDescription("Luna: " + DateTime.Now);

		SavedGameMetadataUpdate updatedMetadata = builder.Build();
		byte[] save = getSaveData();
		savedGameClient.CommitUpdate(game, updatedMetadata, save, onSavedGameWritten);
	}

	private static void onSavedGameWritten (SavedGameRequestStatus status, ISavedGameMetadata game) {
		if (status == SavedGameRequestStatus.Success) {
			PlayerPrefs.SetInt(LOADEDSAVEKEY, 1);
			PlayerPrefs.Save ();
			OnCallInitEvents();
			// handle reading or writing of saved game.
			Debug.Log ("save success");
		} else {
			// handle error
		}
	}

	private static byte[] getSaveData() {
		LunaStoreManager storeManager = LunaStoreManager.Instance;

		VirtualCurrency starsCurrency = (VirtualCurrency) StoreInfo.GetItemByItemId(LunaStoreAssets.STARS_CURRENCY_ID);
		long balance = starsCurrency.GetBalance();

		Dictionary<string,object> dict = new Dictionary<string,object>();

		dict[LunaStoreAssets.STARS_CURRENCY_ID] = balance;
		dict[LunaStoreAssets.FULLGAME_LTVG_ITEM_ID] = storeManager.AcquiredFullGame();
		dict[LunaStoreAssets.MINIGAME_ASAS_ITEM_ID] = storeManager.AcquiredMinigameAsas();

		dict[LunaStoreAssets.COLLECTION01_LTVG_ITEM_ID] = storeManager.AcquiredCollection(1);
		dict[LunaStoreAssets.COLLECTION02_LTVG_ITEM_ID] = storeManager.AcquiredCollection(2);
		dict[LunaStoreAssets.COLLECTION03_LTVG_ITEM_ID] = storeManager.AcquiredCollection(3);
		dict[LunaStoreAssets.COLLECTION04_LTVG_ITEM_ID] = storeManager.AcquiredCollection(4);
		dict[LunaStoreAssets.COLLECTION05_LTVG_ITEM_ID] = storeManager.AcquiredCollection(5);

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


		string json = Json.Serialize(dict);
		return Encoding.UTF8.GetBytes(json);
	}

	private static void error() {

	}

	public static void CallInitEvent() {
		OnCallInitEvents();
	}
	
}
