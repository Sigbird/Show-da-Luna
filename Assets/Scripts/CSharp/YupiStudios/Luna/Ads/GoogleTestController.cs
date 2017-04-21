using UnityEngine;
using System.Collections;
using YupiPlay.Ads;

public class GoogleTestController : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void ShowNative() {
        AdsManager.ShowNativeAd();
    }

    public void ShowRewarded() {
        AdsManager.ShowVideo();
    }
}
