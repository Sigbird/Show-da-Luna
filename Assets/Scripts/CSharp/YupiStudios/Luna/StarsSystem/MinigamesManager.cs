using UnityEngine;
using System.Collections;

public class MinigamesManager : MonoBehaviour {

	//public LunaStoreManager StoreManager;

	public GameObject ParentalBuyStars;

	public GameObject Colorir;

	public GameObject AsasLocked;

	public GameObject AsasUnlocked;

	public GameObject AsasLocked2;
	
	public GameObject AsasUnlocked2;

	public GameObject CaracolLocked;
	
	public GameObject CaracolUnlocked;

	public GameObject AsasUnlockEffects;

	public GameObject StarsSpendAnim;

	public bool FreeAsas = false;
	public bool FreeCaracol = false;

	// Use this for initialization
	void Start () {
		MinigamesCheck ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void PurchaseAsas(){
		LunaStoreManager StoreManager = LunaStoreManager.Instance;

		if(StoreManager.CanAffordItem(LunaStoreAssets.MINIGAME_ASAS_ITEM_ID)){
			StoreManager.PurchaseMinigameAsas();
		}else{
			ParentalBuyStars.SetActive(true);
		}	
	}

	public void PurchaseCaracol(){
		LunaStoreManager StoreManager = LunaStoreManager.Instance;
		
		if(StoreManager.CanAffordItem(LunaStoreAssets.MINIGAME_CARACOL_ITEM_ID)){
			StoreManager.PurchaseMinigameCaracol();
		}else{
			ParentalBuyStars.SetActive(true);
		}	
	}

	public void MinigamesCheck(){
		LunaStoreManager StoreManager = LunaStoreManager.Instance;
		
		if (StoreManager.AcquiredMinigameAsas() || FreeAsas) {
			AsasUnlocked.SetActive (true);
			AsasUnlocked2.SetActive(true);
			AsasLocked.SetActive (false);
			AsasLocked2.SetActive(false);
		} else {
			AsasUnlocked.SetActive (false);
			AsasUnlocked2.SetActive(false);
			AsasLocked.SetActive (true);
			AsasLocked2.SetActive(true);
		}

		if (StoreManager.AcquiredMinigameCaracol() || FreeCaracol) {
			CaracolUnlocked.SetActive (true);
			CaracolLocked.SetActive (false);
		} else {
			CaracolUnlocked.SetActive (false);
			CaracolLocked.SetActive (true);
		}

	}

	public void AsasPurchasedEffects() {
		AsasUnlockEffects.GetComponent<AudioSource>().Play();
		StarsSpendAnim.SetActive(true);
		StarsSpendAnim.GetComponent<Animator>().SetTrigger("Spend");
	}

	void OnEnable() {
		LunaStoreManager.OnAsasPurchased += MinigamesCheck;
		LunaStoreManager.OnAsasPurchased += AsasPurchasedEffects;
	}

	void OnDisable() {
		LunaStoreManager.OnAsasPurchased -= MinigamesCheck;
		LunaStoreManager.OnAsasPurchased -= AsasPurchasedEffects;
	}

}
