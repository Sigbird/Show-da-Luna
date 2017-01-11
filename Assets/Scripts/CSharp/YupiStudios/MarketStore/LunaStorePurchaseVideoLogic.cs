using UnityEngine;
using System.Collections;

public class LunaStorePurchaseVideoLogic : MonoBehaviour {

	private float UPDATE_MAX_DELAY = 1.0f;
	private float updateDelay;

	[Tooltip("Numero do video ou da coleçao")]
	public static int itemNumber;
	public static int itemCol;
	public GameObject purchaseObject;
	public GameObject purchaseCelebration;

	private LunaStoreManager instance;
	private bool purchased;

	void Start(){
		purchased = false;
		instance = LunaStoreManager.Instance;
		purchased = CheckIfPurchased ();
		//SetDelayToMax ();
		if (purchased)
			gameObject.SetActive (false);
	}
	
	void Update(){
//		if (!purchased) {
//			updateDelay -= Time.deltaTime;
//			if(updateDelay <= 0){
//				TryToUpdate();
//				SetDelayToMax();
//			}
//		}
	}

	private void SetDelayToMax() 
	{
		updateDelay = UPDATE_MAX_DELAY;
	}

	private void ProcessPurchased(){
		purchaseObject.SetActive (false);
		purchaseCelebration.SetActive (true);
		purchaseCelebration.GetComponent<AudioSource> ().PlayOneShot (purchaseCelebration.GetComponent<AudioSource> ().clip);
	}

	private bool CheckIfPurchased(){
		return (LunaStoreManager.Instance  != null && LunaStoreManager.Instance.AcquiredVideo(itemNumber,itemCol));
	}

	private void TryToUpdate(){
		if (instance && instance.NeedUpdate) {
			purchased = CheckIfPurchased();
			if(purchased)
				ProcessPurchased();
				gameObject.SetActive(false);
		}
	}
}
