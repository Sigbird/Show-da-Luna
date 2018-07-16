using UnityEngine;
using System.Collections;

namespace YupiPlay.Luna.Aquario 
{
    public class PlayerScoreController : AbstractScoreController {        

        override protected GameScore GetScoreOwner() {
            return PlayerScore.Instance;
        }
    }

}
