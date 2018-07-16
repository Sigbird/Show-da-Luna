using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna.Aquario 
{    
    public class SwimHorizontalBehaviour : SwimmerBaseBehaviour {                
        override protected void BehaviourAction() {
            int random = Random.Range(-1,2);

            if (random != 0) {
                if ((int) transform.localScale.x == 1) {
                    rb.AddRelativeForce(Vector2.right * SwimForce);
                } else {
                    rb.AddRelativeForce(Vector2.left * SwimForce);
                }
            }
        }
        
    }
}
