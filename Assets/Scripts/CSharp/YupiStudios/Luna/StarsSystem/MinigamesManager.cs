using UnityEngine;
using YupiPlay.Luna;
using YupiPlay.Luna.Store;

public class MinigamesManager : MonoBehaviour {

	//public LunaStoreManager StoreManager;

	public GameObject ParentalBuyStars;

	public GameObject Colorir;

	public GameObject AsasLocked;

	public GameObject AsasUnlocked;

	public GameObject AsasLocked2;
	
	public GameObject AsasUnlocked2;

	public GameObject CaracolLocked;
	
	public GameObject CaracolUnlocked;

	public GameObject CaracolLocked2;

	public GameObject CaracolUnlocked2;

	public GameObject BonecoLocked;

	public GameObject BonecoUnlocked;

	public GameObject BonecoLocked2;

	public GameObject BonecoUnlocked2;

	public GameObject AquarioLocked;

	public GameObject AquarioUnlocked;

	public GameObject AsasUnlockEffects;

	public GameObject StarsSpendAnim;

	public bool FreeAsas = false;
	public bool FreeCaracol = false;
	public bool FreeBoneco = false;
	public bool FreeAquario = false;

	// Use this for initialization
	void Start () {
		MinigamesCheck();
	}
	
	// Update is called once per frame
	void Update () {
		MinigamesCheck ();
	}

	public void PurchaseAsas(){		
        if (Inventory.Instance.BuyProduct(LunaStoreAssets.MINIGAME_ASAS_ITEM_ID, LunaStoreAssets.MINIGAME_ASAS_PRICE)) {
            MinigamesCheck();
            AsasPurchasedEffects();
            return;
        }

        ParentalBuyStars.SetActive(true);  
	}

	public void PurchaseCaracol(){
        if (Inventory.Instance.BuyProduct(LunaStoreAssets.MINIGAME_CARACOL_ITEM_ID, LunaStoreAssets.MINIGAME_CARACOL_PRICE)) {
            MinigamesCheck();
            AsasPurchasedEffects();
            return;
        }

        ParentalBuyStars.SetActive(true);
    }

	public void PurchaseBoneco(){
		if (Inventory.Instance.BuyProduct(LunaStoreAssets.MINIGAME_BONECO_ITEM_ID, LunaStoreAssets.MINIGAME_BONECO_PRICE)) {
			MinigamesCheck();
			AsasPurchasedEffects();
			return;
		}

		ParentalBuyStars.SetActive(true);
	}

	public void PurchaseAquario(){
		if (Inventory.Instance.BuyProduct(LunaStoreAssets.MINIGAME_AQUARIO_ITEM_ID, LunaStoreAssets.MINIGAME_AQUARIO_PRICE)) {
			MinigamesCheck();
			AsasPurchasedEffects();
			return;
		}

		ParentalBuyStars.SetActive(true);
	}

	public void MinigamesCheck(){		
		if (BuildConfiguration.CurrentPurchaseType == BuildType.Free) {
			FreeAsas = true;
			FreeCaracol = true;
			FreeBoneco = true;
			FreeAquario = true;
		}
		
		if (Inventory.Instance.HasProduct(LunaStoreAssets.MINIGAME_ASAS_ITEM_ID) || FreeAsas) {
			AsasUnlocked.SetActive (true);
			AsasUnlocked2.SetActive(true);
			AsasLocked.SetActive (false);
			AsasLocked2.SetActive(false);
		} else {
			AsasUnlocked.SetActive (false);
			AsasUnlocked2.SetActive(false);
			AsasLocked.SetActive (true);
			AsasLocked2.SetActive(true);
		}

		if (Inventory.Instance.HasProduct(LunaStoreAssets.MINIGAME_CARACOL_ITEM_ID) || FreeCaracol) {
			CaracolUnlocked.SetActive (true);
			CaracolLocked.SetActive (false);
			CaracolUnlocked2.SetActive (true);
			CaracolLocked2.SetActive (false);
		} else {
			CaracolUnlocked.SetActive (false);
			CaracolLocked.SetActive (true);
			CaracolUnlocked2.SetActive (false);
			CaracolLocked2.SetActive (true);
		}

		if (Inventory.Instance.HasProduct(LunaStoreAssets.MINIGAME_BONECO_ITEM_ID) || FreeBoneco) {
			BonecoUnlocked.SetActive (true);
			BonecoLocked.SetActive (false);
			BonecoUnlocked2.SetActive (true);
			BonecoLocked2.SetActive (false);
		} else {
			BonecoUnlocked.SetActive (false);
			BonecoLocked.SetActive (true);
			BonecoUnlocked2.SetActive (false);
			BonecoLocked2.SetActive (true);
		}

		if (Inventory.Instance.HasProduct(LunaStoreAssets.MINIGAME_AQUARIO_ITEM_ID) || FreeAquario) {
			AquarioUnlocked.SetActive (true);
			AquarioLocked.SetActive (false);
		} else {
			AquarioUnlocked.SetActive (false);
			AquarioLocked.SetActive (true);
		}

	}

	public void AsasPurchasedEffects() {
		AsasUnlockEffects.GetComponent<AudioSource>().Play();
		StarsSpendAnim.SetActive(true);
		StarsSpendAnim.GetComponent<Animator>().SetTrigger("Spend");
	}

	void OnEnable() {
		
	}

	void OnDisable() {
		
	}

}
