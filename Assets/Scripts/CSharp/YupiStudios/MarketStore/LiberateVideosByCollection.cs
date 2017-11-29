using UnityEngine;
using System.Collections;

public class LiberateVideosByCollection : MonoBehaviour {
	
	public GameObject StarsSpendAnim;
	public GameObject PurchaseWindow;

	public delegate void BuyVideoFeedback();
	public static event BuyVideoFeedback FeedbackEvent;

	private float UPDATE_MAX_DELAY = 1.0f;
	private float updateDelay;
	
	public GameObject purchaseCelebration;

	private LunaStoreManager instance;
	private bool purchased;

	public static int collectionNumber;
	[Tooltip("Faz o papel do purchase object")]
	public GameObject[] purchaseObjectsCol1;
	public GameObject[] purchaseObjectsCol2;
	public GameObject[] purchaseObjectsCol3;
	public GameObject[] purchaseObjectsCol4;
	public GameObject[] purchaseObjectsCol5;
	private VideoManager[] managers;
	
	private void VideoPurchaseEffects(string itemID) {		
		PurchaseWindow.SetActive (false);

		StarsSpendAnim.SetActive(true);
		StarsSpendAnim.GetComponent<AudioSource>().Play();
		StarsSpendAnim.GetComponent<Animator>().SetTrigger("Spend");
	}

	private void CollectionPurchaseEffects(string itemID) {
		purchaseCelebration.GetComponent<AudioSource>().Play();
		
		PurchaseWindow.SetActive (false);		
		StarsSpendAnim.SetActive(true);
		StarsSpendAnim.GetComponent<Animator>().SetTrigger("Spend");
	}


	void OnEnable(){
		LunaStoreManager.OnCollectionPurchased += CollectionPurchaseEffects;
		LunaStoreManager.OnVideoPurchased += VideoPurchaseEffects;
	}

	void OnDisable(){
		LunaStoreManager.OnCollectionPurchased -= CollectionPurchaseEffects;
		LunaStoreManager.OnVideoPurchased -= VideoPurchaseEffects;	
	}
}
