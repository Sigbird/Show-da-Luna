using UnityEngine;
using System.Collections;
using YupiPlay.Luna.Store;

public class LunaStoreCalls : MonoBehaviour {

    public GameObject BuyStarsParental;
    public GameObject BuyStarsWindow;
    public Animator StarsAnim;
    public AudioSource BuyVideoSound;
    public AudioSource BuyCollectionSound;
	public GameObject BabyTutorial;

	public int vidNumber{get; set;}
	public int colNumber{get; set;}
    

	public void RestorePurchase(){
		//LunaStoreManager.Instance.RestorePurchase();
	}	

	public void PurchaseCollection()
	{
        if (Inventory.Instance.PurchaseCollection(colNumber)) {
            BuyStarsParental.SetActive(false);
            StarsAnim.gameObject.SetActive(true);
            StarsAnim.SetTrigger("Spend");
            BuyCollectionSound.gameObject.SetActive(true);
            BuyCollectionSound.Play();
            gameObject.SetActive(false);
            return;
        }

        BuyStarsParental.SetActive(true);
	}

	public void PurchaseIndividualVideo()
	{
		if (Inventory.Instance.PurchaseVideo(vidNumber, colNumber)) {
            BuyStarsParental.SetActive(false);
            StarsAnim.gameObject.SetActive(true);
            StarsAnim.SetTrigger("Spend");
            BuyVideoSound.gameObject.SetActive(true);
            BuyVideoSound.Play();
            gameObject.SetActive(false);

			foreach (GameObject o in GameObject.FindGameObjectsWithTag("Video")) {
				o.GetComponent<VideoManager> ().CheckVideoState ();
			}

            return;
        }

        BuyStarsParental.SetActive(true);
	}

	public void Start()
	{
		if (PlayerPrefs.GetInt ("BabyModeTutorial") == 0) {
			BabyTutorial.SetActive (true);
		}
		//LunaStoreManager.Instance.StartIAB ();
	}

	public void OnDestroy()
	{
		//LunaStoreManager.Instance.StopIAB ();
	}

	public void ClosedBabyMode(){
		PlayerPrefs.SetInt ("BabyModeTutorial", 1);
	}

}
