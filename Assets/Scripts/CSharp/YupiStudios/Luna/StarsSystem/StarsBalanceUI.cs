using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Soomla.Store;
using YupiPlay.Luna;

public class StarsBalanceUI : MonoBehaviour {
	public Text StarsBalance;
	public GameObject EffectsObject;
	public GameObject StarsAnim;
	public GameObject BuyStarsWindow;

	void Awake() {
		if (BuildConfiguration.CurrentPurchaseType == BuildType.Free) {
			gameObject.SetActive(false);
		}
	}
	void Start() {
		UpdateBalance();
	}

	public void UpdateBalance() {		
		if (SoomlaStore.Initialized) {
			VirtualCurrency starsCurrency = (VirtualCurrency) StoreInfo.GetItemByItemId(LunaStoreAssets.STARS_CURRENCY_ID);		
			StarsBalance.text = starsCurrency.GetBalance().ToString();	
		}
	}

	public void BuyStarsEffects() {
		if (BuyStarsWindow != null) {
			BuyStarsWindow.SetActive(false);	
		}

		EffectsObject.SetActive(true);
		EffectsObject.GetComponent<AudioSource>().Play();
		StarsAnim.SetActive(true);
		StarsAnim.GetComponent<Animator>().SetTrigger("Spend");

	}

	void OnEnable() {
		LunaStoreManager.OnBalanceChanged += UpdateBalance;
		LunaStoreManager.OnBoughtStars += BuyStarsEffects;
	}

	void OnDisable() {
		LunaStoreManager.OnBalanceChanged -= UpdateBalance;
		LunaStoreManager.OnBoughtStars -= BuyStarsEffects;
	}		
}
