using UnityEngine;

public class StarsSystemManager : MonoBehaviour {

	private const int INITIAL_STAR_COUNT = 0;

	public const string EVENT01_KEY = "event01";
	public const string EVENT03_KEY = "event03";
	public const string EVENT04_KEY = "event04";
	public const string DATE_KEY = "stored_date";

	public StarsBalanceUI BalanceUI;
	public RewardNotification Notification;

	// Use this for initialization
	void Start () {
		//checkInitEvents();
		//GamesServicesSignIn.SignIn();
	}	

	private void checkInitEvents() {
		//LunaStoreManager storeManager = LunaStoreManager.Instance;		

		//evento 01
		if (!PlayerPrefs.HasKey(EVENT01_KEY)) {
			PlayerPrefs.SetInt(EVENT01_KEY, 1);
			PlayerPrefs.Save();
			Notification.Events[0].SetActive(true);
			StarsSystemEvents.Event01();
			BalanceUI.UpdateBalance();
			return;
		}
	}

	
	void OnEnable() {
		GameSave.OnCallInitEvents += checkInitEvents;
	}
	
	void OnDisable() {
		GameSave.OnCallInitEvents += checkInitEvents;
	}
}
