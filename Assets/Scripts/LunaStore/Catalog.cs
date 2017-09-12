using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace YupiPlay.Luna.Store {
    public class Catalog {
        public enum StarsPackages {Stars10, Stars60, Stars150};

        public Dictionary<StarsPackages, StarPackage> StarPackages;
        public Dictionary<string, StarPackage> PackagesIndex;         

        public static Catalog Instance {
            get {
                if (instance == null) {
                    instance = new Catalog();
                }
                return instance;
            }
        }

        private static Catalog instance;

        private Catalog() {                
            var Stars10 = new StarPackage();
            Stars10.Package = StarsPackages.Stars10;
            Stars10.Id = "stars_10_id";
            Stars10.GoogleStoreId = "com.yupiplay.luna.simplestarpack";
            Stars10.Price = 0.99f;
            Stars10.StarsAmount = 10;

            var Stars60 = new StarPackage();
            Stars60.Package = StarsPackages.Stars60;
            Stars60.Id = "stars_60_id";
            Stars60.GoogleStoreId = "com.yupiplay.luna.superstarpack";
            Stars60.Price = 4.99f;
            Stars60.StarsAmount = 60;

            var Stars150 = new StarPackage();
            Stars150.Package = StarsPackages.Stars150;
            Stars150.Id = "stars_150_id";
            Stars150.GoogleStoreId = "com.yupiplay.luna.megastarpack";
            Stars150.Price = 9.99f;
            Stars150.StarsAmount = 150;

            StarPackages = new Dictionary<StarsPackages, StarPackage>();
            PackagesIndex = new Dictionary<string, StarPackage>();

            StarPackages[StarsPackages.Stars10]  = Stars10;
            StarPackages[StarsPackages.Stars60]  = Stars60;
            StarPackages[StarsPackages.Stars150] = Stars150;
            PackagesIndex[Stars10.Id] = Stars10;
            PackagesIndex[Stars60.Id] = Stars60;
            PackagesIndex[Stars150.Id] = Stars150;
        }
    }

}
