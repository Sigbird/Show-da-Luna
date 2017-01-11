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
	
	private void TryToUpdate()
	{
		if (instance && instance.NeedUpdate) {
			purchased = CheckIfPurchased ();
			if (purchased)
				ProcessPurchased();
		}
	}

	void Start()
	{
		purchased = false;
		instance = LunaStoreManager.Instance;
		purchased = CheckIfPurchased ();
		SetDelayToMax ();
		if (purchased) {
			gameObject.SetActive(false);
		}
	}

	void Update () {
		if (!purchased) {
			updateDelay -= Time.deltaTime;
			if (updateDelay <= 0) {
				TryToUpdate ();
				SetDelayToMax ();
			}
		}
	}
}
