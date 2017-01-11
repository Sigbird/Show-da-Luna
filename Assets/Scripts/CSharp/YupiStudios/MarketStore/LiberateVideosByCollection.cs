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
	

	bool CheckIfCollectionPurchase(){
		return (LunaStoreManager.Instance != null && LunaStoreManager.Instance.AcquiredCollection (collectionNumber));
	}

	void UpdateVideosState(){
		if (CheckIfCollectionPurchase ()) {
			foreach(VideoManager video in managers){
				video.ChangeVideoState(VideoManager.VIDEO_STATES.DOWNLOAD);
			}
		}
	}

	void Start(){
//		if (collectionNumber == 1) {
//			for (int i = 0; i < purchaseObjectsCol1.Length; i++) {
//				managers [i] = purchaseObjectsCol1 [i].GetComponent<VideoManager> ();
//			}
//		}
//		if (collectionNumber == 2) {
//			for (int i = 0; i < purchaseObjectsCol2.Length; i++) {
//				managers [i] = purchaseObjectsCol2 [i].GetComponent<VideoManager> ();
//			}
//		}
//		if (collectionNumber == 3) {
//			for (int i = 0; i < purchaseObjectsCol3.Length; i++) {
//				managers [i] = purchaseObjectsCol3 [i].GetComponent<VideoManager> ();
//			}
//		}
//		if (collectionNumber == 4) {
//			for (int i = 0; i < purchaseObjectsCol4.Length; i++) {
//				managers [i] = purchaseObjectsCol4 [i].GetComponent<VideoManager> ();
//			}
//		}
//		if (collectionNumber == 5) {
//			for (int i = 0; i < purchaseObjectsCol5.Length; i++) {
//				managers [i] = purchaseObjectsCol5 [i].GetComponent<VideoManager> ();
//			}
//		}
//		purchased = false;
//		instance = LunaStoreManager.Instance;
//		purchased = CheckIfCollectionPurchase ();
//		//SetDelayToMax ();
//		if (purchased)
//			gameObject.SetActive (false);
	}

	void Update(){
//		UpdateVideosState ();
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

	private void TryToUpdate(){
		if (instance && instance.NeedUpdate) {
			purchased = CheckIfCollectionPurchase();
			if(purchased)
				gameObject.SetActive(false);
		}
	}

	private void ProcessPurchased(string collectionID) {
		

//			foreach(GameObject window in purchaseObjectsCol1)
//				window.SetActive(false);
//
//
//			foreach(GameObject window in purchaseObjectsCol2)
//				window.SetActive(false);
//
//
//			foreach(GameObject window in purchaseObjectsCol3)
//				window.SetActive(false);
//
//
//			foreach(GameObject window in purchaseObjectsCol4)
//				window.SetActive(false);
//
//
//			foreach(GameObject window in purchaseObjectsCol5)
//				window.SetActive(false);

//		if (FeedbackEvent != null)
//			FeedbackEvent();
	}

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
