using System;
using UnityEngine;
using UnityEngine.Purchasing;
using System.Collections.Generic;
using Soomla.Store;

namespace YupiPlay.Luna.Store {
    class StoreManager : MonoBehaviour, IStoreListener {
        private IStoreController controller;
        private IExtensionProvider extensions;
        private Inventory inventory;

        public static StoreManager Instance;        

        public delegate void BoughtStarsAction(int amount, int newBalance);
        public static event BoughtStarsAction OnBoughtStarsEvent;

        private void Awake() {
            var catalog = Catalog.Instance;

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (KeyValuePair<Catalog.StarsPackages, StarPackage> keyValue in Catalog.Instance.StarPackages) {
                var prod = keyValue.Value;
                builder.AddProduct(prod.Id, ProductType.Consumable, new IDs { { prod.GoogleStoreId, GooglePlay.Name } });
            }

            UnityPurchasing.Initialize(this, builder);

            inventory = Inventory.Instance;
            if (Instance == null) Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }        

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
            this.controller = controller;
            this.extensions = extensions;            
        }

        public void OnInitializeFailed(InitializationFailureReason error) {
            throw new NotImplementedException();
        }

        public void OnPurchaseFailed(Product i, PurchaseFailureReason p) {
            throw new NotImplementedException();
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e) {

            var id = e.purchasedProduct.definition.id;
            FulfillPurchase(id);
            return PurchaseProcessingResult.Complete;
        }

        public void FulfillPurchase(string id) {
            int amount = Catalog.Instance.PackagesIndex[id].StarsAmount;

            //StoreInventory.TakeItem(LunaStoreAssets.STARS_CURRENCY_ID, amount);
            StoreInventory.GiveItem(LunaStoreAssets.STARS_CURRENCY_ID, amount);
            //var newBalance = inventory.AddToBalance(amount);

            if (OnBoughtStarsEvent != null) OnBoughtStarsEvent(amount, 0);            
        }
        
        public void PurchaseStarPackage(Catalog.StarsPackages starPackage) {            
                controller.InitiatePurchase(Catalog.Instance.StarPackages[starPackage].Id);            
        }
    }
}
