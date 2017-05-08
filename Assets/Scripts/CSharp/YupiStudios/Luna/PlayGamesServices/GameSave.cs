using UnityEngine;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using Soomla.Store;
using System;
using MiniJSON;
using System.Text;
using YupiPlay.Ads;

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
				} else {
					newSave(game);
				}
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
                SaveData.LoadFromBytes(data);				

				OnCallInitEvents();
			}
		} else {
			Debug.LogError("read fail");
		}
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
		byte[] save = SaveData.GetBytes();
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

	private static void error() {

	}

	public static void CallInitEvent() {
		OnCallInitEvents();
	}
	
}
