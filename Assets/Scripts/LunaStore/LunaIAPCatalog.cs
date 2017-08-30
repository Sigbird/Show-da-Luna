using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace YupiPlay.Luna.Store {
    public class LunaIAPCatalog {
        public List<LunaIAPProduct> Items;

        public const string SIMPLE_STAR_PACK_ID = "stars_10_id";
        public const string SIMPLE_STAR_PACK_PRODUCT_ID = "com.yupiplay.luna.simplestarpack";        

        public LunaIAPCatalog() {            
            var Stars10 = new LunaIAPProduct();
            Stars10.Id = "stars_10_id";            
            Stars10.GoogleStoreId = "com.yupiplay.luna.simplestarpack";
            Stars10.Price = 0.99f;

            var Stars60 = new LunaIAPProduct();
            Stars60.Id = "stars_60_id";
            Stars60.GoogleStoreId = "com.yupiplay.luna.superstarpack";
            Stars60.Price = 4.99f;

            var Stars150 = new LunaIAPProduct();
            Stars60.Id = "stars_150_id";
            Stars60.GoogleStoreId = "com.yupiplay.luna.megastarpack";
            Stars60.Price = 9.99f;

            Items = new List<LunaIAPProduct>();
            Items.Add(Stars10);
            Items.Add(Stars60);
            Items.Add(Stars150);
        }
    }

}
