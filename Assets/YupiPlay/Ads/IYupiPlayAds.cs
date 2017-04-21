using UnityEngine;
using System.Collections;

public interface IYupiPlayAds {

    bool IsAdReady();
    bool IsRewardedVIdeoReady();
    bool ShowAd();
    bool ShowRewardedVideo();
}
