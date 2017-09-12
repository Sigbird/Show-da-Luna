using UnityEngine;

namespace YupiPlay.Luna.Store {
    class Inventory {
        private const string WalletKey = "YupiPlay.Luna.Store.Wallet";

        public static Inventory Instance {
            get {
                if (instance == null) instance = new Inventory();
                return instance;
            }
        }

        private static Inventory instance;

        private Inventory() {
            
        }

        public int GetBalance() {
            return PlayerPrefs.GetInt(WalletKey);
        }

        public void SetBalance(int balance) {
            PlayerPrefs.SetInt(WalletKey, balance);
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
        }

        public bool HasProduct(string productKey) {
            if (PlayerPrefs.GetInt(productKey) == 1) return true;            
            return false;
        }

        //retorna false se não tiver saldo para comprar
        public bool BuyProduct(string productKey, int price) {
            var balance = GetBalance();

            if (balance - price < 0) {
                return false;
            }

            balance -= price;
            SetProduct(productKey);
            SetBalance(balance);
            return true;
        }
    }
}
