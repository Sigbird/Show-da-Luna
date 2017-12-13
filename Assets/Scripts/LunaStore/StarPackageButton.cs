using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YupiPlay.Luna.Store { 
    public class StarPackageButton : MonoBehaviour {
        public Catalog.StarsPackages StarPackage;
        
	    // Use this for initialization
	    void Start () {
            
	    }
	
	    // Update is called once per frame
	    void Update () {
		
	    }

        public void BuyStarsPackage() {
            StoreManager.Instance.PurchaseStarPackage(StarPackage);
        }       
    }
}
