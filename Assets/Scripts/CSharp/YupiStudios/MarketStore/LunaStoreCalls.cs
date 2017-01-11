using UnityEngine;
using System.Collections;
using Soomla.Store;

public class LunaStoreCalls : MonoBehaviour {

	public int vidNumber{get; set;}
	public int colNumber{get; set;}

	public void RestorePurchase(){
		LunaStoreManager.Instance.RestorePurchase();
	}

	public void PurchaseFullGame()
	{
		LunaStoreManager.Instance.PurchaseFullGame();		
	}

	public void PurchaseCollection()
	{
		LunaStoreManager.Instance.PurchaseCollection (colNumber);
	}

	public void PurchaseIndividualVideo()
	{
		LunaStoreManager.Instance.PurchaseVideo (vidNumber, colNumber);
	}

	public void PurchaseSimplePack()
	{
		LunaStoreManager.Instance.PurchaseSimpleStarPack ();
	}

	public void PurchaseSuperPack()
	{
		LunaStoreManager.Instance.PurchaseSuperStarPack ();
	}

	public void PurchaseMegaPack()
	{
		LunaStoreManager.Instance.PurchaseMegaStarPack ();
	}

	public void PurchaseMiniAsas()
	{
		LunaStoreManager.Instance.PurchaseMinigameAsas ();
	}

	public void Start()
	{
		//LunaStoreManager.Instance.StartIAB ();
	}

	public void OnDestroy()
	{
		//LunaStoreManager.Instance.StopIAB ();
	}


}
