using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna.Aquario 
{
    public class SwimVerticalBehaviour : SwimmerBaseBehaviour {
        protected override void BehaviourAction() {
            int random = Random.Range(-1, 2);
            rb.AddRelativeForce(new Vector2(0, random) * SwimForce);
        }

    }
}
