using UnityEngine;

namespace YupiPlay.Luna.Store {
    class Inventory {
        public const string WalletKey = "lunaStoreWallet";

        public static Inventory Instance {
            get {
                if (instance == null) instance = new Inventory();
                return instance;
            }
        }

        private static Inventory instance;

        public delegate void BalanceChangeAction();
        public static event BalanceChangeAction OnBalanceChange;
        public delegate void BuyProductAction(string productId);
        public static event BuyProductAction OnBuyProduct;
        public delegate void GamesServicesDialogAction(bool show);
        public static event GamesServicesDialogAction ShowDialogEvent;

        private Inventory() {
            
        }

        public int GetBalance() {
            return PlayerPrefs.GetInt(WalletKey);
        }

        public void SetBalance(int balance) {
            int oldBalance = GetBalance();

            if (balance != oldBalance) {               
                PlayerPrefs.SetInt(WalletKey, balance);
                PlayerPrefs.Save();

                if (OnBalanceChange != null) OnBalanceChange();

                if (Social.localUser.authenticated) {
                    GameSave.WriteSave();
                } else {
                    if (ShowDialogEvent != null) {
                        ShowDialogEvent(true);
                    }
                }
            }            
        }

        //retorna o novo saldo
        public int AddToBalance(int amount) {
            var balance = GetBalance();
            balance += amount;
            SetBalance(balance);
            return balance;
        }

        //retorna novo saldo
        public int RemoveFromBalance(int amount) {
            var balance = GetBalance();
            var newBalance = balance - amount;

            if (balance - amount <= 0) {
                balance = 0;
            } else {
                balance = newBalance;
            }

            SetBalance(balance);
            return balance;
        }

        public void SetProduct(string productKey) {
            PlayerPrefs.SetInt(productKey, 1);
            PlayerPrefs.Save();

            if (OnBuyProduct != null) OnBuyProduct(productKey);
        }

        public bool HasProduct(string productKey) {
            if (PlayerPrefs.GetInt(productKey) == 1) return true;            
            return false;
        }

        //retorna false se não tiver saldo para comprar
        public bool BuyProduct(string productKey, int price) {
            var balance = GetBalance();
            int newBalance = balance - price;

            if (newBalance < 0) {
                return false;
            }
                        
            SetProduct(productKey);
            SetBalance(newBalance);
            return true;
        }

        public bool AcquiredCollection(int number) {            
            switch (number) {
                case 1:
                    bool hasItem = HasProduct(LunaStoreAssets.COLLECTION01_LTVG_ITEM_ID);
                    bool hasItemStars = HasProduct(LunaStoreAssets.STARS_COLLECTION01_LTVG_ITEM_ID);

                    return (hasItem || hasItemStars);                
                case 2:
                    return HasProduct(LunaStoreAssets.COLLECTION02_LTVG_ITEM_ID);
                case 3:
                    return HasProduct(LunaStoreAssets.COLLECTION03_LTVG_ITEM_ID);
                case 4:
                    return HasProduct(LunaStoreAssets.COLLECTION04_LTVG_ITEM_ID);
                case 5:
                    return HasProduct(LunaStoreAssets.COLLECTION05_LTVG_ITEM_ID);
                case 6:
                    return HasProduct(LunaStoreAssets.COLLECTION06_LTVG_ITEM_ID);
                case 7:
                    return HasProduct(LunaStoreAssets.COLLECTION07_LTVG_ITEM_ID);
                case 8:
                    return HasProduct(LunaStoreAssets.COLLECTION08_LTVG_ITEM_ID);
                case 9:
                    return HasProduct(LunaStoreAssets.COLLECTION09_LTVG_ITEM_ID);
            }
            return false;
        }

        public bool AcquiredVideo(int number, int collection) {           
            bool hasItem;
            bool hasItemStars;

            if (collection == 1) {
                switch (number) {
                    case 1:
                        hasItem = HasProduct(LunaStoreAssets.VIDEO_01_COL_01_LTVG_ITEM_ID);
                        hasItemStars = HasProduct(LunaStoreAssets.STARS_VIDEO_01_COL_01_LTVG_ITEM_ID);
                        return (hasItem || hasItemStars);
                    case 2:
                        hasItem = HasProduct(LunaStoreAssets.VIDEO_02_COL_01_LTVG_ITEM_ID);
                        hasItemStars = HasProduct(LunaStoreAssets.STARS_VIDEO_02_COL_01_LTVG_ITEM_ID);
                        return (hasItem || hasItemStars);
                    case 3:
                        hasItem = HasProduct(LunaStoreAssets.VIDEO_03_COL_01_LTVG_ITEM_ID);
                        hasItemStars = HasProduct(LunaStoreAssets.STARS_VIDEO_03_COL_01_LTVG_ITEM_ID);
                        return (hasItem || hasItemStars);
                    case 4:
                        hasItem = HasProduct(LunaStoreAssets.VIDEO_04_COL_01_LTVG_ITEM_ID);
                        hasItemStars = HasProduct(LunaStoreAssets.STARS_VIDEO_04_COL_01_LTVG_ITEM_ID);
                        return (hasItem || hasItemStars);
                }
                return false;
            }
            if (collection == 2) {
                switch (number) {
                    case 1:
                        return HasProduct(LunaStoreAssets.VIDEO_01_COL_02_LTVG_ITEM_ID);
                    case 2:
                        return HasProduct(LunaStoreAssets.VIDEO_02_COL_02_LTVG_ITEM_ID);
                    case 3:
                        return HasProduct(LunaStoreAssets.VIDEO_03_COL_02_LTVG_ITEM_ID);
                    case 4:
                        return HasProduct(LunaStoreAssets.VIDEO_04_COL_02_LTVG_ITEM_ID);
                }
                return false;
            }
            if (collection == 3) {
                switch (number) {
                    case 1:
                        return HasProduct(LunaStoreAssets.VIDEO_01_COL_03_LTVG_ITEM_ID);
                    case 2:
                        return HasProduct(LunaStoreAssets.VIDEO_02_COL_03_LTVG_ITEM_ID);
                    case 3:
                        return HasProduct(LunaStoreAssets.VIDEO_03_COL_03_LTVG_ITEM_ID);
                    case 4:
                        return HasProduct(LunaStoreAssets.VIDEO_04_COL_03_LTVG_ITEM_ID);
                }
                return false;
            }
            if (collection == 4) {
                switch (number) {
                    case 1:
                        return HasProduct(LunaStoreAssets.VIDEO_01_COL_04_LTVG_ITEM_ID);
                    case 2:
                        return HasProduct(LunaStoreAssets.VIDEO_02_COL_04_LTVG_ITEM_ID);
                    case 3:
                        return HasProduct(LunaStoreAssets.VIDEO_03_COL_04_LTVG_ITEM_ID);
                    case 4:
                        return HasProduct(LunaStoreAssets.VIDEO_04_COL_04_LTVG_ITEM_ID);
                }
                return false;
            }
            if (collection == 5) {
                switch (number) {
                    case 1:
                        return HasProduct(LunaStoreAssets.VIDEO_01_COL_05_LTVG_ITEM_ID);
                    case 2:
                        return HasProduct(LunaStoreAssets.VIDEO_02_COL_05_LTVG_ITEM_ID);
                    case 3:
                        return HasProduct(LunaStoreAssets.VIDEO_03_COL_05_LTVG_ITEM_ID);
                    case 4:
                        return HasProduct(LunaStoreAssets.VIDEO_04_COL_05_LTVG_ITEM_ID);
                    case 5:
                        return HasProduct(LunaStoreAssets.VIDEO_05_COL_05_LTVG_ITEM_ID);
                }
                return false;
            }
            if (collection == 6) {
                switch (number) {
                    case 1:
                        return HasProduct(LunaStoreAssets.VIDEO_01_COL_06_LTVG_ITEM_ID);
                    case 2:
                        return HasProduct(LunaStoreAssets.VIDEO_02_COL_06_LTVG_ITEM_ID);
                    case 3:
                        return HasProduct(LunaStoreAssets.VIDEO_03_COL_06_LTVG_ITEM_ID);
                    case 4:
                        return HasProduct(LunaStoreAssets.VIDEO_04_COL_06_LTVG_ITEM_ID);
                }
                return false;
            }
            if (collection == 7) {
                switch (number) {
                    case 1:
                        return HasProduct(LunaStoreAssets.VIDEO_01_COL_07_LTVG_ITEM_ID);
                    case 2:
                        return HasProduct(LunaStoreAssets.VIDEO_02_COL_07_LTVG_ITEM_ID);
                    case 3:
                        return HasProduct(LunaStoreAssets.VIDEO_03_COL_07_LTVG_ITEM_ID);
                    case 4:
                        return HasProduct(LunaStoreAssets.VIDEO_04_COL_07_LTVG_ITEM_ID);
                    case 5:
                        return HasProduct(LunaStoreAssets.VIDEO_05_COL_07_LTVG_ITEM_ID);
                }
                return false;
            }
            if (collection == 8) {
                switch (number) {
                    case 1:
                        return HasProduct(LunaStoreAssets.VIDEO_01_COL_08_LTVG_ITEM_ID);
                    case 2:
                        return HasProduct(LunaStoreAssets.VIDEO_02_COL_08_LTVG_ITEM_ID);
                    case 3:
                        return HasProduct(LunaStoreAssets.VIDEO_03_COL_08_LTVG_ITEM_ID);
                    case 4:
                        return HasProduct(LunaStoreAssets.VIDEO_04_COL_08_LTVG_ITEM_ID);
                    case 5:
                        return HasProduct(LunaStoreAssets.VIDEO_05_COL_08_LTVG_ITEM_ID);
                }
                return false;
            }
            if (collection == 9) {
                switch (number) {
                    case 1:
                        return HasProduct(LunaStoreAssets.VIDEO_01_COL_09_LTVG_ITEM_ID);
                    case 2:
                        return HasProduct(LunaStoreAssets.VIDEO_02_COL_09_LTVG_ITEM_ID);
                    case 3:
                        return HasProduct(LunaStoreAssets.VIDEO_03_COL_09_LTVG_ITEM_ID);
                    case 4:
                        return HasProduct(LunaStoreAssets.VIDEO_04_COL_09_LTVG_ITEM_ID);
                    case 5:
                        return HasProduct(LunaStoreAssets.VIDEO_05_COL_09_LTVG_ITEM_ID);
                }
                return false;
            }
            return false;
        }

        public bool PurchaseCollection(int collection) {                        
            switch (collection) {
                case 1:
                    return BuyProduct(LunaStoreAssets.STARS_COLLECTION01_LTVG_ITEM_ID, LunaStoreAssets.COLLECTION_PRICE);                    
                case 2:
                    return BuyProduct(LunaStoreAssets.COLLECTION02_LTVG_ITEM_ID, LunaStoreAssets.COLLECTION_PRICE);                    
                case 3:
                    return BuyProduct(LunaStoreAssets.COLLECTION03_LTVG_ITEM_ID, LunaStoreAssets.COLLECTION_PRICE);                    
                case 4:
                    return BuyProduct(LunaStoreAssets.COLLECTION04_LTVG_ITEM_ID, LunaStoreAssets.COLLECTION_PRICE);                    
                case 5:
                    return BuyProduct(LunaStoreAssets.COLLECTION05_LTVG_ITEM_ID, LunaStoreAssets.COLLECTION_PRICE);
                case 6:
                    return BuyProduct(LunaStoreAssets.COLLECTION06_LTVG_ITEM_ID, LunaStoreAssets.COLLECTION_PRICE);                    
                case 7:
                    return BuyProduct(LunaStoreAssets.COLLECTION07_LTVG_ITEM_ID, LunaStoreAssets.COLLECTION_PRICE);                    
                case 8:
                    return BuyProduct(LunaStoreAssets.COLLECTION08_LTVG_ITEM_ID, LunaStoreAssets.COLLECTION_PRICE);                    
                case 9:
                    return BuyProduct(LunaStoreAssets.COLLECTION09_LTVG_ITEM_ID, LunaStoreAssets.COLLECTION_PRICE);                    
                default:
                    return false;
            }                        
        }

        public bool PurchaseVideo(int videoNumber, int collection) {           
            if (collection == 1) {
                switch (videoNumber) {
                    case 1:
                        return BuyProduct(LunaStoreAssets.STARS_VIDEO_01_COL_01_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);                        
                    case 2:
                        return BuyProduct(LunaStoreAssets.STARS_VIDEO_02_COL_01_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 3:
                        return BuyProduct(LunaStoreAssets.STARS_VIDEO_03_COL_01_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 4:
                        return BuyProduct(LunaStoreAssets.STARS_VIDEO_04_COL_01_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    default:
                        return false;
                }                
            }

            if (collection == 2) {
                switch (videoNumber) {
                    case 1:
                        return BuyProduct(LunaStoreAssets.VIDEO_01_COL_02_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 2:
                        return BuyProduct(LunaStoreAssets.VIDEO_02_COL_02_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 3:
                        return BuyProduct(LunaStoreAssets.VIDEO_03_COL_02_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 4:
                        return BuyProduct(LunaStoreAssets.VIDEO_04_COL_02_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    default:
                        return false;
                }
            }
            if (collection == 3) {
                switch (videoNumber) {
                    case 1:
                        return BuyProduct(LunaStoreAssets.VIDEO_01_COL_03_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 2:
                        return BuyProduct(LunaStoreAssets.VIDEO_02_COL_03_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 3:
                        return BuyProduct(LunaStoreAssets.VIDEO_03_COL_03_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 4:
                        return BuyProduct(LunaStoreAssets.VIDEO_04_COL_03_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);            
                    default:
                        return false;
                }
            }

            if (collection == 4) {
                switch (videoNumber) {
                    case 1:
                        return BuyProduct(LunaStoreAssets.VIDEO_01_COL_04_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 2:
                        return BuyProduct(LunaStoreAssets.VIDEO_02_COL_04_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 3:
                        return BuyProduct(LunaStoreAssets.VIDEO_03_COL_04_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 4:
                        return BuyProduct(LunaStoreAssets.VIDEO_04_COL_04_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);            
                    default:
                        return false;
                }
            }

            if (collection == 5) {
                switch (videoNumber) {
                    case 1:
                        return BuyProduct(LunaStoreAssets.VIDEO_01_COL_05_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 2:
                        return BuyProduct(LunaStoreAssets.VIDEO_02_COL_05_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 3:
                        return BuyProduct(LunaStoreAssets.VIDEO_03_COL_05_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 4:
                        return BuyProduct(LunaStoreAssets.VIDEO_04_COL_05_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 5:
                        return BuyProduct(LunaStoreAssets.VIDEO_05_COL_05_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    default:
                        return false;
                }
            }

            if (collection == 6) {
                switch (videoNumber) {
                    case 1:
                        return BuyProduct(LunaStoreAssets.VIDEO_01_COL_06_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 2:
                        return BuyProduct(LunaStoreAssets.VIDEO_02_COL_06_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 3:
                        return BuyProduct(LunaStoreAssets.VIDEO_03_COL_06_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 4:
                        return BuyProduct(LunaStoreAssets.VIDEO_04_COL_06_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    default:
                        return false;
                }
            }

            if (collection == 7) {
                switch (videoNumber) {
                    case 1:
                        return BuyProduct(LunaStoreAssets.VIDEO_01_COL_07_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 2:
                        return BuyProduct(LunaStoreAssets.VIDEO_02_COL_07_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 3:
                        return BuyProduct(LunaStoreAssets.VIDEO_03_COL_07_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 4:
                        return BuyProduct(LunaStoreAssets.VIDEO_04_COL_07_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 5:
                        return BuyProduct(LunaStoreAssets.VIDEO_05_COL_07_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    default:
                        return false;
                }
            }

            if (collection == 8) {
                switch (videoNumber) {
                    case 1:
                        return BuyProduct(LunaStoreAssets.VIDEO_01_COL_08_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 2:
                        return BuyProduct(LunaStoreAssets.VIDEO_02_COL_08_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 3:
                        return BuyProduct(LunaStoreAssets.VIDEO_03_COL_08_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 4:
                        return BuyProduct(LunaStoreAssets.VIDEO_04_COL_08_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 5:
                        return BuyProduct(LunaStoreAssets.VIDEO_05_COL_08_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    default:
                        return false;
                }
            }

            if (collection == 9) {
                switch (videoNumber) {
                    case 1:
                        return BuyProduct(LunaStoreAssets.VIDEO_01_COL_09_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 2:
                        return BuyProduct(LunaStoreAssets.VIDEO_02_COL_09_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 3:
                        return BuyProduct(LunaStoreAssets.VIDEO_03_COL_09_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 4:
                        return BuyProduct(LunaStoreAssets.VIDEO_04_COL_09_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    case 5:
                        return BuyProduct(LunaStoreAssets.VIDEO_05_COL_09_LTVG_ITEM_ID, LunaStoreAssets.VIDEO_PRICE);
                    default:
                        return false;
                }
            }

            return false;
        }

    }
}
