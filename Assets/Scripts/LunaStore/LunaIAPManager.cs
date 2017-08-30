using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace YupiPlay.Luna.Store {
    class LunaIAPManager : MonoBehaviour, IStoreListener {
        private IStoreController controller;
        private IExtensionProvider extensions;

        private void Awake() {
            var catalog = new LunaIAPCatalog();

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (LunaIAPProduct prod in catalog.Items) {
                builder.AddProduct(prod.Id, ProductType.Consumable, new IDs { { prod.GoogleStoreId, GooglePlay.Name } });
            }

            UnityPurchasing.Initialize(this, builder);
        }        

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
            this.controller = controller;
            this.extensions = extensions;            
        }

        public void OnInitializeFailed(InitializationFailureReason error) {
            //throw new NotImplementedException();
        }

        public void OnPurchaseFailed(Product i, PurchaseFailureReason p) {
            throw new NotImplementedException();
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e) {
            return PurchaseProcessingResult.Complete;
        }
    }
}
