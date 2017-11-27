using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using Soomla.Store;
using YupiPlay.Luna;
using YupiPlay.Luna.Store;

public class StarsBalanceUI : MonoBehaviour {
	public Text StarsBalance;
	public GameObject EffectsObject;
	public GameObject StarsAnim;
	public GameObject BuyStarsWindow;

	void Awake() {
		if (BuildConfiguration.CurrentPurchaseType == BuildType.Free) {
			gameObject.SetActive(false);
		}
	}
	void Start() {
		UpdateBalance();


        //pega saldo do inventário novo, para testes
#if UNITY_EDITOR
        //StarsBalance.text = Inventory.Instance.GetBalance().ToString();
#endif
    }

	public void UpdateBalance() {        
        StarsBalance.text = Inventory.Instance.GetBalance().ToString();
        if (/*SoomlaStore.Initialized*/ true) {
            //VirtualCurrency starsCurrency = (VirtualCurrency)StoreInfo.GetItemByItemId(LunaStoreAssets.STARS_CURRENCY_ID);
            //StarsBalance.text = starsCurrency.GetBalance().ToString();
        }        
    }

	public void BuyStarsEffects() {
		if (BuyStarsWindow != null) {
			BuyStarsWindow.SetActive(false);	
		}

		EffectsObject.SetActive(true);
		EffectsObject.GetComponent<AudioSource>().Play();
		StarsAnim.SetActive(true);
		StarsAnim.GetComponent<Animator>().SetTrigger("Spend");

	}

    private void OnBoughtStars(int amount) {
        //StarsBalance.text = newBalance.ToString();
        BuyStarsEffects();
        UpdateBalance();
    }

    void OnEnable() {
		LunaStoreManager.OnBalanceChanged += UpdateBalance;
		LunaStoreManager.OnBoughtStars += BuyStarsEffects;
        //new
        StoreManager.OnBoughtStarsEvent += OnBoughtStars;
    }

	void OnDisable() {
		LunaStoreManager.OnBalanceChanged -= UpdateBalance;
		LunaStoreManager.OnBoughtStars -= BuyStarsEffects;
        //new
        StoreManager.OnBoughtStarsEvent -= OnBoughtStars;
    }

    void OnApplicationFocus(bool focus) {
        if (focus) {
            UpdateBalance();
        }
    }
}
