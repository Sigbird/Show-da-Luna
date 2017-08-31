using UnityEngine;

namespace YupiPlay.Luna.Store {
    class LunaInventory {
        private const string WalletKey = "YupiPlay.Luna.Store.Wallet";

        public static LunaInventory Instance;

        public LunaInventory() {
            if (Instance == null) Instance = this;
        }

        public int GetBalance() {
            return PlayerPrefs.GetInt(WalletKey);
        }

        public void SetBalance(int balance) {
            PlayerPrefs.SetInt(WalletKey, balance);
        }

        public void AddToBalance(int amount) {
            var balance = GetBalance();
            balance += amount;
            SetBalance(balance);
        }

        public void RemoveFromBalance(int amount) {
            var balance = GetBalance();
            var newBalance = balance - amount;

            if (balance - amount <= 0) {
                balance = 0;
            } else {
                balance = newBalance;
            }

            SetBalance(balance);
        }

        public void AddProduct(string productKey) {
            PlayerPrefs.SetInt(productKey, 1);            
        }

        public bool HasPurchasedProduct(string productKey) {
            if (PlayerPrefs.GetInt(productKey) == 1) {
                return true;
            }
            return false;
        }

        //retorna false se não tiver saldo para comprar
        public bool BuyProduct(string productKey, int price) {
            var balance = GetBalance();
            if (balance - price < 0) {
                return false;
            }

            balance -= price;
            AddProduct(productKey);
            SetBalance(balance);
            return true;
        }
    }
}
