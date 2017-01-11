using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Soomla.Store;

public class StarsBalanceUI : MonoBehaviour {
	public Text StarsBalance;
	public GameObject EffectsObject;
	public GameObject StarsAnim;

	void Start() {
		UpdateBalance();
	}

	public void UpdateBalance() {
		bool init = SoomlaStore.Initialized;
		VirtualCurrency starsCurrency = (VirtualCurrency) StoreInfo.GetItemByItemId(LunaStoreAssets.STARS_CURRENCY_ID);		
		StarsBalance.text = starsCurrency.GetBalance().ToString();
	}

	public void BuyStarsEffects() {
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
