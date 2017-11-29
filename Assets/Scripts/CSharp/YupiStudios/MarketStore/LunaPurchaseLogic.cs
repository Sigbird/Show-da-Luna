using UnityEngine;
using System.Collections;

using YupiStudios.Luna.Config;

public class LunaPurchaseLogic : MonoBehaviour {

	private float UPDATE_MAX_DELAY = 1.0f;
	private float updateDelay;

	private LunaStoreManager instance;
	private bool purchased;

	public GameObject purchaseObject;
	public GameObject purchaseCelebration;

	private void SetDelayToMax() 
	{
		updateDelay = UPDATE_MAX_DELAY;
	}

	private void ProcessPurchased()
	{
		purchaseObject.SetActive (false);
		purchaseCelebration.SetActive (true);
	}

	private bool CheckIfPurchased()
	{
		return (GameConfiguration.gameVersion == GameConfiguration.EGameVersion.FullVersion);
	}		

	void Start()
	{
		purchased = false;		
		purchased = CheckIfPurchased();		

		if (purchased) {
			gameObject.SetActive(false);
		}
	}	
}
