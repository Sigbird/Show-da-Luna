namespace YupiPlay.Luna
{
    static class AchievementIds {
        public static string Welcome {
            get {
#if UNITY_ANDROID
                return GPGSIds.achievement_welcome_to_earth_to_luna;
#endif
#if UNITY_IOS
                return GCIds.Welcome;
#endif
            }

            private set {}
        }

        public static string ShareGame {
            get {
#if UNITY_ANDROID
                return GPGSIds.achievement_share_earth_to_luna;
#endif
#if UNITY_IOS
                return GCIds.ShareGame;
#endif
            }
            private set { }
        }

        public static string SeeYouAgain {
            get {
#if UNITY_ANDROID
                return GPGSIds.achievement_it_is_good_to_see_you_again;
#endif
#if UNITY_IOS
                return GCIds.SeeYouAgain;
#endif                
            }
            private set { }
        }

        public static string SharePainting {
            get {
#if UNITY_ANDROID
                return GPGSIds.achievement_share_a_painting_on_facebook;
#endif
#if UNITY_IOS
                return GCIds.SharePainting;
#endif                
            }
            private set { }
        }

        public static string FinishGame {
            get {
#if UNITY_ANDROID
                return GPGSIds.achievement_you_finished_earth_to_luna_lets_color;
#endif
#if UNITY_IOS
                return GCIds.FinishGame;
#endif                
            }
            private set { }
        }

    }
   
}
