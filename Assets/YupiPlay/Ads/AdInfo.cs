using UnityEngine;
using System.Collections;

namespace YupiPlay.Ads
{
    [System.Serializable]
    public class AdInfo
    {
        public string Name;
        public string AndroidId;
        public string IOSId;
        public SystemLanguage Language;

        public string Id
        {
            get
            {
                #if UNITY_IOS
                    return IOSId;
                #endif
                return AndroidId;
            }            
        }
        
    }

}
