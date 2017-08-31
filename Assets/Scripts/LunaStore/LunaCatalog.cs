using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace YupiPlay.Luna.Store {
    public class LunaCatalog {
        public List<LunaStarPackage> Items;

        public static LunaStarPackage Stars10;
        public static LunaStarPackage Stars60;
        public static LunaStarPackage Stars150;

        public const string Stars10Id =  "stars_10_id";
        public const string Stars60Id =  "stars_60_id";
        public const string Stars150Id = "stars_150_id";

        public LunaCatalog() {            
            var stars10 = new LunaStarPackage();
            stars10.Id = Stars10Id;            
            stars10.GoogleStoreId = "com.yupiplay.luna.simplestarpack";
            stars10.Price = 0.99f;
            stars10.StarsAmount = 10;
            Stars10 = stars10;

            var stars60 = new LunaStarPackage();
            stars60.Id = Stars60Id;
            stars60.GoogleStoreId = "com.yupiplay.luna.superstarpack";
            stars60.Price = 4.99f;
            stars60.StarsAmount = 60;
            Stars60 = stars60;

            var stars150 = new LunaStarPackage();
            stars150.Id = Stars150Id;
            stars150.GoogleStoreId = "com.yupiplay.luna.megastarpack";
            stars150.Price = 9.99f;
            stars150.StarsAmount = 150;
            Stars150 = stars150;

            Items = new List<LunaStarPackage>();
            Items.Add(Stars10);
            Items.Add(Stars60);
            Items.Add(Stars150);
        }
    }

}
