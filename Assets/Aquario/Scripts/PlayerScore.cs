using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YupiPlay.Luna.Aquario 
{
    public class PlayerScore : GameScore {
        public static PlayerScore Instance;

       void Awake() {            
            if (Instance == null) {
                Instance = this;
            }
        }
    }
}

