namespace YupiPlay.Luna
{
    static class AchievementIds {
        public static string Welcome {
            get {
#if UNITY_ANDROID
                return GPGSIds.achievement_welcome_to_earth_to_luna;
#endif

                return GCIds.Welcome;
            }

            private set {}
        }

        public static string ShareGame {
            get {
#if UNITY_ANDROID
                return GPGSIds.achievement_share_earth_to_luna;
#endif

                return GCIds.ShareGame;

            }
            private set { }
        }

        public static string SeeYouAgain {
            get {
#if UNITY_ANDROID
                return GPGSIds.achievement_it_is_good_to_see_you_again;
#endif

                return GCIds.SeeYouAgain;

            }
            private set { }
        }

        public static string SharePainting {
            get {
#if UNITY_ANDROID
                return GPGSIds.achievement_share_a_painting_on_facebook;
#endif

                return GCIds.SharePainting;

            }
            private set { }
        }

        public static string FinishGame {
            get {
#if UNITY_ANDROID
                return GPGSIds.achievement_you_finished_earth_to_luna_lets_color;
#endif

                return GCIds.FinishGame;

            }
            private set { }
        }

    }
   
}
