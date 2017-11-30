using UnityEngine;
using System.Collections;

public class LunaStorePurchases : MonoBehaviour {

	public GameObject posFeedbackWindow;
	public GameObject negFeedbackWindow;

	private int amountVideos = 4;
	private int amountCollections = 5;
	//LunaStoreManager instance = LunaStoreManager.Instance;
	
	//public void FeedbackPurchases(){
	//	if (CheckPurchases ())
	//		posFeedbackWindow.SetActive (true);
	//	else
	//		negFeedbackWindow.SetActive (true);
	//}

	//public bool CheckPurchases()
	//{
	//	if (instance != null) {
	//		if(instance.AcquiredFullGame())
	//			return true;
	//		else if(instance.AcquiredCollection(1))
	//			return true;
	//		for(int j = 1; j <= amountCollections; j++){
	//			for(int i = 1; i <= amountVideos; i++){
	//			if(instance.AcquiredVideo(i,j))
	//				return true;
	//			}
	//		}
	//	}
	//	return false;
	//}
}