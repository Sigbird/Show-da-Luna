using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using Soomla;
using Soomla.Store;

using YupiStudios.Analytics;

public class LunaStoreManager : MonoBehaviour {
	public const string STARS_CREDIT_10 = "c10";
	public const string STARS_CREDIT_60 = "c60";
	public const string STARS_CREDIT_150 = "c150";
	public const string STARS_DEBIT_5 = "d5";
	public const string STARS_DEBIT_10 = "d10";
	public const string STARS_DEBIT_20 = "d20";
	public const string STARS_DEBIT_30 = "d30";
	public const string VIDEO = "video";
	public const string COLLECTION = "collection";

    #region Singleton declaration
    private static LunaStoreManager _instace;
    public static LunaStoreManager Instance
    {
        get {
            return _instace;
        }
        set
        {
            if (_instace == null)
                _instace = value;
        }
    }

	private bool _updated;

	public bool NeedUpdate { 
		get
		{
			bool updated = _updated;
			_updated = false;
			return updated;
		}
		private set
		{
			_updated = value;
		}
	}

	public delegate void UpdateBalanceAction();
	public delegate void BoughtStarsAction();
	public delegate void AsasPurchasedAction();
	public delegate void FullGamePurchasedAction();
	public delegate void VideoPurchasedAction(string videoID);
	public delegate void CollectionPurchasedAction(string collectionID);
	public delegate void BeforeBuyVideoAction();
	public delegate void BeforeBuyCollectionAction();
	public static event BeforeBuyVideoAction OnBuyVideo;
	public static event BeforeBuyCollectionAction OnBuyCollection;
	public static event VideoPurchasedAction OnVideoPurchased;
	public static event CollectionPurchasedAction OnCollectionPurchased;
	public static event FullGamePurchasedAction OnFullGamePurchased;
	public static event AsasPurchasedAction OnAsasPurchased;
	public static event UpdateBalanceAction OnBalanceChanged;
	public static event BoughtStarsAction OnBoughtStars;


	public static bool checkIfPurchased(string ITEM_ID) {
		try {
			int balance = StoreInventory.GetItemBalance(ITEM_ID);
			if (balance > 0) {
				return true;
			}
			return false;
		} catch (VirtualItemNotFoundException e) {
			Debug.Log (e.Message);
			return false;
		}

	}

    public bool AcquiredFullGame()
    {
        if (!StoreInitialized)
            return false;

		bool hasItem = checkIfPurchased (LunaStoreAssets.FULLGAME_LTVG_ITEM_ID);
		bool hasItemStars = checkIfPurchased (LunaStoreAssets.STARS_FULL_GAME_ITEM_ID);

		return (hasItem || hasItemStars);		       
    }

	public bool AcquiredCollection(int number)
	{
		if (!StoreInitialized)
			return false;

		switch (number) {
		case 1:
			bool hasItem = checkIfPurchased(LunaStoreAssets.COLLECTION01_LTVG_ITEM_ID);
			bool hasItemStars = checkIfPurchased(LunaStoreAssets.STARS_COLLECTION01_LTVG_ITEM_ID);

			return (hasItem || hasItemStars);
		//TODO REMAINING COLLECTIONS
		case 2:
			return checkIfPurchased(LunaStoreAssets.COLLECTION02_LTVG_ITEM_ID);
		case 3:
			return checkIfPurchased(LunaStoreAssets.COLLECTION03_LTVG_ITEM_ID);
		case 4:
			return checkIfPurchased(LunaStoreAssets.COLLECTION04_LTVG_ITEM_ID);
		case 5:
			return checkIfPurchased(LunaStoreAssets.COLLECTION05_LTVG_ITEM_ID);
		case 6:
			return checkIfPurchased(LunaStoreAssets.COLLECTION06_LTVG_ITEM_ID);		
		}
		return false;
	}

	public bool AcquiredVideo(int number, int collection)
	{
		if (!StoreInitialized)
			return false;
		
		bool hasItem;
		bool hasItemStars;
		if (collection == 1) {
			switch (number) {
			case 1:
				hasItem = checkIfPurchased(LunaStoreAssets.VIDEO_01_COL_01_LTVG_ITEM_ID);
				hasItemStars = checkIfPurchased(LunaStoreAssets.STARS_VIDEO_01_COL_01_LTVG_ITEM_ID);
				return (hasItem || hasItemStars);
			case 2:
				hasItem = checkIfPurchased(LunaStoreAssets.VIDEO_02_COL_01_LTVG_ITEM_ID);
				hasItemStars = checkIfPurchased(LunaStoreAssets.STARS_VIDEO_02_COL_01_LTVG_ITEM_ID);
				return (hasItem || hasItemStars);
			case 3:
				hasItem = checkIfPurchased(LunaStoreAssets.VIDEO_03_COL_01_LTVG_ITEM_ID);
				hasItemStars = checkIfPurchased(LunaStoreAssets.STARS_VIDEO_03_COL_01_LTVG_ITEM_ID);
				return (hasItem || hasItemStars);
			case 4:
				hasItem = checkIfPurchased(LunaStoreAssets.VIDEO_04_COL_01_LTVG_ITEM_ID);
				hasItemStars = checkIfPurchased(LunaStoreAssets.STARS_VIDEO_04_COL_01_LTVG_ITEM_ID);
				return (hasItem || hasItemStars);			
			}
			return false;
		}
		if (collection == 2) {
			switch (number) {
			case 1:
				return checkIfPurchased(LunaStoreAssets.VIDEO_01_COL_02_LTVG_ITEM_ID);
			case 2:
				return checkIfPurchased(LunaStoreAssets.VIDEO_02_COL_02_LTVG_ITEM_ID);
			case 3:
				return checkIfPurchased(LunaStoreAssets.VIDEO_03_COL_02_LTVG_ITEM_ID);
			case 4:
				return checkIfPurchased(LunaStoreAssets.VIDEO_04_COL_02_LTVG_ITEM_ID);
			}
			return false;
		}
		if (collection == 3) {
			switch (number) {
			case 1:
				return checkIfPurchased(LunaStoreAssets.VIDEO_01_COL_03_LTVG_ITEM_ID);
			case 2:
				return checkIfPurchased(LunaStoreAssets.VIDEO_02_COL_03_LTVG_ITEM_ID);
			case 3:
				return checkIfPurchased(LunaStoreAssets.VIDEO_03_COL_03_LTVG_ITEM_ID);
			case 4:
				return checkIfPurchased(LunaStoreAssets.VIDEO_04_COL_03_LTVG_ITEM_ID);
			}
			return false;
		}
		if (collection == 4) {
			switch (number) {
			case 1:
				return checkIfPurchased(LunaStoreAssets.VIDEO_01_COL_04_LTVG_ITEM_ID);
			case 2:
				return checkIfPurchased(LunaStoreAssets.VIDEO_02_COL_04_LTVG_ITEM_ID);
			case 3:
				return checkIfPurchased(LunaStoreAssets.VIDEO_03_COL_04_LTVG_ITEM_ID);
			case 4:
				return checkIfPurchased(LunaStoreAssets.VIDEO_04_COL_04_LTVG_ITEM_ID);
			}
			return false;
		}
		if (collection == 5) {
			switch (number) {
			case 1:
				return checkIfPurchased(LunaStoreAssets.VIDEO_01_COL_05_LTVG_ITEM_ID);
			case 2:
				return checkIfPurchased(LunaStoreAssets.VIDEO_02_COL_05_LTVG_ITEM_ID);
			case 3:
				return checkIfPurchased(LunaStoreAssets.VIDEO_03_COL_05_LTVG_ITEM_ID);
			case 4:
				return checkIfPurchased(LunaStoreAssets.VIDEO_04_COL_05_LTVG_ITEM_ID);
			case 5:
				return checkIfPurchased(LunaStoreAssets.VIDEO_05_COL_05_LTVG_ITEM_ID);
			}
			return false;
		}
		if (collection == 6) {
			switch (number) {
			case 1:
				return checkIfPurchased(LunaStoreAssets.VIDEO_01_COL_06_LTVG_ITEM_ID);
			case 2:
				return checkIfPurchased(LunaStoreAssets.VIDEO_02_COL_06_LTVG_ITEM_ID);
			case 3:
				return checkIfPurchased(LunaStoreAssets.VIDEO_03_COL_06_LTVG_ITEM_ID);
			case 4:
				return checkIfPurchased(LunaStoreAssets.VIDEO_04_COL_06_LTVG_ITEM_ID);			
			}
			return false;
		}
		return false;
	}

	public bool AcquiredMinigameAsas() {
		if (!StoreInitialized)
			return false;

		return checkIfPurchased(LunaStoreAssets.MINIGAME_ASAS_ITEM_ID);
	}

	public bool AcquiredMinigameCaracol() {
		if (!StoreInitialized)
			return false;
		
		return checkIfPurchased(LunaStoreAssets.MINIGAME_CARACOL_ITEM_ID);
	}
	
    #endregion

    public bool StoreInitialized { get; private set; }

    public static List<VirtualGood> NonConsumableItems = null;

    void Awake()
    {
        // Force GameObject Singleton
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
        StoreInitialized = false;
        StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialized;
        StoreEvents.OnUnexpectedStoreError += onUnexpectedStoreError;
        StoreEvents.OnMarketPurchase += onMarketPurchase;
        StoreEvents.OnMarketPurchaseStarted += onMarketPurchaseStarted;
        StoreEvents.OnMarketPurchaseCancelled += onMarketPurchaseCancelled;
		StoreEvents.OnCurrencyBalanceChanged += onCurrencyBalanceChanged;
		StoreEvents.OnItemPurchased += onItemPurchased;
        SoomlaStore.Initialize(new LunaStoreAssets());
	}

	public void RestorePurchase ()
	{
		SoomlaStore.RefreshInventory ();
		NeedUpdate = true;
		YupiAnalyticsEventHandler.PurchaseEvent("Restore Purchase", "");

		if (Social.localUser.authenticated) {
			GameSave.WriteSave();
		}
	}

    private void onMarketPurchaseCancelled(PurchasableVirtualItem pvi)
    {
		NeedUpdate = true;
        YupiAnalyticsEventHandler.PurchaseEvent("Cancelled Purchase", pvi.Name);
    }

    private void onMarketPurchaseStarted(PurchasableVirtualItem pvi)
    {
		NeedUpdate = true;
        YupiAnalyticsEventHandler.PurchaseEvent("Started Purchase", pvi.Name);
    }


    /// <summary>
    /// Handles a market purchase event.
    /// </summary>
    /// <param name="pvi">Purchasable virtual item.</param>
    /// <param name="purchaseToken">Purchase token.</param>
    private void onMarketPurchase(PurchasableVirtualItem pvi, string payload, Dictionary<string, string> extra)
    {
		NeedUpdate = true;
        YupiAnalyticsEventHandler.PurchaseEvent("Complete Purchase", pvi.Name);

		if (OnBoughtStars != null) {
			OnBoughtStars();
		}

		if (Social.localUser.authenticated) {
			GameSave.WriteSave();
		}
    }

    private void onUnexpectedStoreError(int errorCode)
    {
        SoomlaUtils.LogError("ExampleEventHandler", "error with code: " + errorCode);
    }

    private void onSoomlaStoreInitialized()
    {
        StoreInitialized = true;
    }

	public void StartIAB()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		SoomlaStore.StartIabServiceInBg();
#endif
	}

	public void StopIAB()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		SoomlaStore.StopIabServiceInBg();
#endif
	}

	public static void BuyItem(string ITEM_ID, string payload) {
		try {
			int itemBalance = StoreInventory.GetItemBalance(ITEM_ID);
			StoreInventory.BuyItem(ITEM_ID, payload);

		} catch (VirtualItemNotFoundException e) {
			Debug.Log (e.Message);
		} catch (InsufficientFundsException e) {
			Debug.Log (e.Message);
		}

	}

    public void PurchaseFullGame()
    {
        if (StoreInitialized) {
			BuyItem(LunaStoreAssets.STARS_FULL_GAME_ITEM_ID, LunaStoreAssets.STARS_FULL_GAME_ITEM_ID);
			return;
        } 
        Debug.LogError("Soomla Store Not Initialized");        
    }

	public void PurchaseCollection(int collection)
	{
		if (OnBuyCollection != null)
			OnBuyCollection ();

		if (StoreInitialized) 
		{
			switch (collection) {
			case 1:
				BuyItem(LunaStoreAssets.STARS_COLLECTION01_LTVG_ITEM_ID, COLLECTION);
				return;
			case 2:
				BuyItem(LunaStoreAssets.COLLECTION02_LTVG_ITEM_ID, COLLECTION);
				return;
			case 3:
				BuyItem(LunaStoreAssets.COLLECTION03_LTVG_ITEM_ID, COLLECTION);
				return;
			case 4:
				BuyItem(LunaStoreAssets.COLLECTION04_LTVG_ITEM_ID, COLLECTION);
				return;
			case 5:
				BuyItem(LunaStoreAssets.COLLECTION05_LTVG_ITEM_ID, COLLECTION);
				return;
			case 6:
				BuyItem(LunaStoreAssets.COLLECTION06_LTVG_ITEM_ID, COLLECTION);
				return;
			default:
				return;
			}
		}
		Debug.LogError("Soomla Store Not Initialized");
	}

	public void PurchaseVideo(int videoNumber, int collection)
	{
		if (OnBuyVideo != null)
			OnBuyVideo ();
		
		if (StoreInitialized) 
		{
			if(collection == 1){
				switch (videoNumber) {
				case 1:
					BuyItem(LunaStoreAssets.STARS_VIDEO_01_COL_01_LTVG_ITEM_ID, VIDEO);

					return;
				case 2:
					BuyItem(LunaStoreAssets.STARS_VIDEO_02_COL_01_LTVG_ITEM_ID, VIDEO);

					return;
				case 3:
					BuyItem(LunaStoreAssets.STARS_VIDEO_03_COL_01_LTVG_ITEM_ID, VIDEO);
					return;
				case 4:
					BuyItem(LunaStoreAssets.STARS_VIDEO_04_COL_01_LTVG_ITEM_ID, VIDEO);
					return;
				default:
					return;
				}
				return;
			}
			if(collection == 2){
				switch (videoNumber) {
				case 1:
					BuyItem(LunaStoreAssets.VIDEO_01_COL_02_LTVG_ITEM_ID, VIDEO);
					return;
				case 2:
					BuyItem(LunaStoreAssets.VIDEO_02_COL_02_LTVG_ITEM_ID, VIDEO);
					return;
				case 3:
					BuyItem(LunaStoreAssets.VIDEO_03_COL_02_LTVG_ITEM_ID, VIDEO);
					return;
				case 4:
					BuyItem(LunaStoreAssets.VIDEO_04_COL_02_LTVG_ITEM_ID, VIDEO);
					return;
				default:
					return;
				}		
			}
			if(collection == 3){
				switch (videoNumber) {
				case 1:
					BuyItem(LunaStoreAssets.VIDEO_01_COL_03_LTVG_ITEM_ID, VIDEO);
					return;
				case 2:
					BuyItem(LunaStoreAssets.VIDEO_02_COL_03_LTVG_ITEM_ID, VIDEO);
					return;
				case 3:
					BuyItem(LunaStoreAssets.VIDEO_03_COL_03_LTVG_ITEM_ID, VIDEO);
					return;
				case 4:
					BuyItem(LunaStoreAssets.VIDEO_04_COL_03_LTVG_ITEM_ID, VIDEO);
					return;
				default:
					return;
				}			
			}
			if(collection == 4){
				switch (videoNumber) {
				case 1:
					BuyItem(LunaStoreAssets.VIDEO_01_COL_04_LTVG_ITEM_ID, VIDEO);
					return;
				case 2:
					BuyItem(LunaStoreAssets.VIDEO_02_COL_04_LTVG_ITEM_ID, VIDEO);
					return;
				case 3:
					BuyItem(LunaStoreAssets.VIDEO_03_COL_04_LTVG_ITEM_ID, VIDEO);
					return;
				case 4:
					BuyItem(LunaStoreAssets.VIDEO_04_COL_04_LTVG_ITEM_ID, VIDEO);
					return;
				default:
					return;
				}			
			}
			if(collection == 5){
				switch (videoNumber) {
				case 1:
					BuyItem(LunaStoreAssets.VIDEO_01_COL_05_LTVG_ITEM_ID, VIDEO);
					return;
				case 2:
					BuyItem(LunaStoreAssets.VIDEO_02_COL_05_LTVG_ITEM_ID, VIDEO);
					return;
				case 3:
					BuyItem(LunaStoreAssets.VIDEO_03_COL_05_LTVG_ITEM_ID, VIDEO);
					return;
				case 4:
					BuyItem(LunaStoreAssets.VIDEO_04_COL_05_LTVG_ITEM_ID, VIDEO);
					return;				
				case 5:
					BuyItem(LunaStoreAssets.VIDEO_05_COL_05_LTVG_ITEM_ID, VIDEO);
					return;
				default:
					return;
				}
			}
			if(collection == 6){
				switch (videoNumber) {
				case 1:
					BuyItem(LunaStoreAssets.VIDEO_01_COL_06_LTVG_ITEM_ID, VIDEO);
					return;
				case 2:
					BuyItem(LunaStoreAssets.VIDEO_02_COL_06_LTVG_ITEM_ID, VIDEO);
					return;
				case 3:
					BuyItem(LunaStoreAssets.VIDEO_03_COL_06_LTVG_ITEM_ID, VIDEO);
					return;
				case 4:
					BuyItem(LunaStoreAssets.VIDEO_04_COL_06_LTVG_ITEM_ID, VIDEO);
					return;								
				default:
					return;
				}
			}
			return;
		}
		Debug.LogError("Soomla Store Not Initialized");
	}

	public void PurchaseMinigameAsas()
	{
		if (StoreInitialized) {
			BuyItem(LunaStoreAssets.MINIGAME_ASAS_ITEM_ID, LunaStoreAssets.MINIGAME_ASAS_ITEM_ID);
			return;
		} 
		Debug.LogError("Soomla Store Not Initialized");        
	}

	public void PurchaseMinigameCaracol()
	{
		if (StoreInitialized) {
			BuyItem(LunaStoreAssets.MINIGAME_CARACOL_ITEM_ID, LunaStoreAssets.MINIGAME_CARACOL_ITEM_ID);
			return;
		} 
		Debug.LogError("Soomla Store Not Initialized");        
	}

	public void PurchaseSimpleStarPack() {
		if (StoreInitialized) {
			BuyItem(LunaStoreAssets.SIMPLE_STAR_PACK_ID, STARS_CREDIT_10);
		}	
		Debug.LogError("Soomla Store Not Initialized");
	}

	public void PurchaseSuperStarPack() {
		if (StoreInitialized) {
			BuyItem(LunaStoreAssets.SUPER_STAR_PACK_ID, STARS_CREDIT_60);
		}	
		Debug.LogError("Soomla Store Not Initialized");
	}

	public void PurchaseMegaStarPack() {
		if (StoreInitialized) {
			BuyItem(LunaStoreAssets.MEGA_STAR_PACK_ID, STARS_CREDIT_150);
		}	
		Debug.LogError("Soomla Store Not Initialized");
	}

	public bool CanAffordItem(string ITEM_ID) {
		if (StoreInitialized) {
			return StoreInventory.CanAfford(ITEM_ID);
		}
		return false;
	}

	public bool AcquiredAnyMarketItens() {
		if (StoreInitialized) {
			bool hasFullGame = checkIfPurchased(LunaStoreAssets.FULLGAME_LTVG_ITEM_ID);
			bool hasCollection01 = checkIfPurchased(LunaStoreAssets.COLLECTION01_LTVG_ITEM_ID);;

			bool hasVideo01Col01 = checkIfPurchased(LunaStoreAssets.VIDEO_01_COL_01_LTVG_ITEM_ID);
			bool hasVideo02Col01 = checkIfPurchased(LunaStoreAssets.VIDEO_02_COL_01_LTVG_ITEM_ID);
			bool hasVideo03Col01 = checkIfPurchased(LunaStoreAssets.VIDEO_03_COL_01_LTVG_ITEM_ID);
			bool hasVideo04Col01 = checkIfPurchased(LunaStoreAssets.VIDEO_04_COL_01_LTVG_ITEM_ID);

			return (hasFullGame || hasCollection01 || hasVideo01Col01 || hasVideo02Col01 || hasVideo03Col01 || hasVideo04Col01);
		}
		return false;
	}

	public static void CallBalanceChangeEvent() {
		if (OnBalanceChanged != null) {
			OnBalanceChanged();
		}
	}

	public void onCurrencyBalanceChanged(VirtualCurrency currency, int balance, int amountAdded)  {
		CallBalanceChangeEvent();

		if (amountAdded > 0) {
			YupiAnalyticsEventHandler.StarsEvent("Credit", "PlayStorePurchase", amountAdded);
		} else {
			YupiAnalyticsEventHandler.StarsEvent("Debit", "LunaPurchase", amountAdded);
		}
	}

	public void onItemPurchased(PurchasableVirtualItem pvi, string payload) {
		//if any of the minigames was purchased
		if (payload == LunaStoreAssets.MINIGAME_ASAS_ITEM_ID || payload == LunaStoreAssets.MINIGAME_CARACOL_ITEM_ID) {
			if (OnAsasPurchased != null) {
				OnAsasPurchased();
			}
		}

		if (payload == LunaStoreAssets.MINIGAME_ASAS_ITEM_ID) {
			if (OnAsasPurchased != null) {
				OnAsasPurchased();
            }
        }

		if (payload == LunaStoreAssets.STARS_FULL_GAME_ITEM_ID) {
			if (OnFullGamePurchased != null) {
				OnFullGamePurchased();
			}
		}

		if (payload == VIDEO) {
			if (OnVideoPurchased != null) {
				OnVideoPurchased(pvi.ItemId);
			}
		}

		if (payload == COLLECTION) {
			if (OnCollectionPurchased != null) {
				OnCollectionPurchased(pvi.ItemId);
			}
		}

		if (Social.localUser.authenticated) {
			GameSave.WriteSave();
		}
	}
}
