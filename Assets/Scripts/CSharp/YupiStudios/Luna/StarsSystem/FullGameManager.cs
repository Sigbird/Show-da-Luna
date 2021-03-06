﻿using UnityEngine;
using System.Collections;
using YupiPlay.Luna.Store;
using YupiStudios.Luna.Config;

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
		if (Inventory.Instance.BuyProduct(LunaStoreAssets.STARS_FULL_GAME_ITEM_ID, LunaStoreAssets.STARS_FULL_GAME_PRICE)) {
            UnlockedFullGame();
            FullGameUnlockEffects();
            return;
		}

        BuyStarsParental.SetActive(true);		
	}

	void OnEnable() {
        StoreManager.OnBoughtStarsEvent += CloseWindow;		
	}

	void OnDisable() {
        StoreManager.OnBoughtStarsEvent -= CloseWindow;
    }

	public void FullGameUnlockEffects() {
		UnlockEffects.GetComponent<AudioSource>().Play();
		SpendAnimation.SetActive(true);
		SpendAnimation.GetComponent<Animator>().SetTrigger("Spend");
	}


	public void UnlockedFullGame() {        
		PurchaseCanvas.SetActive(false);
	}
	
	public void CloseWindow(int amount) {
		BuyStarsWindow.SetActive(false);
	}
}
