using UnityEngine;
using System;

#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
#endif

#if UNITY_IOS
//dependências UNITY_IOS
#endif

public class GameSave {
	//production filename luna_save_production_0003
	private const string FILENAME = "luna_save_production_0003";	

	private const string LOADSAVETITLE_EN = "Restore stars balance and purchased items?";
	private const string LOADSAVETITLE_PT = "Restaurar saldo de estrelas e itens comprados?";
	private const string LOADSAVETITLE_ES = "Restablecer el saldo de estrellas y los artículos comprados?";
	public const string LOADEDSAVEKEY = "loaded_save_on_device";

	public delegate void CallInitEventsAction();
	public static event CallInitEventsAction OnCallInitEvents;

	public static void InitSave() {
#if UNITY_ANDROID
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
		savedGameClient.OpenWithAutomaticConflictResolution(FILENAME, DataSource.ReadNetworkOnly, 
		                                                    ConflictResolutionStrategy.UseOriginal,
		                                                    initOnSavedGameOpened);
#endif
    }

#if UNITY_ANDROID
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
			//OnCallInitEvents();
		}
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

				if (OnCallInitEvents != null) OnCallInitEvents();
			}
		} else {
			Debug.LogError("read fail");
		}
	}
#endif

    public static void LoadSave() {
#if UNITY_ANDROID
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
		savedGameClient.OpenWithAutomaticConflictResolution(FILENAME, DataSource.ReadCacheOrNetwork, 
		                                                    ConflictResolutionStrategy.UseOriginal,
		                                                    onSavedGameOpenedRead);
#endif
    }

#if UNITY_ANDROID
    private static void onSavedGameOpenedRead(SavedGameRequestStatus status, ISavedGameMetadata game) {
		if (status == SavedGameRequestStatus.Success) {
			readSave(game);
		} else {
			Debug.LogError ("save open error");
			error();
		}
	}
#endif

    public static void WriteSave() {
#if UNITY_ANDROID
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
		savedGameClient.OpenWithAutomaticConflictResolution(FILENAME, DataSource.ReadCacheOrNetwork, 
		                                                    ConflictResolutionStrategy.UseOriginal,
		                                                    onSavedGameOpenedWrite);
#endif
	}

#if UNITY_ANDROID
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
            if (OnCallInitEvents != null) OnCallInitEvents();

            // handle reading or writing of saved game.
            Debug.Log ("save success");
		} else {
			// handle error
		}
	}
#endif

    private static void error() {

	}

	public static void CallInitEvent() {
        if (OnCallInitEvents != null) OnCallInitEvents();
    }
	
}
