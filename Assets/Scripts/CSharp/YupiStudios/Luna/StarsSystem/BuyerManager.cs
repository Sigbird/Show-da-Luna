using UnityEngine;
using System.Collections;

public class BuyerManager : MonoBehaviour {

	public GameObject BuyStarsParental;
	public GameObject BuyStarsWindow;

	public void BuyVideo(){

		//BuyStarsParental.SetActive (true);

		if (LunaStoreManager.Instance.CanAffordItem (LunaStoreAssets.STARS_VIDEO_01_COL_01_LTVG_ITEM_ID)) {
			BuyStarsParental.SetActive(false);
		} else {
			BuyStarsParental.SetActive(true);
		//	BuyStarsWindow.SetActive(true);
		}
	}

	public void BuyCollection(){

		//BuyStarsParental.SetActive(true);

		if (LunaStoreManager.Instance.CanAffordItem (LunaStoreAssets.STARS_COLLECTION01_LTVG_ITEM_ID)) {
				BuyStarsParental.SetActive(false);
		} else {
			BuyStarsParental.SetActive(true);
			//BuyStarsWindow.SetActive(true);
		}
	}

	void OnEnable(){
		LunaStoreManager.OnBuyVideo += BuyVideo;
		LunaStoreManager.OnBuyCollection += BuyCollection;
	}

	void OnDisable(){
		LunaStoreManager.OnBuyVideo -= BuyVideo;
		LunaStoreManager.OnBuyCollection -= BuyCollection;
	}

}
