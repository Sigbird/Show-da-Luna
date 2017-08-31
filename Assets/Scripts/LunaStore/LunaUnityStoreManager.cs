using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace YupiPlay.Luna.Store {
    class LunaUnityStoreManager : MonoBehaviour, IStoreListener {
        private IStoreController controller;
        private IExtensionProvider extensions;
        private LunaInventory inventory;

        public static LunaUnityStoreManager Instance;

        public delegate void BoughtStarsAction(int amount);
        public static event BoughtStarsAction OnBoughtStarsEvent;

        private void Awake() {
            var catalog = new LunaCatalog();

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (LunaStarPackage prod in catalog.Items) {
                builder.AddProduct(prod.Id, ProductType.Consumable, new IDs { { prod.GoogleStoreId, GooglePlay.Name } });
            }

            UnityPurchasing.Initialize(this, builder);

            inventory = new LunaInventory();
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
            int amount = 0;
            switch(id) {
                case LunaCatalog.Stars10Id:
                    amount = LunaCatalog.Stars10.StarsAmount;                    
                    break;
                case LunaCatalog.Stars60Id:
                    amount = LunaCatalog.Stars60.StarsAmount;                    
                    break;
                case LunaCatalog.Stars150Id:
                    amount = LunaCatalog.Stars150.StarsAmount;                    
                    break;
            }
            inventory.AddToBalance(amount);

            if (OnBoughtStarsEvent != null) OnBoughtStarsEvent(amount);
        }
    }
}
