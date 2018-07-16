using UnityEngine;
using System.Collections;

namespace YupiPlay.Luna.Aquario 
{
    public class EnemyScore : GameScore {
        public static EnemyScore Instance;

        void Awake() {
            if (Instance == null) {
                Instance = this;
            }
        }
    }
}

