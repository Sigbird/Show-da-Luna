using UnityEngine;
using System.Collections;

namespace YupiPlay.Luna.Aquario 
{
    public class EnemyScoreController : AbstractScoreController {      

        override protected GameScore GetScoreOwner() {
            return EnemyScore.Instance;
        }
    }
}

