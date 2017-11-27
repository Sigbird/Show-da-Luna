using UnityEngine;
using System.Collections;
//using Soomla.Store;

public class FullGameManager : MonoBehaviour {

	public GameObject BuyStarsParental;
	public GameObject PurchaseCanvas;
	public GameObject BuyStarsWindow;
	public GameObject UnlockEffects;
	public GameObject SpendAnimation;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BuyFullGame() {
		if (/*StoreInventory.CanAfford(LunaStoreAssets.STARS_FULL_GAME_ITEM_ID)*/ true) {
			LunaStoreManager.Instance.PurchaseFullGame();
		} else {
			BuyStarsParental.SetActive(true);
		}
	}

	void OnEnable() {
		LunaStoreManager.OnFullGamePurchased += UnlockedFullGame;
		LunaStoreManager.OnFullGamePurchased += FullGameUnlockEffects;
		LunaStoreManager.OnBoughtStars += CloseWindow;
	}

	void OnDisable() {
		LunaStoreManager.OnFullGamePurchased -= UnlockedFullGame;
		LunaStoreManager.OnFullGamePurchased -= FullGameUnlockEffects;
		LunaStoreManager.OnBoughtStars -= CloseWindow;
	}

	public void FullGameUnlockEffects() {
		UnlockEffects.GetComponent<AudioSource>().Play();
		SpendAnimation.SetActive(true);
		SpendAnimation.GetComponent<Animator>().SetTrigger("Spend");
	}


	public void UnlockedFullGame() {
		PurchaseCanvas.SetActive(false);

	}
	
	public void CloseWindow() {
		BuyStarsWindow.SetActive(false);
	}
}
